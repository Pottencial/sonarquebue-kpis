using AzureDevOpsDashBoard.Dtos.AzureDevOps;
using System.Data.SqlClient;
using Dapper;

namespace AzureDevOpsDashBoard.Repositories
{
    public class ProjectsRepository
    {
        private readonly string _connectionString;
        public ProjectsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public virtual bool Save(Project project)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var existProject = db.ExecuteScalar<bool>("SELECT 1 FROM Projects WHERE id = @id", new { project.Id });

                if (!existProject)
                {
                    var result = db.Execute("INSERT INTO Projects VALUES (@id, @name, @lastUpdateTime)", project);
                    existProject = result > 0;
                }

                return existProject;
            }
        }
    }
}
