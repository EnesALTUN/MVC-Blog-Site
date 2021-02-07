using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCBlogTez.Models.ViewModels
{
    public class RegisterViewModel
    {
        public Kullanici Kullanici { get; set; }
        public List<Cinsiyet> Cinsiyetler { get; set; }
    }
}