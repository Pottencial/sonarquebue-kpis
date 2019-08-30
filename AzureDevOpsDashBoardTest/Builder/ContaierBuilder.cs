using AzureDevOpsDashBoard.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureDevOpsDashBoardTest.Builder
{
    public class ContainerBuilder 
    {
        private static ContainerBuilder _instance;
        private static Mock<IServiceProvider> container;

        private ContainerBuilder()
        {
            if (container == null)
                container = new Mock<IServiceProvider>();
        }

        public static ContainerBuilder Instance()
        {
            if (_instance == null)
                _instance = new ContainerBuilder();

            return _instance;
        }

        public ContainerBuilder SetProjectsRepository(ProjectsRepository repository)
        {
            container.Setup(p => p.GetService(typeof(ProjectsRepository))).Returns(repository);

            return this;
        }

        public ContainerBuilder SetRepositoriesRepository(RepositoriesRepository repository)
        {
            container.Setup(p => p.GetService(typeof(RepositoriesRepository))).Returns(repository);

            return this;
        }

        public ContainerBuilder SetBuildsRepository(BuildsRepository repository)
        {
            container.Setup(p => p.GetService(typeof(BuildsRepository))).Returns(repository);

            return this;
        }

        public ContainerBuilder SetAzureDevopsRepository(AzureDevOpsRepository repository)
        {
            container.Setup(p => p.GetService(typeof(AzureDevOpsRepository))).Returns(repository);

            return this;
        }

        public IServiceProvider Build()
        {
            return container.Object;
        }
    }
}
