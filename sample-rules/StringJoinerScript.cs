using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace laserfiche_sample_scripts
{
    // Rule script
    public class StringJoinerScript
    {
        // This is the rule script entry point and must have this signature.
        public Task<IDictionary<string, object>> JoinTokensAsync(IDictionary<string, object> inputs)
        {
            string joinedString = string.Join(" ", inputs.Values.Select(r => r.ToString()));

            IDictionary<string, object> outputs = new Dictionary<string, object>{
            {"result", joinedString}
        };

            return Task.FromResult(outputs);
        }
    }
}