using System.Reflection;
using CManager.Application.Commands;
using MediatR;

namespace CManager.Infrastructure.Extensions
{
    public static class IoCExtensions
    {
        public static IServiceCollection AddIoC(this IServiceCollection services, IConfiguration configursation)
        {
            services.AddMediatR(Assembly.GetAssembly(typeof(AddCaracteristicaCommand.CaracteristicaCommand)));

            #region Repositories
            services.AddSingleton<IAnuncioRepository, AnuncioRepository>();
            services.AddSingleton<ICaracteristicaRepository, CaracteristicaRepository>();
            services.AddSingleton<IOpcionalRepository, OpcionalRepository>();
            services.AddSingleton<ITipoCombustivelRepository, TipoCombustivelRepository>();
            #endregion

            return services;
        }
    }
}