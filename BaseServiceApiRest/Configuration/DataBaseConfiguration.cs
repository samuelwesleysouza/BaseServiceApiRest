using BaseServiceRestApi_Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BaseServiceApiRest.Configuration
{
    public static class DataBaseConfiguration
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // Certifique-se de usar a chave correta da string de conexão (DefaultConnection)
            services.AddDbContext<BaseServiceRestApiContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
