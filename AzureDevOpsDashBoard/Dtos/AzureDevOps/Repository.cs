using System;

namespace AzureDevOpsDashBoard.Dtos.AzureDevOps
{
    public class Repository {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Project Project { get; set; }
    }
}
