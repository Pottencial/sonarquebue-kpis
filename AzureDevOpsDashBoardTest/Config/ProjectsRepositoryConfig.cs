using AzureDevOpsDashBoard.Dtos.AzureDevOps;
using AzureDevOpsDashBoard.Repositories;
using Moq;
using System;

namespace AzureDevOpsDashBoardTest.Config
{
    public class ProjectsRepositoryConfig
    {
        private static ProjectsRepositoryConfig _instance;
        private Mock<ProjectsRepository> _repository;

        private ProjectsRepositoryConfig()
        {
            if (_repository == null)
                _repository = new Mock<ProjectsRepository>(string.Empty);
        }

        public static ProjectsRepositoryConfig Instance()
        {
            if (_instance == null)
                _instance = new ProjectsRepositoryConfig();

            return _instance;
        }

        public ProjectsRepositoryConfig Save()
        {
            _repository.Setup(r => r.Save(It.IsAny<Project>()))
                .Returns(() => true);

            return this;
        }

        public ProjectsRepositoryConfig NotSaveWithException()
        {
            _repository.Setup(r => r.Save(It.IsAny<Project>()))
                .Throws(new Exception("Erro ao integrar no banco"));

            return this;
        }

        public ProjectsRepository Build()
        {
            return _repository.Object;
        }
    }
}
