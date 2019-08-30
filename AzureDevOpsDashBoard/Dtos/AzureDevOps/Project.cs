using System;

namespace AzureDevOpsDashBoard.Dtos.AzureDevOps
{
    public class Project {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }
}
