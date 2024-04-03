var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;
var config = builder.Configuration;

builder.Logging.AddSentry(options => options.Dsn = builder.Configuration["Sentry:Dsn"]);

if(env.EnvironmentName != Environments.Development){
    builder.Configuration.AddSecretsManager(
    region: Amazon.RegionEndpoint.USEast1, 
    configurator: options => {
        options.PollingInterval = TimeSpan.FromHours(1);
        options.AcceptedSecretArns = new List<string>(){"Issuer", "Audience", "SecurityKey", "ConnectionStringDB"};
        options.KeyGenerator = (secret, name) => secret.Name == "ConnectionStringDB" ? $"ApiSettings:{secret.Name}" : $"JwtOptions:{secret.Name}";
    });
}

builder.Services.AddApiProblemDetails();

builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
builder.Services.AddScoped(c => c.GetService<IOptionsSnapshot<ApiSettings>>().Value);
builder.Services.AddScoped(c => c.GetService<IOptionsSnapshot<JwtOptions>>().Value);

var serviceProvider = builder.Services.BuildServiceProvider();
var apiSettings = serviceProvider.GetService<ApiSettings>();
var jwtOptions = serviceProvider.GetService<JwtOptions>();

Console.WriteLine($"ApiSettings: ${JsonConvert.SerializeObject(apiSettings)}");

Log.Logger = new LoggerConfiguration()
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri($"{apiSettings.ElasticSearch.Endpoint}"))
            {
                IndexFormat = $"{apiSettings.ElasticSearch.Index[0]}",
                AutoRegisterTemplate = true
            })
            .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(dispose: true);
builder.Host.UseSerilog(Log.Logger); 

builder.Services.AddDbContext<IdentityDBContext>(options =>
        options.UseMySql(apiSettings.ConnectionStringDB, ServerVersion.AutoDetect(apiSettings.ConnectionStringDB)));

builder.Services.AddDbContext<CManagerDBContext>(options =>
        options.UseMySql(apiSettings.ConnectionStringDB, ServerVersion.AutoDetect(apiSettings.ConnectionStringDB)));

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<IdentityDBContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IIdentityService, IdentityService>();

IoCExtensions.AddIoC(builder.Services);
PollyExtensions.AddPolly(builder.Services);

builder.Services.AddStackExchangeRedisCache(redis => 
{
    redis.InstanceName = apiSettings?.Cache.InstanceName;
    redis.Configuration = apiSettings?.Cache.Configuration;
});

builder.Services.AddControllers()
                .AddJsonOptions(options => { options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;})
                .AddFluentValidation(options => { options.RegisterValidatorsFromAssemblyContaining<GetAnunciosQuery.Anuncios>();});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "CManager.Api",
        Version = "v1"
    });

    options.SwaggerDoc("v2", new OpenApiInfo
    {
        Title = "CManager.Api",
        Version = "v2"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. 
                        Enter 'Bearer' [space] and then your token in the text input below. 
                        Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            },
            Scheme = "oauth2",
            Name = "Bearer",
            In = ParameterLocation.Header,

            },
            new List<string>()
        }
    });
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddAuthentication(builder.Configuration, jwtOptions);
builder.Services.AddHealthChecks();
builder.Services.AddTransient<ExceptionLoggingMiddleware>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseCors();
app.UseSerilogRequestLogging();
app.UseProblemDetails();
app.UseMiddleware<ExceptionLoggingMiddleware>();
app.UseSentryTracing();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.UseHttpMetrics();
app.UseAuthorization();
app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/health");
    endpoints.MapMetrics();
});

app.Run();