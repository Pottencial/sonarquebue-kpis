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
    public class GetProjectsTest
    {

        [Fact]
        public void ShouldBeRetrieveProjects()
        {
            var logger = (ListLogger)LogFactory.CreateLogger(LoggerTypes.List);

            var projectRepository = ProjectsRepositoryConfig
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
                        .Build();

            GetProjects.Container = ContainerBuilder
                                    .Instance()
                                    .SetProjectsRepository(projectRepository)
                                    .SetAzureDevopsRepository(azureRepository)
                                    .Build();

            GetProjects.Run(null, logger).GetAwaiter().GetResult();

            var msg = logger.Logs.LastOrDefault();

            Assert.Contains("Projetos Capturados", msg);
        }

        [Fact]
        public void ShouldBeExceptionWhenRetrieveProjects()
        {
            var logger = (ListLogger)LogFactory.CreateLogger(LoggerTypes.List);

            var projectRepository = ProjectsRepositoryConfig
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
                                    .Build();

            GetProjects.Container = ContainerBuilder
                                    .Instance()
                                    .SetProjectsRepository(projectRepository)
                                    .SetAzureDevopsRepository(azureRepository)
                                    .Build();

            GetProjects.Run(null, logger).GetAwaiter().GetResult();

            Assert.Contains("Ocorreu erro na execucao", logger.Logs);
        }
    }
}
