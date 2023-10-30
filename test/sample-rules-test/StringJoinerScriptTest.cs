// Copyright (c) Laserfiche.
// Licensed under the MIT License. See LICENSE in the project root for license information.
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace laserfiche_sample_scripts
{

    [TestClass]
    public class StringJoinerScriptTest
    {

        [TestMethod]
        public async Task StringJoiner_JoinTokens_Test()
        {
            // Arrange
            IDictionary<string, object> inputs = new Dictionary<string, object>{
                {"str","Hello"},
                {"num",42},
                {"bool",true},
            };

            // Act
            IDictionary<string, object> outputs = await (new StringJoinerScript()).JoinTokensAsync(inputs);

            // Assert
            string actualResult = outputs["result"] as string;
            string expectedResult = "Hello 42 True";
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}