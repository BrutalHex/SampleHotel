using Parser.IntegrationTest.Infrastructure;
using Fynd.Parser.ApplicationContract.DTO;
using System.Threading.Tasks;
using Xunit;
using Fynd.Parser.Domain;
using System.Net.Http;
using Fynd.Parser.Endpoint.Controller.Dto;

namespace Parser.IntegrationTest
{
    public class DataExtractorControllerTest : BaseIntegrationTest
    {
        public DataExtractorControllerTest(
        TestWebApplicationFactory<Fynd.Parser.Endpoint.Startup> factory) : base(factory)
        {

        }


        [Fact(DisplayName ="")]
        public async Task Test_Http_valid_post()
        {

            string content=string.Empty;



            StringContent httpContent = new StringContent( Newtonsoft.Json.JsonConvert.SerializeObject( new ParserInput { Html= content } ) , 
                System.Text.Encoding.UTF8, "application/json");

            var response= await HttpClient.PostAsync($"http://localhost:5001/api/DataExtractor/Extract", httpContent);
            var responseData = await response.Content.ReadAsStringAsync();
             var data=  Newtonsoft.Json.JsonConvert.DeserializeObject<ExportedInformation>(responseData);
            Assert.True(true);
        }

     

    }
}
