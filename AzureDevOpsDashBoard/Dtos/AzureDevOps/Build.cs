using System;

namespace AzureDevOpsDashBoard.Dtos.AzureDevOps
{
    public class Build {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public Repository Repository { get; set; }
    }
}
