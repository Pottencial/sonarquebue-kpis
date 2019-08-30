using Microsoft.Extensions.DependencyInjection;
using SonarQubeDashBoard.Repositories;

namespace SonarQubeDashBoard.Configuration
{
    public class CoreAppModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            const string personalToken = "94ca00342a0fc6ad2fb79b06b43fca0b5f478aa4";
            const string connectionString = "Data Source=127.0.0.1,1401;Initial Catalog=sql-db-general-codemetrics;User ID=SA;Password=Potte@2018";

            services.AddSingleton(provider => new SonarQubeRepository(personalToken));
            services.AddSingleton(provider => new ComponentRepository(connectionString));
            services.AddSingleton(provider => new MeasureRepository(connectionString));
        }
    }
}
