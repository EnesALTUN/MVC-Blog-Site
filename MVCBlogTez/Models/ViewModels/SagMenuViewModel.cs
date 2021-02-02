using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCBlogTez.Models.ViewModels
{
    public class SagMenuViewModel
    {
        public List<Duyuru> Duyurular { get; set; }
        public List<Kategori> Kategoriler { get; set; }
        public List<Yazi> Yazilar { get; set; }
    }
}