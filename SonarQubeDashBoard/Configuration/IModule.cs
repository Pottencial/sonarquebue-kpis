using Microsoft.Extensions.DependencyInjection;

namespace SonarQubeDashBoard.Configuration
{
    public interface IModule
    {
        void Load(IServiceCollection services);
    }

    public class Module : IModule
    {
        public virtual void Load(IServiceCollection services)
        {
            return;
        }
    }
}
