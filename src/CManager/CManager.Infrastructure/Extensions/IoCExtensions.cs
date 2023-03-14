using System.Reflection;
using CManager.Application.Commands;
using MediatR;

namespace CManager.Infrastructure.Extensions
{
    public static class IoCExtensions
    {
        public static IServiceCollection AddIoC(this IServiceCollection services, IConfiguration configursation)
        {
            CacheExtensions.AddCacheDependency(services);
            ServicesExtensions.RegisterDBServices(services, configursation);
            
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