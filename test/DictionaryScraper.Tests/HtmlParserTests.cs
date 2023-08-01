namespace DictionaryScraper.Tests;

using DictionaryScraper.Core;

/// <summary>
/// Provides unit tests for the <see cref="DictionaryParser"/> class.
/// </summary>
[TestClass]
public class HtmlParserTests
{
    /// <summary>
    /// Tests that the <see cref="DictionaryParser.Extract(string)"/> method can extract data from an HTML string.
    /// </summary>
    /// <param name="filename">The name of the file containing the HTML string to test.</param>
    [DataTestMethod]
    [DeploymentItem("Data//time.html")]
    [DeploymentItem("Data//tangible.html")]
    [DataRow("Data//time.html")]
    [DataRow("Data//tangible.html")]
    public void TestParser(string filename)
    {
        var html = File.ReadAllText(filename);
        Assert.IsNotNull(html);

        var parser = new DictionaryParser();
        var result = parser.Extract(html);
        
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Count > 0);
    }
}
