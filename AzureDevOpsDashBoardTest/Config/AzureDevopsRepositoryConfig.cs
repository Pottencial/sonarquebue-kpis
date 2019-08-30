using AzureDevOpsDashBoard.Dtos.AzureDevOps;
using AzureDevOpsDashBoard.Repositories;
using Moq;
using System;

namespace AzureDevOpsDashBoardTest.Config
{
    public class AzureDevopsRepositoryConfig
    {
        private static AzureDevopsRepositoryConfig _instance;
        private Mock<AzureDevOpsRepository> _repository;

        private AzureDevopsRepositoryConfig()
        {
            if (_repository == null)
                _repository = new Mock<AzureDevOpsRepository>(string.Empty);
        }

        public static AzureDevopsRepositoryConfig Instance()
        {
            if (_instance == null)
                _instance = new AzureDevopsRepositoryConfig();

            return _instance;
        }

        public AzureDevopsRepositoryConfig GetData<T>(T resultData)
        {
            _repository.Setup(r => r.GetData<T>(It.IsAny<string>()))
                .ReturnsAsync(() => resultData);

            return this;
        }

        public AzureDevOpsRepository Build()
        {
            return _repository.Object;
        }
    }
}
