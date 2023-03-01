using System.Reflection;
using CManager.Application.Commands;

namespace CManager.Infrastructure.Extensions
{
    public static class IoCExtensions
    {
        public static void AddIoC(this ServiceCollection services, IConfiguration configuration){
            services.AddMediatR(action => Assembly.GetAssembly(typeof(AddAnuncioCommand.Command)));
        }
    }
}