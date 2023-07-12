using System;
using System.Collections.Generic;
using System.Threading.Tasks;

class StringJoiner_Tests
{
    public async static Task StringJoiner_JoinTokens_Test()
    {
        // Arrange
        IDictionary<string, object> inputs = new Dictionary<string, object>{
            {"str","Hello"},
            {"num",42},
            {"bool",true},
        };

        // Act
        IDictionary<string, object> outputs = await (new StringJoiner()).JoinTokensAsync(inputs);

        //Assert
        string actualResult = outputs["result"] as string;
        string expectedResult = "Hello 42 True";
        string testResult = actualResult == expectedResult ? "succeeded" : "failed";
        Console.WriteLine($"Test {testResult}. {nameof(StringJoiner.JoinTokensAsync)} expected result: '{expectedResult}', actual: '{actualResult}'");
    }
}