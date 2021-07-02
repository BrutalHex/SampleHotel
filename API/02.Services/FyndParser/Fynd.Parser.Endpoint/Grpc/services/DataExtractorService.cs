using AutoMapper;
using Fynd.Parser.ApplicationContract;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fynd.Parser.Endpoint.Grpc.services
{

    /// <summary>
    /// Parser service 
    /// </summary>
    /// <remarks>
    /// grpc ui command:
    /// grpcui -proto ParserConvertor.proto -import-path "C:\projects\Test\Fynd\ParserSample\API\02.Services\FyndParser\Fynd.Parser.Endpoint\Grpc\Proto" -plaintext localhost:5000
    /// </remarks>
    public class DataExtractorService : Fynd.Parser.Endpoint.Grpc.DataExtractor.DataExtractorBase
    {
        private readonly IDataExtractorService _dataExtractorService;
        private readonly IMapper _mapper;
        public DataExtractorService(IDataExtractorService dataExtractorService, IMapper mapper)
        {
            _dataExtractorService = dataExtractorService;
            _mapper = mapper;
        }


        /// <summary>
        /// returns the conversion rate for given crypto
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async override Task<Fynd.Parser.Endpoint.Grpc.ExtractResponse> Extract(Fynd.Parser.Endpoint.Grpc.ExtractRequest request, ServerCallContext context)
        {

          var data= await _dataExtractorService.ParseHtml(request.Html);

            return null;
        }
    }
}
