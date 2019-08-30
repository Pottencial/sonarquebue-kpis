using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SonarQubeDashBoard.Configuration;
using SonarQubeDashBoard.Repositories;
using Microsoft.Extensions.DependencyInjection;
using SonarQubeDashBoard.Dtos;
using System.Threading.Tasks;
using System.Linq;

namespace SonarQubeDashBoard
{
    public static class GetProjects
    {
        public static IServiceProvider Container = new ContainerBuilder()
                                               .RegisterModule(new CoreAppModule())
                                               .Build();

        [FunctionName("GetProjects")]
        public static async Task RunAsync([TimerTrigger("*/10 * * * * *")]TimerInfo myTimer, ILogger log)
        {
            try
            {
                var sonarQubeClient = Container.GetService<SonarQubeRepository>();
                var componentRepo = Container.GetService<ComponentRepository>();
                var measureRepo = Container.GetService<MeasureRepository>();

                var result = await sonarQubeClient.GetData<Projects>($"http://sonar.pottencial.com.br:4040/api/components/search?qualifiers=TRK&ps=500");

                foreach (var project in result.Components)
                {
                    var measure = await sonarQubeClient.GetData<Measure>($"http://sonar.pottencial.com.br:4040/api/measures/component?componentKey={project.Key}&metricKeys=code_smells,bugs,vulnerabilities,coverage&additionalFields=periods,metrics");

                    var component = measure.Component;

                    //Project
                    componentRepo.Save(component);

                    foreach (var metric in component.Measures)
                    {
                        if (measure.Periods == null) continue;

                        var date = measure.Periods?.Last()?.Date ?? DateTime.UtcNow;
                        var description = measure.Metrics.FirstOrDefault(m => m.Key == metric.Metric);

                        measureRepo.Save(component.Id, metric.Metric, description?.Description, date, metric.Value);
                    }
                }

                log.LogInformation($"Projetos Capturados");
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Ocorreu erro na execucao");
            }
        }
    }
}
