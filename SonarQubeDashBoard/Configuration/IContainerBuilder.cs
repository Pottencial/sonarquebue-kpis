using Microsoft.Extensions.DependencyInjection;
using System;

namespace SonarQubeDashBoard.Configuration
{
    public interface IContainerBuilder
    {
        IContainerBuilder RegisterModule(IModule module = null);

        IServiceProvider Build();
    }


    public class ContainerBuilder : IContainerBuilder
    {
        private readonly IServiceCollection _services;
        public ContainerBuilder()
        {
            _services = new ServiceCollection();
        }

        public IContainerBuilder RegisterModule(IModule module = null)
        {
            if (module == null)
                module = new Module();

            module.Load(_services);

            return this;
        }

        public IServiceProvider Build()
        {
            var provider = _services.BuildServiceProvider();

            return provider;
        }
    }
}
