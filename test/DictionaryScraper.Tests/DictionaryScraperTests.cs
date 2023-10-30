// Copyright (c) Laserfiche.
// Licensed under the MIT License. See LICENSE in the project root for license information.
namespace DictionaryScraper.Tests;

using DictionaryScraper.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// Provides unit tests for the <see cref="DictionaryScraper"/> class.
/// </summary>
[TestClass]
public class DictionaryScraperTests
{
    /// <summary>
    /// Tests that the result of calling <see cref="DictionaryScraper.GetDefinition(IDictionary{string, object})"/> contains the expected keys.
    /// </summary>
    [TestMethod]
    public async Task ResultsShouldHaveSpecificKeysGivenGetDefinitionCall()
    {
        var sut = new DictionaryScraper();
        var result = await sut.GetDefinition(new Dictionary<string, object> { ["Word"]="quixotic"});
        Assert.IsTrue(result.ContainsKey("Definitions"));
        Assert.IsTrue(result.ContainsKey("Error"));
        Assert.IsTrue(result.ContainsKey("ReceivedArgs"));
    }

    /// <summary>
    /// Tests that the result of calling <see cref="DictionaryScraper.GetDefinition(IDictionary{string, object})"/> contains the received arguments.
    /// </summary>
    [TestMethod]
    public async Task ResultsShouldEchoReceivedArguments()
    {
        var sut = new DictionaryScraper();
        var args = new Dictionary<string, object> { ["Word"] = "suplerflous" };
        var result = await sut.GetDefinition(args);
        result.TryGetValue("ReceivedArgs", out var receivedArgs);
        Assert.AreEqual(args, receivedArgs);
    }

    /// <summary>
    /// Tests that the result of calling <see cref="DictionaryScraper.GetDefinition(IDictionary{string, object})"/> for a valid word does not contain an error message.
    /// </summary>
    [TestMethod]
    public async Task SuccessfulParseShouldNotContainErrors()
    {
        var sut = new DictionaryScraper();
        var args = new Dictionary<string, object> { ["Word"] = "tangible" };
        var result = await sut.GetDefinition(args);
        result.TryGetValue("Error", out var receivedError);
        Assert.AreEqual(null, receivedError);
    }

    /// <summary>
    /// Tests that the result of calling <see cref="DictionaryScraper.GetDefinition(IDictionary{string, object})"/> for a valid word contains at least one definition.
    /// </summary>
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