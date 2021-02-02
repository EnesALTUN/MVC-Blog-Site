using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCBlogTez.Models
{
    public class Duyuru
    {
        public int DuyuruId { get; set; }
        public string DuyuruIcerik { get; set; }
        public int KullaniciId { get; set; }
        public DateTime YayinlanmaTarihi { get; set; }
        public int DurumId { get; set; }

        
        public virtual Kullanici Kullanici { get; set; }
        public virtual Durum Durum { get; set; }
    }
}