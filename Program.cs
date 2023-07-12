using System;
using System.Threading.Tasks;

class Program
{
    async static Task Main()
    {
        // Run tests
        await StringJoiner_Tests.StringJoiner_JoinTokens_Test();
    }
}