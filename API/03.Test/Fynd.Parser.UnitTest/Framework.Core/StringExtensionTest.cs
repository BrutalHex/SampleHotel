
using Fynd.Framework.Core.Extenions;
using Moq;
using System.Collections.Generic;
using System.Net.Http;
using Xunit;

namespace Fynd.Parser.UnitTest.Framework.Core
{
    public class StringExtensionTest
    {
      


        [Theory(DisplayName = "tests removal of special characters")]
        [InlineData("\r hello there \r", "\r")]
        [InlineData("this is \n my house","\n")]
        public void RemoveSpecialCharacter(string sentence,string character)
        {

            var result = sentence.SpecialTrim();
            Assert.True(!result.Contains(character));
        }


    }
}
