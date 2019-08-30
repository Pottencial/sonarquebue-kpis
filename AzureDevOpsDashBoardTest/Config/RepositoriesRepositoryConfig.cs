using AzureDevOpsDashBoard.Dtos.AzureDevOps;
using AzureDevOpsDashBoard.Repositories;
using Moq;
using System;
using System.Collections.Generic;

namespace AzureDevOpsDashBoardTest.Config
{
    public class RepositoriesRepositoryConfig
    {
        private static RepositoriesRepositoryConfig _instance;
        private Mock<RepositoriesRepository> _repository;

        private RepositoriesRepositoryConfig()
        {
            if (_repository == null)
                _repository = new Mock<RepositoriesRepository>(string.Empty);
        }

        public static RepositoriesRepositoryConfig Instance()
        {
            if (_instance == null)
                _instance = new RepositoriesRepositoryConfig();

            return _instance;
        }

        public RepositoriesRepositoryConfig Save()
        {
            _repository.Setup(r => r.Save(It.IsAny<Repository>()))
                .Returns(() => true);

            return this;
        }

        public RepositoriesRepositoryConfig NotSaveWithException()
        {
            _repository.Setup(r => r.Save(It.IsAny<Repository>()))
                .Throws(new Exception("Erro ao integrar no banco"));

            return this;
        }

        public RepositoriesRepositoryConfig List()
        {
            _repository.Setup(r => r.List())
                .Returns(() => new List<Repository> {
                    new Repository{
                        Id = Guid.NewGuid(),
                        Name = "Repositorio A",
                        Project = new Project {
                            Id = Guid.NewGuid(),
                            Name = "Project A",
                            Description = "oK",
                            LastUpdateTime = DateTime.UtcNow
                        }
                    }
                });

            return this;
        }

        public RepositoriesRepositoryConfig NotList()
        {
            _repository.Setup(r => r.List())
                .Returns(() => null);

            return this;
        }

        public RepositoriesRepositoryConfig NotListWithException()
        {
            _repository.Setup(r => r.List())
                .Throws(new Exception("Erro ao integrar no banco"));

            return this;
        }

        public RepositoriesRepository Build()
        {
            return _repository.Object;
        }
    }
}
