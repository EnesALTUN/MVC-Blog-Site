using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MVCBlogTez.Func
{
    public class Tool
    {
        public static string GenerateMd5(string yourString)
        {
            return string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(yourString)).Select(s => s.ToString("x2")));
        }

        public static string MetinKisalt(string metin, int neKadar)
        {
            return metin.Length > neKadar ? string.Format("{0}...", metin.Substring(0, neKadar)) : metin;
        }
    }
}