using Fynd.Parser.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fynd.Parser.ApplicationContract
{
    public interface IDataExtractorService
    {
        public Task<ExportedInformation> ParseHtml(string input);

    }
}
