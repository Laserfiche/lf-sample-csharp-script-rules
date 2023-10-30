// Copyright (c) Laserfiche.
// Licensed under the MIT License. See LICENSE in the project root for license information.
namespace Template.Tests;

using Your.Namespace;
[TestClass]
public class TemplateTests
{
    [DataTestMethod]
    [DataRow("aaa")]
    [DataRow("bbb")]
    [DataRow("ccc")]
    public async Task TestEntryMethod(string input)
    {
        var invoker = new YourScriptClass();
        var arguments = new Dictionary<string, object>
        {
            [YourScriptClass.InputParameterName] = input
        };

        var outputs = await invoker.YourScriptMethod(arguments).ConfigureAwait(false);
        Assert.AreEqual(input, outputs[YourScriptClass.OutputParameterName]);
    }
}
