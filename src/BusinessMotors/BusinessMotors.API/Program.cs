var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;

builder.Logging.AddSentry(options => options.Dsn = builder.Configuration["Sentry:Dsn"]);

if(env.EnvironmentName != Environments.Development){
    builder.Configuration.AddSecretsManager(
    region: Amazon.RegionEndpoint.USEast1, 
    configurator: options => {
        options.PollingInterval = TimeSpan.FromHours(1);
        options.AcceptedSecretArns = new List<string>(){"Issuer", "Audience", "ConnectionStringDB"};
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

builder.Services.AddDbContext<BusinessMotorsDBContext>(options =>
        options.UseMySql(apiSettings.ConnectionStringDB, ServerVersion.AutoDetect(apiSettings.ConnectionStringDB)));

builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

builder.Services.AddDefaultIdentity<Usuario>()
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
                .AddJsonOptions(options => 
                { 
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                })
                .AddFluentValidation(options => { options.RegisterValidatorsFromAssemblyContaining<GetAnunciosQuery.Anuncios>();});

builder.Services.AddEndpointsApiExplorer();
SwaggerGen.AddSwaggerGen(builder.Services);

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddHealthChecks();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddAuthentication(builder.Configuration, jwtOptions);
builder.Services.AddAuthorization();
// builder.Services.AddTransient<ExceptionLoggingMiddleware>();

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

app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyHeader();
    options.AllowAnyMethod();
});
app.UseSerilogRequestLogging();
app.UseSentryTracing();
app.UseProblemDetails();
app.UseIpRateLimiting();
// app.UseMiddleware<ExceptionLoggingMiddleware>();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpMetrics();
app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/health");
    endpoints.MapMetrics();
});

app.Run();