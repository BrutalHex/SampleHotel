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


        [Fact]
        public async Task Test_Valid_Html()
        {
            string content = string.Empty;
            var transferClient = new Fynd.Parser.Endpoint.Grpc.DataExtractor.DataExtractorClient(GrpcChannel);
            var respons = await transferClient.ExtractAsync(new Fynd.Parser.Endpoint.Grpc.ExtractRequest {  Html= content });

            Assert.True(true);
        }
        

    }
}
