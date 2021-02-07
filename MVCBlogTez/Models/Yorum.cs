using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCBlogTez.Models
{
    public class Yorum
    {
        public int YorumId { get; set; }
        public int KullaniciId { get; set; }

        [Required(ErrorMessage = "*Yorum alanı boş geçilemez.")]
        public string YorumIcerik { get; set; }
        public DateTime YayinlanmaTarihi { get; set; }
        public int YaziId { get; set; }
        public int DurumId { get; set; }


        public virtual Kullanici Kullanici { get; set; }
        public virtual Yazi Yazi { get; set; }
        public virtual Durum Durum { get; set; }
    }
}