using Fynd.Parser.Domain;
using Fynd.Parser.IntegrationTest.Artifacts;
using Parser.IntegrationTest.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Parser.IntegrationTest.Grpc
{
    public class DataExtractorServiceTest : BaseIntegrationTest
    {
        public DataExtractorServiceTest(
        TestWebApplicationFactory<Fynd.Parser.Endpoint.Startup> factory) : base(factory)
        {

        }


        [Theory(DisplayName = "Test_Valid_Html")]
        [MemberData(nameof(SampleHtmlData.Get), MemberType = typeof(SampleHtmlData))]
        public async Task Test_Valid_Html(string content)
        {

            var transferClient = new Fynd.Parser.Endpoint.Grpc.DataExtractor.DataExtractorClient(GrpcChannel);
            var respons = await transferClient.ExtractAsync(new Fynd.Parser.Endpoint.Grpc.ExtractRequest {  Html= content });

            var expected = SampleHtmlData.GetMatchingDtoToProvidedHtml();

            Assert.False(HasDifferenceInValues(respons, expected));
        }
        

    }
}
