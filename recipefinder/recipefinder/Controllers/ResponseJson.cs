namespace recipefinder.Controllers
{
    public class ResponseJson
    {
        public string filename { get; set; }
        public double[] probs { get; set; }
        public string[] labels { get; set; }
        public string visualizations { get; set; }
        public string attributions { get; set; }
    }
}