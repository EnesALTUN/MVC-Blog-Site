using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCBlogTez.Models
{
    public class Cinsiyet
    {
        public int CinsiyetId { get; set; }
        public string CinsiyetAd { get; set; }


        public virtual ICollection<Kullanici> Kullanicilar { get; set; }
    }
}