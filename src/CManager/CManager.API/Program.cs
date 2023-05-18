var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;
var config = builder.Configuration;

builder.Logging.AddSentry(options => options.Dsn = builder.Configuration["Sentry:Dsn"]);

if(env.EnvironmentName != Environments.Development){
    builder.Configuration.AddSecretsManager(
    region: Amazon.RegionEndpoint.USEast1, 
    configurator: options => {
        options.PollingInterval = TimeSpan.FromMinutes(5);
        options.AcceptedSecretArns = new List<string>(){"Issuer", "Audience", "SecurityKey", "ConnectionStringDB"};
        options.KeyGenerator = (secret, name) => secret.Name == "ConnectionStringDB" ? $"ApiSettings:{secret.Name}" : $"JwtOptions:{secret.Name}";
    });
}

builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
builder.Services.AddScoped(c => c.GetService<IOptionsSnapshot<ApiSettings>>().Value);
builder.Services.AddScoped(c => c.GetService<IOptionsSnapshot<JwtOptions>>().Value);

var serviceProvider = builder.Services.BuildServiceProvider();
var apiSettings = serviceProvider.GetService<ApiSettings>();
var jwtOptions = serviceProvider.GetService<JwtOptions>();

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

builder.Services.AddStackExchangeRedisCache(redis => 
{
    redis.InstanceName = apiSettings?.Cache.InstanceName;
    redis.Configuration = apiSettings?.Cache.Configuration;
});

builder.Services.AddControllers()
                .AddJsonOptions(options => { options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;})
                .AddFluentValidation(options => { options.RegisterValidatorsFromAssemblyContaining<GetAnunciosQuery.Anuncios>();});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(builder.Configuration, jwtOptions);
builder.Services.AddHealthChecks();
builder.Services.AddTransient<ExceptionLoggingMiddleware>();

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

app.UseMiddleware<ExceptionLoggingMiddleware>();
app.UseSentryTracing();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseCors(builder => builder
            .SetIsOriginAllowed(orign => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/health");
});

app.Run();