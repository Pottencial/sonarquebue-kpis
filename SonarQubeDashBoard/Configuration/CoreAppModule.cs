using Microsoft.Extensions.DependencyInjection;
using SonarQubeDashBoard.Repositories;

namespace SonarQubeDashBoard.Configuration
{
    public class CoreAppModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            const string personalToken = "<SONAR_TOKEN>";
            const string connectionString = "Data Source=127.0.0.1,1401;Initial Catalog=<DATABASE_NAME>;User ID=SA;Password=<PASSWORD>";

            services.AddSingleton(provider => new SonarQubeRepository(personalToken));
            services.AddSingleton(provider => new ComponentRepository(connectionString));
            services.AddSingleton(provider => new MeasureRepository(connectionString));
        }
    }
}
