using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextSummarizer
{
    public static class StringExtensions
    {
        public static string CleanString(this string value)
        {
            if (value.Contains("https"))
            {
                return string.Empty;
            }
            else
            {
                string retVal = "";
                for (int i = 0; i < value.Length; i++)
                {
                    if (char.IsLetter(value[i]) && value[i] is not ' ')
                    {
                        retVal += value[i];
                    }
                }
                return retVal.ToLower();
            }
        }
    }
}
