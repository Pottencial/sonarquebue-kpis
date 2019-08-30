namespace SonarQubeDashBoard.Dtos
{
    public class MeasureValue
    {
        public string Metric { get; set; }
        public double Value { get; set; }
        public bool BestValue { get; set; }
    }
}
