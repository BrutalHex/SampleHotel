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
            extractionsettings.Add("Stars", new Tuple<string, FetchText>("//span[@class='hp__hotel_ratings__stars hp__hotel_ratings__stars__clarification_track']/i[1]", HotelStarFetecher));
            extractionsettings.Add("ReviewPoints", new Tuple<string, FetchText>("//div[@id='reviewFloater']/div/a/span[2]/span[@class='average js--hp-scorecard-scoreval']", GeneralFetecher));
            extractionsettings.Add("ReviewPointsDescription", new Tuple<string, FetchText>("//div[@id='reviewFloater']/div/a/span[1]", GeneralFetecher));
            extractionsettings.Add("NumberOfReviews", new Tuple<string, FetchText>("//div[@id='reviewFloater']/div/span/strong", GeneralFetecher));
            extractionsettings.Add("Description", new Tuple<string, FetchText>("//div[@id='summary']/p", DescriptionFetecher));
            extractionsettings.Add("RoomCategories", new Tuple<string, FetchText>("//table[@id='maxotel_rooms']/tbody/tr/td[@class='ftd']", ListFetecher));
            extractionsettings.Add("AlternativeHotels", new Tuple<string, FetchText>("//table[@id='althotelsTable']/tbody/tr/td/p[@class='althotels-name']/a", ListFetecher));
        }

        public async Task<ExportedInformation> ParseHtml(string input)
        {
            ExportedInformation result = new ExportedInformation();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(input);
            TypeBasedHtmlExtractor(result, doc);
        
            return result;
        }

        /// <summary>
        /// extracts information for given object type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="doc">HtmlDocument</param>
        /// <returns>T</returns>
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

        private delegate object FetchText(HtmlDocument doc, string pattern);


        /// <summary>
        /// extracts information based on provided pattern
        /// </summary>
        /// <param name="doc">HtmlDocument</param>
        /// <param name="pattern">xPath Pattern</param>
        /// <returns></returns>
        public string GeneralFetecher(HtmlDocument doc, string pattern)
        {
            var item = FindSingleNode(doc, pattern);
            if (item == null)
            {
                return null;
            }
            return item.InnerText.SpecialTrim();
        }


        /// <summary>
        /// extracts hotel stars
        /// </summary>
        /// <param name="doc">HtmlDocument</param>
        /// <param name="pattern">xPath Pattern</param>
        /// <returns></returns>
        public string HotelStarFetecher(HtmlDocument doc, string pattern)
        {
            var item = FindSingleNode(doc, pattern);
            if(item==null)
            {
                return null;
            }
            var sentence= string.Join(" ", item.GetClasses()) ;
           var possibleStars = Enumerable.Range(1, 5).Select(number =>  new { Star=number,Description= $"ratings_stars_{number}" }).ToList();
            var exitingStar = possibleStars.FirstOrDefault(target => sentence.Contains( target.Description));
            if(exitingStar!=null)
            {
              return exitingStar.Star.ToString();
            }

            return null;

        }


        /// <summary>
        /// extracs descriptions
        /// </summary>
        /// <param name="doc">HtmlDocument</param>
        /// <param name="pattern">xPath Pattern</param>
        /// <returns></returns>
        public string DescriptionFetecher(HtmlDocument doc, string pattern)
        {
            var items = FindMultipleNodes(doc, pattern);
            if (items == null || items.Count==0)
            {
                return null;
            }
            var sentence = string.Join("\n", items.Select(text=>text.InnerText.SpecialTrim()));
            return sentence;
        }
        public List<string> ListFetecher(HtmlDocument doc, string pattern)
        {
            var items = FindMultipleNodes(doc, pattern);
            if (items == null || items.Count == 0)
            {
                return null;
            }
          return      items.Select(text => text.InnerText.SpecialTrim()).ToList();
         
        }
        


        /// <summary>
        /// extracts only one node based on pattern
        /// </summary>
        /// <param name="doc">HtmlDocument</param>
        /// <param name="pattern">xPath Pattern</param>
        /// <returns></returns>
        private HtmlNode FindSingleNode(HtmlDocument doc, string pattern)
        {
            return doc.DocumentNode.SelectSingleNode(pattern);
        }

        /// <summary>
        /// extracts all matching nodes 
        /// </summary>
        /// <param name="doc">HtmlDocument</param>
        /// <param name="pattern">xPath Pattern</param>
        /// <returns></returns>
        private HtmlNodeCollection FindMultipleNodes(HtmlDocument doc, string pattern)
        {
            return doc.DocumentNode.SelectNodes(pattern);
        }
    }
}
