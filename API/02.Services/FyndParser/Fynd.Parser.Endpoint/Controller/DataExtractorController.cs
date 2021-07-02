
using Fynd.Parser.ApplicationContract;
using Fynd.Parser.Domain;
using Fynd.Parser.Endpoint.Controller.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fynd.Parser.Endpoint.Controller
{
    [Route("api/[controller]/[action]/{key?}")]
    [ApiController]
    public class DataExtractorController : ControllerBase
    {
       private readonly  IDataExtractorService _dataExtractorService;
        public DataExtractorController(IDataExtractorService dataExtractorService)
        {
            _dataExtractorService = dataExtractorService;
        }
        

      /// <summary>
      /// extracts information from received information
      /// </summary>
      /// <param name="parserInput"></param>
      /// <returns></returns>
        [HttpPost]
        public async Task<ExportedInformation> Extract(ParserInput parserInput)
        {
            var result =await _dataExtractorService.ParseHtml(parserInput.Html);
            return result;
        }

    }
}
