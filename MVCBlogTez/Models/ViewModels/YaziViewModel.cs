using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCBlogTez.Models.ViewModels
{
    public class YaziViewModel
    {
        public List<Yazi> Yazilar { get; set; }
        public List<Etiket> Etiketler { get; set; }
        public List<Yorum> Yorumlar { get; set; }
        public Kullanici Kullanici { get; set; }
        public Yazi Yazi { get; set; }
        public List<Kategori> Kategoriler { get; set; }
        public Yorum Yorum { get; set; }
    }
}