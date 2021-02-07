using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCBlogTez.Models
{
    public class Iletisim
    {

        [Required(ErrorMessage = "*Ad Soyad alanı boş geçilemez.")]
        public string AdSoyad { get; set; }

        [Required(ErrorMessage = "*Email alanı boş geçilemez.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Geçerli Email adresi giriniz.")]
        public string Email { get; set; }
        public string Telefon { get; set; }

        [Required(ErrorMessage = "*Konu alanı boş geçilemez.")]
        public string Konu { get; set; }

        [Required(ErrorMessage = "*Mesaj alanı boş geçilemez.")]
        public string Mesaj { get; set; }
    }
}