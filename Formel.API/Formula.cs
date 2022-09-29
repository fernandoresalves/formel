using AngouriMath;

namespace Formel.API
{
    public class Formula
    {
        public Guid Id { get; set; }
        public string Definition { get; set; }
        public string Parameters { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }

        public static decimal Run(string definition, IDictionary<string, string> parameters)
        {
             string definitionRefined = string.Empty;

            foreach (KeyValuePair<string, string> parameter in parameters)
            {
                definitionRefined = definition.Replace(parameter.Key, parameter.Value);
                definition = definitionRefined;
            }

            Entity function = definitionRefined;

            return (decimal)function.EvalNumerical();
        }
    }
}
