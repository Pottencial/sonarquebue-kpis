using AzureDevOpsDashBoard.Dtos.AzureDevOps;
using AzureDevOpsDashBoard.Repositories;
using Moq;
using System;

namespace AzureDevOpsDashBoardTest.Config
{
    public class BuildsRepositoryConfig
    {
        private static BuildsRepositoryConfig _instance;
        private Mock<BuildsRepository> _repository;

        private BuildsRepositoryConfig()
        {
            if (_repository == null)
                _repository = new Mock<BuildsRepository>(string.Empty);
        }

        public static BuildsRepositoryConfig Instance()
        {
            if (_instance == null)
                _instance = new BuildsRepositoryConfig();

            return _instance;
        }

        public BuildsRepositoryConfig Save()
        {
            _repository.Setup(r => r.Save(It.IsAny<Build>()))
                .Returns(() => true);

            return this;
        }

        public BuildsRepositoryConfig NotSaveWithException()
        {
            _repository.Setup(r => r.Save(It.IsAny<Build>()))
                .Throws(new Exception("Erro ao integrar no banco"));

            return this;
        }

        public BuildsRepository Build()
        {
            return _repository.Object;
        }
    }
}
