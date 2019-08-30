using AzureDevOpsDashBoard.Dtos.AzureDevOps;
using System.Data.SqlClient;
using Dapper;

namespace AzureDevOpsDashBoard.Repositories
{
    public class BuildsRepository
    {
        private readonly string _connectionString;
        public BuildsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public virtual bool Save(Build build)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var existBuild = db.ExecuteScalar<bool>("SELECT 1 FROM Builds WHERE id = @id", new { build.Id });

                if (!existBuild)
                {
                    var result = db.Execute("INSERT INTO Builds VALUES (@id, @name, @createdDate, @repositoryId)", new { build.Id, build.Name, build.CreatedDate, repositoryId = build.Repository.Id});
                    existBuild = result > 0;
                }

                return existBuild;
            }
        }
    }
}
