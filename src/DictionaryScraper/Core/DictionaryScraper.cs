namespace DictionaryScraper.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;
    using OpenScraping;
    using OpenScraping.Config;

    public class DictionaryScraper
    {
        private const string DictionaryDotComDefinitionConfig = @"
        {
            'definitions':
            {
                '_xpath': '//html//body//div[1]//div//div//div[2]//div//main//section//section//div[1]//section[2]//div//div',
                'definition': './/span[last()]'
            }
        }";

        private const string WordInputKey = "Word";

        public Task<IDictionary<string, object>> GetDefinition(IDictionary<string, object> args)
        {
            if (args.TryGetValue(WordInputKey, out var word))
            {
                this.TryScrapeWebDictionary(word.ToString(), out List<string> definitions, out string error);
                return Task.FromResult<IDictionary<string, object>>(this.GenerateResponse(definitions, error, args));
            }

            return Task.FromResult<IDictionary<string, object>>(this.GenerateResponse(new List<string>(), "Required parameter 'word' not found.", args));

        }

        private IDictionary<string, object> GenerateResponse(List<string> definitions, string error, IDictionary<string, object> args)
        {
            return new Dictionary<string, object>
                {
                    ["Error"] = error,
                    ["Definitions"] = definitions,
                    ["ReceivedArgs"] = args
                 };
        }

        private void TryScrapeWebDictionary(string word, out List<string> definitions, out string error)
        {
            try
            {
                definitions = this.ScrapeDefinitions(word);
                error = null;
            }
            catch (Exception ex)
            {
                definitions = new List<string>();
                error = ex.Message;
            }
        }

        private List<string> ScrapeDefinitions(string word)
        {
            List<string> definitions = new List<string>();
            var config = StructuredDataConfig.ParseJsonString(DictionaryDotComDefinitionConfig);
            var html = this.GetHTML($"https://www.dictionary.com/browse/{word}?s=t").GetAwaiter().GetResult();
            var extractor = new StructuredDataExtractor(config);
            var scrapingResults = extractor.Extract(html);
            foreach (var result in scrapingResults.SelectToken("definitions").Children())
            {
                try
                {
                    var definition = result.SelectToken("definition");
                    definitions.Add(definition.ToString());
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Error parsing definition: {result}");
                    Console.WriteLine($"Exception: {ex.Message}");
                }
            }
            return definitions;
        }

        private async Task<string> GetHTML(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
