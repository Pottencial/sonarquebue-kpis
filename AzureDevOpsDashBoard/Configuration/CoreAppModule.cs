using AzureDevOpsDashBoard.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AzureDevOpsDashBoard.Configuration
{
    public class CoreAppModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            const string connectionString = "Data Source=127.0.0.1,1401;Initial Catalog=sql-db-general-codemetrics;User ID=SA;Password=Potte@2018";
            const string personalToken = "3wca4eqnqti7o7gtreuwftimdghlq6mb3ns4hvkxp7omztskby2a";

            services.AddSingleton(provider => new AzureDevOpsRepository(personalToken));
            services.AddSingleton(provider => new ProjectsRepository(connectionString));
            services.AddSingleton(provider => new BuildsRepository(connectionString));
            services.AddSingleton(provider => new RepositoriesRepository(connectionString));
        }
    }
}
