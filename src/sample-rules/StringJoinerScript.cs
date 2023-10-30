// Copyright (c) Laserfiche.
// Licensed under the MIT License. See LICENSE in the project root for license information.
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
            const string RESULT_TOKEN_NAME = "result"; // Configure this token name as an Output in the script rule configuration.

            string joinedString = string.Join(" ", inputs.Values.Select(r => r.ToString())); // The result value.

            IDictionary<string, object> outputs = new Dictionary<string, object>
            {
                { RESULT_TOKEN_NAME, joinedString }
            };

            return Task.FromResult(outputs);
        }
    }
}
