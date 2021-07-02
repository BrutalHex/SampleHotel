using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fynd.Parser.Endpoint.Grpc.Map
{

    public class GrpcServiceProfile : AutoMapper.Profile
    {
        public GrpcServiceProfile()
        {
            CreateMap<Domain.ExportedInformation, Fynd.Parser.Endpoint.Grpc.ExtractResponse>().ReverseMap();



        }
    }
}
