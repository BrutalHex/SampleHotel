using Fynd.Parser.ApplicationContract;
using Fynd.Parser.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fynd.Parser.Application
{
    public class DataExtractorService: IDataExtractorService
    {


        public async Task<ExportedInformation> ParseHtml(string input)
        {




            return new ExportedInformation();
        }

    }
}
