namespace Formel.API.Models
{
    public class PutFormula
    {
        public Guid Id { get; set; }
        public string Definition { get; set; }
        public IDictionary<string, string> Parameters { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
    }
}
