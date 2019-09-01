using AzureDevOpsDashBoard.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AzureDevOpsDashBoard.Configuration
{
    public class CoreAppModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            const string connectionString = "Data Source=127.0.0.1,1401;Initial Catalog=<DATABASE_NAME>;User ID=SA;Password=<PASSWORD>";
            const string personalToken = "<AZURE_TOKEN>";

            services.AddSingleton(provider => new AzureDevOpsRepository(personalToken));
            services.AddSingleton(provider => new ProjectsRepository(connectionString));
            services.AddSingleton(provider => new BuildsRepository(connectionString));
            services.AddSingleton(provider => new RepositoriesRepository(connectionString));
        }
    }
}
