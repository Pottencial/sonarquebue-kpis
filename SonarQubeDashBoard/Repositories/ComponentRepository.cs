using Dapper;
using SonarQubeDashBoard.Dtos;
using System.Data.SqlClient;

namespace SonarQubeDashBoard.Repositories
{
    public class ComponentRepository
    {
        private readonly string _connectionString;
        public ComponentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public virtual bool Save(Component component)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var existRepository = db.ExecuteScalar<bool>("SELECT 1 FROM Components WHERE id = @id", new { component.Id });

                if (!existRepository)
                {
                    var result = db.Execute("INSERT INTO Components VALUES (@id, @key, @name)", new { component.Id, component.Key, component.Name });
                    existRepository = result > 0;
                }

                return existRepository;
            }
        }
    }
}
