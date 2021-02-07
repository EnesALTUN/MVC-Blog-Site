using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCBlogTez.Models
{
    public class Kategori
    {
        public int KategoriId { get; set; }

        [Required(ErrorMessage = "*Kategori Ad alanı boş geçilemez.")]
        public string KategoriAd { get; set; }
        public int YaziSayisi { get; set; }
        public int KullaniciId { get; set; }
        public DateTime OlusturulmaTarihi { get; set; }
        public int DurumId { get; set; }


        public virtual Kullanici Kullanici { get; set; }
        public virtual Durum Durum { get; set; }
        public virtual ICollection<Yazi> Yazilar { get; set; }
    }
}