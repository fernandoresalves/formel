namespace Formel.API.Models
{
    public class PutFormula
    {
        public string Definition { get; set; }
        public IEnumerable<string> Parameters { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
    }
}
