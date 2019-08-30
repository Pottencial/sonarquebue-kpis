using System.Collections.Generic;

namespace SonarQubeDashBoard.Dtos
{
    public class Projects
    {
        public Paging Paging { get; set; }

        public List<Component> Components { get; set; }
    }
}
