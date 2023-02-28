using CManager.Infrastructure.Common;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;
builder.Configuration.SetBasePath(env.ContentRootPath)
                     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                     .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                     .AddEnvironmentVariables();

builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
builder.Services.AddScoped(c => c.GetService<IOptionsSnapshot<ApiSettings>>().Value);
builder.Services.AddScoped(c => c.GetService<IOptionsSnapshot<JwtOptions>>().Value);

var serviceProvider = builder.Services.BuildServiceProvider();
var apiSettings = serviceProvider.GetService<ApiSettings>();
var jwtOptions = serviceProvider.GetService<JwtOptions>();

CacheExtensions.AddCacheDependency(builder.Services);
builder.Services.AddStackExchangeRedisCache(redis =>
{
    redis.InstanceName = apiSettings.Cache.InstanceName;
    redis.Configuration = apiSettings.Cache.Configuration;
});

builder.Services.AddDbContext<IdentityDBContext>(options => options.UseSqlServer(builder.Configuration.GetSection("ConnectionString").Value));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(builder.Configuration, jwtOptions);
builder.Services.RegisterServices(builder.Configuration);
builder.Services.AddHealthChecks();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();
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