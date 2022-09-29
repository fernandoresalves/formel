namespace Formel.API.Models
{
    public class ResponseFormulaDetails
    {
        public Guid Id { get; set; }
        public string Definition { get; set; }
        public IEnumerable<string> Parameters { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
    }
}
