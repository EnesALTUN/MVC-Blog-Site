using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCBlogTez.Models
{
    public class Durum
    {
        public int DurumId { get; set; }
        public string DurumAd { get; set; }


        public virtual ICollection<Rol> Roller { get; set; }
        public virtual ICollection<Kullanici> Kullanicilar { get; set; }
        public virtual ICollection<Duyuru> Duyurular { get; set; }
        public virtual ICollection<Kategori> Kategoriler { get; set; }
        public virtual ICollection<Slider> Sliderlar { get; set; }
        public virtual ICollection<Yazi> Yazilar { get; set; }
        public virtual ICollection<Yorum> Yorumlar { get; set; }
        public virtual ICollection<Etiket> Etiketler { get; set; }
    }
}