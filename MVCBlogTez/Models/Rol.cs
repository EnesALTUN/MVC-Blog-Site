using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCBlogTez.Models
{
    public class Rol
    {
        public int RolId { get; set; }
        public string RolAd { get; set; }
        public int DurumId { get; set; }


        public virtual Durum Durum { get; set; }
        public virtual ICollection<Kullanici> Kullanicilar { get; set; }
    }
}