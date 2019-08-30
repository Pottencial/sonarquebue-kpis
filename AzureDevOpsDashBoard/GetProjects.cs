using System.Threading.Tasks;
using AzureDevOpsDashBoard.Configuration;
using AzureDevOpsDashBoard.Dtos.AzureDevOps;
using AzureDevOpsDashBoard.Repositories;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AzureDevOpsDashBoard
{
    public static class GetProjects
    {
        public static IServiceProvider Container = new ContainerBuilder()
                                                       .RegisterModule(new CoreAppModule())
                                                       .Build();

        [FunctionName("GetProjects")]
        public static async Task Run([TimerTrigger("* * */1 * * *")]TimerInfo myTimer, ILogger log)
        {
            try
            {
                const string organization = "pottencial";
                var azureDevOpsClient = Container.GetService<AzureDevOpsRepository>();
                var repo = Container.GetService<ProjectsRepository>();

                var result = await azureDevOpsClient.GetData<Response<Project>>($"https://dev.azure.com/{organization}/_apis/projects?stateFilter=All&api-version=5.0");

                foreach (var project in result.value)
                    repo.Save(project);

                log.LogInformation($"Projetos Capturados");
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Ocorreu erro na execucao");
            }
        }
    }
}
