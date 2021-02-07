using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCBlogTez.Models
{
    public class Etiket
    {
        public int EtiketId { get; set; }

        [Required(ErrorMessage = "*Etiket Adı alanı boş geçilemez.")]
        public string EtiketAd { get; set; }
        public int YaziId { get; set; }
        public int DurumId { get; set; }


        public virtual Yazi Yazi { get; set; }
        public virtual Durum Durum { get; set; }
    }
}