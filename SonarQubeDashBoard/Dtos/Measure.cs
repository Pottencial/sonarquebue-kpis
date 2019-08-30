using System;
using System.Collections.Generic;

namespace SonarQubeDashBoard.Dtos
{
    public class Measure
    {
        public Component Component { get; set; }
        public List<Metric> Metrics { get; set; }
        public List<Period> Periods { get; set; }
    }

    public class Metric
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class Period
    {
        public int Index { get; set; }
        public string Mode { get; set; }
        public DateTime Date { get; set; }
    }
}
