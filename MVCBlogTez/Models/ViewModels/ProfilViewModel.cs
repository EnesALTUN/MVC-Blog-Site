using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCBlogTez.Models.ViewModels
{
    public class ProfilViewModel
    {
        public List<Kullanici> Kullanicilar { get; set; }
        public List<Cinsiyet> Cinsiyetler { get; set; }
    }
}