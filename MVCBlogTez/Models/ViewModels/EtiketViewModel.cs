using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCBlogTez.Models.ViewModels
{
    public class EtiketViewModel
    {
        public Etiket Etiket { get; set; }
        public List<Yazi> Yazilar { get; set; }
    }
}