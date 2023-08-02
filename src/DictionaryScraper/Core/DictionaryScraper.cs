namespace DictionaryScraper.Core
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// Provides a way to scrape definitions from an online dictionary.
    /// </summary>
    public class DictionaryScraper
    {
        private const string WordInputKey = "Word";
        
        private const string DictionaryDotComUrl = "https://www.dictionary.com/browse/{0}?s=t";
        
        private static HttpClient Client { get; } = new HttpClient();

        /// <summary>
        /// Gets the definition for the specified word.
        /// </summary>
        /// <param name="args">A dictionary of input arguments.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        /// The task result contains a dictionary with the following keys: 
        ///     "Error" (a string containing an error message, if any), 
        ///     "Definitions" (a list of strings containing the definitions for the specified word), 
        ///     "ReceivedArgs" (a dictionary containing the input arguments).
        /// </returns>
        public Task<IDictionary<string, object>> GetDefinition(IDictionary<string, object> args)
        {
            if (args.TryGetValue(WordInputKey, out var word))
            {
                TryScrapeWebDictionary(word.ToString(), out var definitions, out var error);
                return Task.FromResult(GenerateResponse(definitions, error, args));
            }

            return Task.FromResult(GenerateResponse(new List<string>(), "Required parameter 'word' not found.", args));
        }

        /// <summary>
        /// Scrapes the web dictionary for the specified word.
        /// </summary>
        /// <param name="word">The word to scrape the web dictionary for.</param>
        /// <returns>A list of strings containing the definitions for the specified word.</returns>
        private static List<string> ScrapeDefinitions(string word)
        {
            var definitions = new List<string>();
            var html = GetHTML(string.Format(DictionaryDotComUrl, word)).GetAwaiter().GetResult();
            var scrapingResults = new DictionaryParser().Extract(html);
            
            foreach (var result in scrapingResults.SelectToken("definitions").Children())
            {
                try
                {
                    // var definition = result.SelectToken("definition");
                    definitions.Add(result.ToString());
                }
                catch(Exception ex)
                {
                    Console.Error.WriteLine($"Error parsing definition: {result}");
                    Console.Error.WriteLine($"Exception: {ex.Message}");
                }
            }
            
            return definitions;
        }

        /// <summary>
        /// Sends an HTTP GET request to the specified URL and returns the response body as a string.
        /// </summary>
        /// <param name="url">The URL to send the HTTP GET request to.</param>
        /// <returns>The response body as a string.</returns>
        private static async Task<string> GetHTML(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url));
            var response = await Client.SendAsync(request).ConfigureAwait(false);
            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Generates a dictionary with the specified error message, definitions, and input arguments.
        /// </summary>
        /// <param name="definitions">A list of strings containing the definitions for the specified word.</param>
        /// <param name="error">A string containing an error message, if any.</param>
        /// <param name="args">A dictionary containing the input arguments.</param>
        /// <returns>A dictionary with the specified error message, definitions, and input arguments.</returns>
        private static IDictionary<string, object> GenerateResponse(IReadOnlyCollection<string> definitions, string error, IDictionary<string, object> args)
        {
            return new Dictionary<string, object>
                {
                    ["Error"] = error,
                    ["Definitions"] = definitions,
                    ["ReceivedArgs"] = args
                 };
        }

        /// <summary>
        /// Attempts to scrape the web dictionary for the specified word.
        /// </summary>
        /// <param name="word">The word to scrape the web dictionary for.</param>
        /// <param name="definitions">A list of strings containing the definitions for the specified word.</param>
        /// <param name="error">A string containing an error message, if any.</param>
        private static void TryScrapeWebDictionary(string word, out List<string> definitions, out string error)
        {
            try
            {
                definitions = ScrapeDefinitions(word);
                error = null;
            }
            catch (Exception ex)
            {
                definitions = new List<string>();
                error = ex.Message;
            }
        }
    }
}
