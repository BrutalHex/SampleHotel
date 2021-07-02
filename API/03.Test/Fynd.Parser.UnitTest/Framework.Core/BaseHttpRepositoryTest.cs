using Fynd.Framework.Core.Domain.Communication;
using Moq;
using System.Collections.Generic;
using System.Net.Http;
using Xunit;

namespace Fynd.Parser.UnitTest.Framework.Core
{
    public class BaseHttpRepositoryTest
    {
      


        [Fact(DisplayName = "Is HandleJsonSettings returns settings")]
        public void Is_HandleJsonSettings_returns_settings()
        {
            var mock = new Mock<HttpClient>();
            BaseHttpRepository httpRepo = new BaseHttpRepository(mock.Object);

        
            var result= httpRepo.HandleJsonSettings(null);
            Assert.True(result != null);

            var test2= httpRepo.HandleJsonSettings(new Newtonsoft.Json.JsonSerializerSettings { NullValueHandling=Newtonsoft.Json.NullValueHandling.Include});
            Assert.True(test2.NullValueHandling== Newtonsoft.Json.NullValueHandling.Include);
        }

        [Theory(DisplayName = "check produced querystrings")]
        [MemberData(nameof(DictionaryData.GetDictionaryData), MemberType = typeof(DictionaryData))]
        public void check_produced_querystrings(Dictionary<string, string> queryString, string correctOutput)
        {
            var mock = new Mock<HttpClient>();
            BaseHttpRepository httpRepo = new BaseHttpRepository(mock.Object);

            var result = httpRepo.BuildQueryString(queryString);
            Assert.True(result.Equals(correctOutput));
        }

      
        public class DictionaryData
        {

            public static IEnumerable<object[]> GetDictionaryData()
            {
                List<object[]> objs = new List<object[]>();
                objs.Add(new object[] { null, string.Empty });
                var dic=new Dictionary<string, string>();
                dic.Add("a", "2");
                dic.Add("b", "3");
                objs.Add(new object[] { dic, "?a=2&b=3" });
                return objs;
            }
           
        }
    }
}
