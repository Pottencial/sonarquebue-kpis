using Microsoft.Extensions.DependencyInjection;

namespace AzureDevOpsDashBoard.Configuration
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
