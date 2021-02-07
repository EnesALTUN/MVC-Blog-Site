using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCBlogTez.Models
{
    public class Yazi
    {
        public int YaziId { get; set; }

        [Required(ErrorMessage = "*Başlık alanı boş geçilemez.")]
        public string Baslik { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "*İçerik alanı boş geçilemez.")]
        public string Icerik { get; set; }
        public int YorumSayisi { get; set; }
        public int KullaniciId { get; set; }
        public int KategoriId { get; set; }
        public DateTime YayinlanmaTarihi { get; set; }
        public string FotografKonum { get; set; }
        public int DurumId { get; set; }


        public virtual Kullanici Kullanici { get; set; }
        public virtual Kategori Kategori { get; set; }
        public virtual Durum Durum { get; set; }
        public virtual ICollection<Yorum> Yorumlar { get; set; }
        public virtual ICollection<Etiket> Etiketler { get; set; }
    }
}