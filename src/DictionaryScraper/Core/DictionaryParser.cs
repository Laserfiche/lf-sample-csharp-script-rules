namespace DictionaryScraper.Core
{
    using Newtonsoft.Json.Linq;
    using OpenScraping;
    using OpenScraping.Config;
    
    /// <summary>
    /// Provides a way to extract definitions from a web page using a specific configuration.
    /// </summary>
    public class DictionaryParser
    {
        private const string DictionaryDotComDefinitionConfig2023 = @"
        {
            'definitions':
            {
                '_xpath': '//html//body//div//div//main//div//section//section[@id=""top-definitions""]//div//section[1]//div[1]//div[1]//ol//li[1]//div',
                'definition': './/p'
            }
        }";

        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryParser"/> class.
        /// </summary>
        public DictionaryParser()
        {
            var config = StructuredDataConfig.ParseJsonString(DictionaryDotComDefinitionConfig2023);
            this.Extractor = new StructuredDataExtractor(config);
        }
        
        /// <summary>
        /// Gets the structured data extractor used by this instance of the <see cref="DictionaryParser"/> class.
        /// </summary>
        private StructuredDataExtractor Extractor { get; }
        
        /// <summary>
        /// Extracts structured data from the specified HTML string using the configuration specified in the constructor.
        /// </summary>
        /// <param name="html">The HTML string to extract data from.</param>
        /// <returns>A <see cref="JContainer"/> object containing the extracted data.</returns>
        public JContainer Extract(string html)
        {
            return this.Extractor.Extract(html);
        }
    }
}
