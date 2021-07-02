
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
        [InlineData(@"\r hello there \r", @"\r")]
        [InlineData(@"this is \n my house",@"\n")]
        public void RemoveSpecialCharacter(string sentence,string character)
        {

            var result = sentence.SpecialTrim();
            Assert.True(!result.Contains(character));
        }

        //[Theory(DisplayName = "check produced querystrings")]
        //[MemberData(nameof(DictionaryData.GetDictionaryData), MemberType = typeof(DictionaryData))]
        //public void check_produced_querystrings(Dictionary<string, string> queryString, string correctOutput)
        //{
        //    var mock = new Mock<HttpClient>();
        //    BaseHttpRepository httpRepo = new BaseHttpRepository(mock.Object);

        //    var result = httpRepo.BuildQueryString(queryString);
        //    Assert.True(result.Equals(correctOutput));
        //}

      
        //public class DictionaryData
        //{

        //    public static IEnumerable<object[]> GetDictionaryData()
        //    {
        //        List<object[]> objs = new List<object[]>();
        //        objs.Add(new object[] { null, string.Empty });
        //        var dic=new Dictionary<string, string>();
        //        dic.Add("a", "2");
        //        dic.Add("b", "3");
        //        objs.Add(new object[] { dic, "?a=2&b=3" });
        //        return objs;
        //    }
           
        //}
    }
}
