using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCBlogTez.Models
{
    public class Kullanici
    {
        public int KullaniciId { get; set; }

        [Required(ErrorMessage = "*Ad alanı boş geçilemez.")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "*Soyad alanı boş geçilemez.")]
        public string Soyad { get; set; }

        [Required(ErrorMessage = "*Email alanı boş geçilemez.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Geçerli Email adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "*Parola alanı boş geçilemez.")]
        public string Parola { get; set; }
        public string Telefon { get; set; }
        public string FotografKonum { get; set; }
        public DateTime KayitTarihi { get; set; }
        public DateTime SonGirisTarihi { get; set; }
        public int? CinsiyetId { get; set; }

        [Required(ErrorMessage = "*Doğum Tarihi alanı boş geçilemez.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DogumTarihi { get; set; }
        public string AcikAdres { get; set; }
        public int DurumId { get; set; }
        public int RolId { get; set; }


        public virtual Cinsiyet Cinsiyet { get; set; }
        public virtual Durum Durum { get; set; }
        public virtual Rol Rol { get; set; }
        public virtual ICollection<Duyuru> Duyurular { get; set; }
        public virtual ICollection<Kategori> Kategoriler { get; set; }
        public virtual ICollection<Yazi> Yazilar { get; set; }
        public virtual ICollection<Yorum> Yorumlar { get; set; }
    }
}