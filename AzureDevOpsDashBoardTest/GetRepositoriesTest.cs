using AzureDevOpsDashBoard;
using AzureDevOpsDashBoard.Dtos.AzureDevOps;
using AzureDevOpsDashBoardTest.Builder;
using AzureDevOpsDashBoardTest.Config;
using AzureDevOpsDashBoardTest.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AzureDevOpsDashBoardTest
{
    public class GetRepositoriesTest
    {

        [Fact]
        public void ShouldBeRetrieveRepositories()
        {
            var logger = (ListLogger)LogFactory.CreateLogger(LoggerTypes.List);

            var repository = RepositoriesRepositoryConfig
                                    .Instance()
                                    .Save()
                                    .Build();

            var azureRepository = AzureDevopsRepositoryConfig
                                    .Instance()
                                    .GetData(new Response<Project>
                                    {
                                        value = new List<Project> {
                                            new Project {
                                                Id = Guid.NewGuid(),
                                                Name = "Project Name",
                                                LastUpdateTime = DateTime.UtcNow,
                                                Description = "Project Test"
                                            }
                                        }
                                    })
                                    .GetData(new Response<Repository>
                                    {
                                        value = new List<Repository> {
                                            new Repository {
                                                Id = Guid.NewGuid(),
                                                Name = "Project Name",
                                                Project = new Project {
                                                    Id = Guid.NewGuid(),
                                                    Name = "Project Name",
                                                    LastUpdateTime = DateTime.UtcNow,
                                                    Description = "Project Test"
                                                }
                                            }
                                        }
                                    })
                                    .Build();


            GetRepositories.Container = ContainerBuilder
                                    .Instance()
                                    .SetRepositoriesRepository(repository)
                                    .SetAzureDevopsRepository(azureRepository)
                                    .Build();

            GetRepositories.Run(null, logger).GetAwaiter().GetResult();

            var msg = logger.Logs.LastOrDefault();

            Assert.Contains("Repositorios Capturados", msg);
        }

        [Fact]
        public void ShouldBeExceptionWhenRetrieveRepositories()
        {
            var logger = (ListLogger)LogFactory.CreateLogger(LoggerTypes.List);

            var repository = RepositoriesRepositoryConfig
                                    .Instance()
                                    .NotSaveWithException()
                                    .Build();
            var azureRepository = AzureDevopsRepositoryConfig
                                    .Instance()
                                    .GetData(new Response<Project>
                                    {
                                        value = new List<Project> {
                                                                    new Project {
                                                                        Id = Guid.NewGuid(),
                                                                        Name = "Project Name",
                                                                        LastUpdateTime = DateTime.UtcNow,
                                                                        Description = "Project Test"
                                                                    }
                                        }
                                    })
                                    .GetData(new Response<Repository>
                                    {
                                        value = new List<Repository> {
                                                                    new Repository {
                                                                        Id = Guid.NewGuid(),
                                                                        Name = "Project Name",
                                                                        Project = new Project {
                                                                            Id = Guid.NewGuid(),
                                                                            Name = "Project Name",
                                                                            LastUpdateTime = DateTime.UtcNow,
                                                                            Description = "Project Test"
                                                                        }
                                                                    }
                                        }
                                    })
                                    .Build();

            GetRepositories.Container = ContainerBuilder
                                    .Instance()
                                    .SetRepositoriesRepository(repository)
                                    .SetAzureDevopsRepository(azureRepository)
                                    .Build();

            GetRepositories.Run(null, logger).GetAwaiter().GetResult();

            Assert.Contains("Ocorreu erro na execucao", logger.Logs);
        }
    }
}
