using System.Reflection;
using BusinessMotors.Application.Commands;
using MediatR;

namespace BusinessMotors.Infrastructure.Extensions
{
    public static class IoCExtensions
    {
        public static IServiceCollection AddIoC(this IServiceCollection services)
        {
            CacheExtensions.AddCacheDependency(services);
            
            services.AddMediatR(Assembly.GetAssembly(typeof(AddCaracteristicaCommand.CaracteristicaCommand)));

            #region Repositories
            services.AddScoped<IAnuncioRepository, AnuncioRepository>();
            services.AddScoped<ICaracteristicaRepository, CaracteristicaRepository>();
            services.AddScoped<IOpcionalRepository, OpcionalRepository>();
            services.AddScoped<ITipoCombustivelRepository, TipoCombustivelRepository>();
            services.AddScoped<IMarcaRepository, MarcaRepository>();
            services.AddScoped<IModeloRepository, ModeloRepository>();
            services.AddScoped<IVersaoRepository, VersaoRepository>();
            #endregion

            return services;
        }
    }
}