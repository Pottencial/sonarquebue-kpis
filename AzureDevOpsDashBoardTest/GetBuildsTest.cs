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
    public class GetBuildsTest
    {

        [Fact]
        public void ShouldBeRetrieveBuilds()
        {
            var logger = (ListLogger)LogFactory.CreateLogger(LoggerTypes.List);

            var buildRepository = BuildsRepositoryConfig
                                    .Instance()
                                    .Save()
                                    .Build();

            var repository = RepositoriesRepositoryConfig
                                .Instance()
                                .List()
                                .Build();

            var azureRepository = AzureDevopsRepositoryConfig
                                    .Instance()
                                    .GetData(new Response<Build>
                                    {
                                        value = new List<Build> {
                                            new Build {
                                                Id = 1,
                                                Name = "Teste",
                                                Repository = new Repository {
                                                    Id = Guid.NewGuid(),
                                                    Name = "Repository Test"
                                                }
                                            }
                                        }
                                    })
                                    .GetData(new Build {
                                        Id = 1,
                                        Name = "Teste",
                                        Repository = new Repository
                                        {
                                            Id = Guid.NewGuid(),
                                            Name = "Repository Test"
                                        }
                                    })
                                    .Build();

            GetBuildsDefinition.Container = ContainerBuilder
                                    .Instance()
                                    .SetBuildsRepository(buildRepository)
                                    .SetRepositoriesRepository(repository)
                                    .SetAzureDevopsRepository(azureRepository)
                                    .Build();

            GetBuildsDefinition.Run(null, logger).GetAwaiter().GetResult();

            var msg = logger.Logs.LastOrDefault();

            Assert.Contains("Builds Capturados", msg);
        }

        [Fact]
        public void ShouldBeExceptionWhenSaveBuilds()
        {
            var logger = (ListLogger)LogFactory.CreateLogger(LoggerTypes.List);

            var buildRepository = BuildsRepositoryConfig
                                    .Instance()
                                    .NotSaveWithException()
                                    .Build();

            var azureRepository = AzureDevopsRepositoryConfig
                                    .Instance()
                                    .GetData(new Response<Build>
                                    {
                                        value = new List<Build> {
                                            new Build {
                                                Id = 1,
                                                Name = "Teste",
                                                Repository = new Repository {
                                                    Id = Guid.NewGuid(),
                                                    Name = "Repository Test"
                                                }
                                            }
                                        }
                                    })
                                    .GetData(new Build
                                    {
                                        Id = 1,
                                        Name = "Teste",
                                        Repository = new Repository
                                        {
                                            Id = Guid.NewGuid(),
                                            Name = "Repository Test"
                                        }
                                    })
                                    .Build();

            var repository = RepositoriesRepositoryConfig
                     .Instance()
                     .List()
                     .Build();

            GetBuildsDefinition.Container = ContainerBuilder
                                    .Instance()
                                    .SetBuildsRepository(buildRepository)
                                    .SetRepositoriesRepository(repository)
                                    .SetAzureDevopsRepository(azureRepository)
                                    .Build();

            GetBuildsDefinition.Run(null, logger).GetAwaiter().GetResult();

            Assert.Contains("Ocorreu erro na execucao", logger.Logs);
        }
    }
}
