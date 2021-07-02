using Parser.IntegrationTest.Infrastructure;
using System.Threading.Tasks;
using Xunit;
using Fynd.Parser.Domain;
using System.Net.Http;
using Fynd.Parser.Endpoint.Controller.Dto;
using Fynd.Parser.IntegrationTest.Artifacts;

namespace Parser.IntegrationTest
{
    public class DataExtractorControllerTest : BaseIntegrationTest
    {
        public DataExtractorControllerTest(
        TestWebApplicationFactory<Fynd.Parser.Endpoint.Startup> factory) : base(factory)
        {

        }



        [Theory(DisplayName = "Test_Http_valid_post")]
        [MemberData(nameof(SampleHtmlData.Get), MemberType = typeof(SampleHtmlData))]
        public async Task Test_Http_valid_post(string content)
        {

            StringContent httpContent = new StringContent( Newtonsoft.Json.JsonConvert.SerializeObject( new ParserInput { Html= content } ) , 
                System.Text.Encoding.UTF8, "application/json");

            var response= await HttpClient.PostAsync($"http://localhost:5001/api/DataExtractor/Extract", httpContent);
            var responseData = await response.Content.ReadAsStringAsync();
             var data=  Newtonsoft.Json.JsonConvert.DeserializeObject<ExportedInformation>(responseData);

           
            var expected = SampleHtmlData.GetMatchingDtoToProvidedHtml();


            Assert.False(HasDifferenceInValues(data, expected));
        }

     

    }
}
