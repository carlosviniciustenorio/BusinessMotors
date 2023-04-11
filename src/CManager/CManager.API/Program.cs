using CManager.API.Middlewares;
using CManager.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Sentry.Extensions.Logging.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;
builder.Configuration.AddEnvironmentVariables();

builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
builder.Services.AddScoped(c => c.GetService<IOptionsSnapshot<ApiSettings>>().Value);
builder.Services.AddScoped(c => c.GetService<IOptionsSnapshot<JwtOptions>>().Value);
var serviceProvider = builder.Services.BuildServiceProvider();
var apiSettings = serviceProvider.GetService<ApiSettings>();
var jwtOptions = serviceProvider.GetService<JwtOptions>();
Console.WriteLine($"apiSettings na Program: {JsonConvert.SerializeObject(apiSettings)}");
Console.WriteLine($"jwtOptions na Program: {JsonConvert.SerializeObject(jwtOptions)}");

builder.Services.AddDbContext<IdentityDBContext>(options => options.UseSqlServer(builder.Configuration.GetSection("ConnectionString").Value));
builder.Services.AddDbContext<CManagerDBContext>(options => options.UseSqlServer(builder.Configuration.GetSection("ConnectionString").Value));

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<IdentityDBContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Logging.AddSentry(builder.Configuration["Sentry:Dsn"]);

IoCExtensions.AddIoC(builder.Services, builder.Configuration);

builder.Services.AddStackExchangeRedisCache(redis => 
{
    redis.InstanceName = apiSettings.Cache.InstanceName;
    redis.Configuration = apiSettings.Cache.Configuration;
});

builder.Services.AddControllers().AddJsonOptions(options => {
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(builder.Configuration, jwtOptions);
builder.Services.RegisterDBServices(builder.Configuration);
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