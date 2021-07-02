using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fynd.Framework.Core.Extenions
{
    public  static class StringExtension
    {
        /// <summary>
        /// removes special characters
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns></returns>
        public static string SpecialTrim(this string sentence)
        {

            if(string.IsNullOrEmpty(sentence))
            {
                return sentence;
            }

            List<string> stringsToRemove = new List<string> { "\r" ,"\n"};

            foreach (var item in stringsToRemove)
            {
                sentence = sentence.Replace(item,"");
            }
            return sentence.Trim();


        }
    }
}
