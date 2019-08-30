using System;
using System.Threading.Tasks;
using AzureDevOpsDashBoard.Configuration;
using AzureDevOpsDashBoard.Dtos.AzureDevOps;
using AzureDevOpsDashBoard.Repositories;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace AzureDevOpsDashBoard
{
    public static class GetBuildsDefinition
    {
        public static IServiceProvider Container = new ContainerBuilder()
                                               .RegisterModule(new CoreAppModule())
                                               .Build();

        [FunctionName("GetBuildsDefinition")]
        public static async Task Run([TimerTrigger("* */2 * * * *")]TimerInfo myTimer, ILogger log)
        {
            try
            {
                const string organization = "pottencial";

                var azureDevOpsClient = Container.GetService<AzureDevOpsRepository>();
                var repoBuild = Container.GetService<BuildsRepository>();
                var repoRepo = Container.GetService<RepositoriesRepository>();

                var repositories = repoRepo.List();

                foreach (var repository in repositories)
                {
                    var builds = await azureDevOpsClient.GetData<Response<Build>>($"https://dev.azure.com/{organization}/{repository.Project.Id}/_apis/build/definitions/?repositoryType=TfsGit&repositoryId={repository.Id}&api-version=5.0");

                    foreach (var build in builds.value)
                    {
                        var buildItem = await azureDevOpsClient.GetData<Build>($"https://dev.azure.com/{organization}/{repository.Project.Id}/_apis/build/definitions/{build.Id}?api-version=5.0");

                        if (buildItem != null)
                            repoBuild.Save(buildItem);
                        else
                            log.LogError($"Não foi possível encontrar o Team Project: {repository.Project.Name} -> Repositorio: {repository.Name} -> Build: {build.Name}");
                    }
                }

                log.LogInformation("Builds Capturados");
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Ocorreu erro na execucao");
            }
        }
    }
}
