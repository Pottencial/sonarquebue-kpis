using Dapper;
using System;
using System.Data.SqlClient;

namespace SonarQubeDashBoard.Repositories
{
    public class MeasureRepository
    {
        private readonly string _connectionString;
        public MeasureRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public virtual bool Save(string componentId, string metricKey, string metricName, DateTime date, double value)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var existMeasure = db.ExecuteScalar<bool>("SELECT 1 FROM Measures WHERE componentId = @componentId AND metricKey = @metricKey and date = @date", new { componentId, metricKey, date });

                if (!existMeasure)
                {
                    var result = db.Execute("INSERT INTO Measures VALUES (@componentId, @metricKey, @date, @metricName, @value)", new { componentId, metricKey, date, metricName, value });
                    existMeasure = result > 0;
                }

                return existMeasure;
            }
        }
    }
}
