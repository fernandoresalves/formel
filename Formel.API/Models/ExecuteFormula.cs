namespace Formel.API.Models
{
    public class ExecuteFormula
    {
        public Guid Id { get; set; }
        public IDictionary<string, string> Parameters { get; set; }
    }
}
