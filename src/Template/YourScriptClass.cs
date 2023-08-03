namespace Your.Namespace
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    /// <summary>
    /// The class name is used in the "Class name" property in the rule designer page
    /// If you use this dll directly, the "Class name" property in the designer page should be "Your.Namespace.YourScriptClass"
    /// </summary>
    public class YourScriptClass
    {
        internal const string InputParameterName = "InputParameterName";
        
        internal const string OutputParameterName = "OutputParameterName";

        /// <summary>
        /// Ensure your method uses the same signature as used below.
        /// The name of the method that you will call is used in the "Method name" property in the rule designer page 
        /// </summary>
        public Task<IDictionary<string, object>> YourScriptMethod(IDictionary<string, object> arguments)
        {
            // Retrieve your input parameters from the names provided under "Input" in the rule designer page.
            var inputParameter = arguments[InputParameterName];

            // Other tasks for your script can be performed here
            // ...

            // Return the information back to your script rule.
            return Task.FromResult<IDictionary<string, object>>(
                new Dictionary<string, object>
                {
                    // The dictionary key should match the name used under "Output" in the rule designer page.
                    [OutputParameterName] = inputParameter
                });
        }
    }
}
