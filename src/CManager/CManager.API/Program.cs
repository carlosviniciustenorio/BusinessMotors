var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;
builder.Configuration.SetBasePath(env.ContentRootPath)
                     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                     .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                     .AddEnvironmentVariables();

builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

CacheExtensions.AddCacheDependency(builder.Services);
builder.Services.AddStackExchangeRedisCache(redis =>
{
    redis.InstanceName = builder.Configuration.GetSection("ApiSettings:Cache:InstanceName").Value;
    redis.Configuration = builder.Configuration.GetSection("ApiSettings:Cache:Configuration").Value;
});

builder.Services.AddDbContext<IdentityDBContext>(options => options.UseSqlServer(builder.Configuration.GetSection("ConnectionString").Value));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(builder.Configuration);
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