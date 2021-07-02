using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Fynd.Framework.Core.Domain.Communication
{
    public class BaseHttpRepository
    {
        protected HttpClient HttpClient { get; private set; }


        public BaseHttpRepository(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }



        /// <summary>
        /// final step to call external service
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headerValues"></param>
        /// <param name="queryString"></param>
        /// <returns>
        /// the response value as string
        /// </returns>
        private async Task<string> ExecuteGet(string url, Dictionary<string, string> headerValues, Dictionary<string, string> queryString)
        {
            HttpClient.DefaultRequestHeaders.Clear();
            string responseData;
            try
            {
                foreach (var header in headerValues)
                {
                    HttpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
                var finalizedUrl = url + BuildQueryString(queryString);
                using var response = await HttpClient.GetAsync(finalizedUrl);
                responseData = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {

                throw ex.InnerException ?? ex;
            }
            return responseData;
        }


        /// <summary>
        /// calls the provided url and returns the data
        /// </summary>
        /// <typeparam name="T">the http call rresult will deserialized to provided type</typeparam>
        /// <param name="url">the address of http service</param>
        /// <param name="headerValues">the headers required for service</param>
        /// <param name="queryString">the list of querystring parameters</param>
        /// <param name="settings">settiings for deserialization</param>
        /// <returns></returns>
        public async Task<T> Get<T>(string url, Dictionary<string, string> headerValues, Dictionary<string, string> queryString, JsonSerializerSettings settings = null)
        {
            string response = await ExecuteGet(url, headerValues, queryString);
            HandleJsonSettings(settings);
            var result = JsonConvert.DeserializeObject<T>(response, settings);
            return result;
        }


        /// <summary>
        /// manages json convert settings
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public JsonSerializerSettings HandleJsonSettings(JsonSerializerSettings settings)
        {
            var defaultSetting= new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                };

            return settings ?? defaultSetting;

        }


        /// <summary>
        /// create a query string based on provided valuse
        /// </summary>
        /// <param name="queryString">queryString</param>
        /// <returns>
        /// a string that represents queryString to be used in URL
        /// </returns>
        public string BuildQueryString(Dictionary<string, string> queryString)
        {
            if (queryString == null || queryString.Count == 0)
                return string.Empty;

            var pparams = string.Join("&", queryString.Select(item => $"{item.Key}={item.Value}").ToList());
            return $"?{pparams}";
        }
    }
}
