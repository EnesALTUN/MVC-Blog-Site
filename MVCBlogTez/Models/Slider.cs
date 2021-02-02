using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCBlogTez.Models
{
    public class Slider
    {
        public int SliderId { get; set; }
        public string SliderBaslik { get; set; }
        public string SliderIcerik { get; set; }
        public string FotografKonum { get; set; }
        public int Siralama { get; set; }
        public int DurumId { get; set; }


        public virtual Durum Durum { get; set; }
    }
}