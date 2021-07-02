using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fynd.Parser.Domain
{
    public class ExportedInformation
    {
        public string HotelName { get; set; }
        public string Address { get; set; }

        public string ClassificationAndStars { get; set; }
        public string ReviewPoints { get; set; }
        public string NumberOfReviews { get; set; }
        public string Description { get; set; }

        public List<string> RoomCategories { get; set; }

        public List<string> AlternativeHotels { get; set; }
 
 
    }
}
