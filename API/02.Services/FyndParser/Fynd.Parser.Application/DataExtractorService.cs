using Fynd.Framework.Core.Extenions;
using Fynd.Parser.ApplicationContract;
using Fynd.Parser.Domain;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fynd.Parser.Application
{
    public class DataExtractorService: IDataExtractorService
    {

        Dictionary<string, Tuple<string, FetchText>> extractionsettings = new Dictionary<string, Tuple<string, FetchText>>();
        public DataExtractorService()
        {
            extractionsettings.Add("HotelName",new Tuple<string, FetchText>( "//span[@id='hp_hotel_name']", GeneralFetecher));
            extractionsettings.Add("Address", new Tuple<string, FetchText>("//span[@id='hp_address_subtitle']", GeneralFetecher));
            extractionsettings.Add("ClassificationAndStars", new Tuple<string, FetchText>("//span[@class='hp__hotel_ratings__stars hp__hotel_ratings__stars__clarification_track']/i[1]", HotelStarFetecher));
            extractionsettings.Add("ReviewPoints", new Tuple<string, FetchText>("", GeneralFetecher));
            extractionsettings.Add("NumberOfReviews", new Tuple<string, FetchText>("", GeneralFetecher));
            extractionsettings.Add("Description", new Tuple<string, FetchText>("", GeneralFetecher));
            extractionsettings.Add("RoomCategories", new Tuple<string, FetchText>("", GeneralFetecher));
            extractionsettings.Add("AlternativeHotels", new Tuple<string, FetchText>("", GeneralFetecher));
        }

        public async Task<ExportedInformation> ParseHtml(string input)
        {
            ExportedInformation result = new ExportedInformation();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(input);
            TypeBasedHtmlExtractor(result, doc);
        



            return new ExportedInformation();
        }


        public T TypeBasedHtmlExtractor<T>(T dto, HtmlDocument doc) where T: class
        {
            foreach (var prop in dto.GetType().GetProperties())
            {
                if(extractionsettings.Keys.Contains(prop.Name))
                {

                    prop.SetValue(dto, extractionsettings[prop.Name].Item2(doc, extractionsettings[prop.Name].Item1));

                  
                }
            }


            return dto;
        }

        private delegate string FetchText(HtmlDocument doc, string pattern);


        public string GeneralFetecher(HtmlDocument doc, string pattern)
        {
            var item = FindSingleNode(doc, pattern);
            if (item == null)
            {
                return null;
            }
            return item.InnerText.SpecialTrim();
        }

        public string HotelStarFetecher(HtmlDocument doc, string pattern)
        {
            var item = FindSingleNode(doc, pattern);
            if(item==null)
            {
                return null;
            }
            var sentence= string.Join(" ", doc.DocumentNode.SelectSingleNode(pattern).GetClasses()) ;
           var possibleStars = Enumerable.Range(1, 5).Select(number =>  new { Star=number,Description= $"ratings_stars_{number}" }).ToList();
            var exitingStar = possibleStars.FirstOrDefault(target => sentence.Contains( target.Description));
            if(exitingStar!=null)
            {
              return exitingStar.Star.ToString();
            }

            return null;

        }

        private HtmlNode FindSingleNode(HtmlDocument doc, string pattern)
        {
            return doc.DocumentNode.SelectSingleNode(pattern);
        }
    }
}
