using AzureDevOpsDashBoard.Dtos.AzureDevOps;
using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;

namespace AzureDevOpsDashBoard.Repositories
{
    public class RepositoriesRepository
    {
        private readonly string _connectionString;
        public RepositoriesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public virtual bool Save(Repository repository)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var existRepository = db.ExecuteScalar<bool>("SELECT 1 FROM Repositories WHERE id = @id", new { repository.Id });

                if (!existRepository)
                {
                    var result = db.Execute("INSERT INTO Repositories VALUES (@id, @name, @projectId)", new { repository.Id, repository.Name, projectId = repository.Project.Id });
                    existRepository = result > 0;
                }

                return existRepository;
            }
        }

        public virtual IEnumerable<Repository> List()
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var repositories = db.Query<Repository, Project, Repository>(@" SELECT R.ID, R.NAME, P.ID, P.NAME
                                                                  FROM REPOSITORIES R INNER JOIN PROJECTS P
                                                                    ON R.PROJECTID = P.ID",
                                                                    splitOn: "ID", 
                                                                    map: (repository, project) => { repository.Project = project; return repository; });
                return repositories;
            }
        }
    }
}
