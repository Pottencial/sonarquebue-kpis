using System;
using System.Threading.Tasks;
using AzureDevOpsDashBoard.Dtos.AzureDevOps;
using AzureDevOpsDashBoard.Repositories;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using AzureDevOpsDashBoard.Configuration;

namespace AzureDevOpsDashBoard
{
    public static class GetRepositories
    {
        public static IServiceProvider Container = new ContainerBuilder()
                                       .RegisterModule(new CoreAppModule())
                                       .Build();

        [FunctionName("GetRepositories")]
        public static async Task Run([TimerTrigger("* */6 * * * *")]TimerInfo myTimer, ILogger log)
        {
            try
            {
                const string organization = "pottencial";
                var azureDevOpsClient = Container.GetService<AzureDevOpsRepository>();
                var repoRepo = Container.GetService<RepositoriesRepository>();

                var result = await azureDevOpsClient.GetData<Response<Project>>($"https://dev.azure.com/{organization}/_apis/projects?stateFilter=All&api-version=5.0");

                foreach (var teamProject in result.value)
                {
                    var resultRepositories = await azureDevOpsClient.GetData<Response<Repository>>($"https://dev.azure.com/pottencial/{teamProject.Id}/_apis/git/repositories?api-version=5.0");

                    foreach (var repository in resultRepositories.value)
                        repoRepo.Save(repository);
                }

                log.LogInformation("Repositorios Capturados");
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Ocorreu erro na execucao");
            }
        }
    }
}
