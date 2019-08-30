using System.Collections.Generic;

namespace SonarQubeDashBoard.Dtos
{
    public class Component
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Qualifier { get; set; }
        public string Project { get; set; }
        public List<MeasureValue> Measures { get; set; }
    }
}
