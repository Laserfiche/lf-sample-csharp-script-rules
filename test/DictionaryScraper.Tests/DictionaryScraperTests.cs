namespace DictionaryScraper.Tests;

using DictionaryScraper.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

[TestClass]
public class DictionaryScraperTests
{
    [TestMethod]
    public async Task ResultsShouldHaveSpecificKeysGivenGetDefinitionCall()
    {
        var sut = new DictionaryScraper();
        var result = await sut.GetDefinition(new Dictionary<string, object> { ["Word"]="quixotic"});
        Assert.IsTrue(result.ContainsKey("Definitions"));
        Assert.IsTrue(result.ContainsKey("Error"));
        Assert.IsTrue(result.ContainsKey("ReceivedArgs"));
    }

    [TestMethod]
    public async Task ResultsShouldEchoReceivedArguments()
    {
        var sut = new DictionaryScraper();
        var args = new Dictionary<string, object> { ["Word"] = "suplerflous" };
        var result = await sut.GetDefinition(args);
        result.TryGetValue("ReceivedArgs", out var receivedArgs);
        Assert.AreEqual(args, receivedArgs);
    }

    [TestMethod]
    public async Task SuccessfulParseShouldNotContainErrors()
    {
        var sut = new DictionaryScraper();
        var args = new Dictionary<string, object> { ["Word"] = "tangible" };
        var result = await sut.GetDefinition(args);
        result.TryGetValue("Error", out var receivedError);
        Assert.AreEqual(null, receivedError);
    }

    [TestMethod]
    public async Task SuccessfulParseShouldContainDefinitions()
    {
        var sut = new DictionaryScraper();
        var args = new Dictionary<string, object> { ["Word"] = "time" };
        var result = await sut.GetDefinition(args);
        result.TryGetValue("Definitions", out var receivedDefs);
        var definitionList = (List<string>)receivedDefs;
        Assert.IsNotNull(receivedDefs);
        Assert.IsTrue(definitionList.Count > 0);
    }
}