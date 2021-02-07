 using MVCBlogTez.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using MVCBlogTez.Models.ViewModels;
using System.IO;

namespace MVCBlogTez.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            return Redirect("/Admin/Kullanici");
        }

        #region Kullanici İşlemleri
        public ActionResult Kullanici()
        {
            var kullanici = blogContext.Kullanici.Where(x => x.DurumId == 1).ToList();

            return View(kullanici);
        }

        public ActionResult AdminlikVer(int? id)
        {
            if (id != null)
            {
                var kullanici = blogContext.Kullanici.Where(x => x.KullaniciId == id).FirstOrDefault();
                kullanici.RolId = 1;

                blogContext.SaveChanges();
            }

            return Redirect("/Admin/Kullanici");
        }

        public ActionResult AdminlikGeriAl(int? id)
        {
            if (id != null)
            {
                var kullanici = blogContext.Kullanici.Where(x => x.KullaniciId == id).FirstOrDefault();
                kullanici.RolId = 2;

                blogContext.SaveChanges();
            }

            return Redirect("/Admin/Kullanici");
        }
        #endregion

        #region Duyuru İşlemleri
        public ActionResult Duyuru()
        {
            var duyuru = blogContext.Duyuru.ToList();

            return View(duyuru);
        }

        public ActionResult DuyuruEkle()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DuyuruEkle(Duyuru duyuru)
        {
            duyuru.KullaniciId = (int)Session["UserId"];
            duyuru.YayinlanmaTarihi = DateTime.Now;
            duyuru.DurumId = 1;

            blogContext.Duyuru.Add(duyuru);
            blogContext.SaveChanges();

            return Redirect("/Admin/Duyuru");
        }

        public ActionResult DuyuruSil(int? id)
        {
            if (id != null)
            {
                var duyuru = blogContext.Duyuru.Where(x => x.DuyuruId == id).SingleOrDefault();

                blogContext.Duyuru.Remove(duyuru);
                blogContext.SaveChanges();
            }
            return Redirect("/Admin/Duyuru");
        }

        public ActionResult DuyuruDuzenle(int? id)
        {
            Duyuru duyuru = blogContext.Duyuru.Where(x => x.DuyuruId == id).SingleOrDefault();

            return View(duyuru);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DuyuruDuzenle(Duyuru duyuru)
        {
            Duyuru duyuru1 = blogContext.Duyuru.Where(x => x.DuyuruId == duyuru.DuyuruId).SingleOrDefault();

            duyuru1.DuyuruIcerik = duyuru.DuyuruIcerik;
            blogContext.SaveChanges();

            return Redirect("/Admin/Duyuru");
        }
        #endregion

        #region Etiket İşlemleri
        public ActionResult Etiket()
        {
            var etiket = blogContext.Etiket.Where(x => x.DurumId == 1).ToList();

            return View(etiket);
        }

        public ActionResult EtiketEkle()
        {
            EtiketViewModel etiketview = new EtiketViewModel();

            etiketview.Yazilar = blogContext.Yazi.ToList();

            return View(etiketview);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EtiketEkle(Etiket etiket)
        {
            if (etiket != null)
            {
                etiket.DurumId = 1;
                etiket.YaziId = Convert.ToInt32(Request.Form["yazilar"]);
                blogContext.Etiket.Add(etiket);
                blogContext.SaveChanges();
            }
            return Redirect("/Admin/Etiket");
        }

        public ActionResult EtiketDuzenle(int? id)
        {
            EtiketViewModel etiketview = new EtiketViewModel();

            etiketview.Etiket = blogContext.Etiket.Where(x => x.EtiketId == id).SingleOrDefault();
            etiketview.Yazilar = etiketview.Yazilar = blogContext.Yazi.ToList();

            return View(etiketview);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EtiketDuzenle(Etiket etiket)
        {
            Etiket etiket1 = blogContext.Etiket.Where(x => x.EtiketId == etiket.EtiketId).SingleOrDefault();

            etiket1.EtiketAd = etiket.EtiketAd;
            etiket1.YaziId = Convert.ToInt32(Request.Form["yazilar"]);
            blogContext.SaveChanges();

            return Redirect("/Admin/Etiket");
        }


        public ActionResult EtiketSil(int? id)
        {
            if (id != null)
            {
                var etiket = blogContext.Etiket.Where(x => x.EtiketId == id).SingleOrDefault();

                etiket.DurumId = 2;

                blogContext.SaveChanges();
            }
            return Redirect("/Admin/Etiket");
        }
        #endregion

        #region Kategori İşlemleri
        public ActionResult Kategori()
        {
            var kategoriler = blogContext.Kategori.Where(x => x.DurumId == 1).ToList();

            return View(kategoriler);
        }

        public ActionResult KategoriEkle()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KategoriEkle(Kategori kategori)
        {
            kategori.YaziSayisi = 0;
            kategori.KullaniciId = (int)Session["UserId"];
            kategori.OlusturulmaTarihi = DateTime.Now;
            kategori.DurumId = 1;

            blogContext.Kategori.Add(kategori);
            blogContext.SaveChanges();

            return Redirect("/Admin/Kategori");
        }

        public ActionResult KategoriSil(int? id)
        {
            if (id != null)
            {
                Kategori kategori = blogContext.Kategori.Where(x => x.KategoriId == id).SingleOrDefault();

                kategori.DurumId = 2;

                blogContext.SaveChanges();
            }

            return Redirect("/Admin/Kategori");
        }
        #endregion

        #region Rol İşlemleri
        public ActionResult Rol()
        {
            var rol = blogContext.Rol.ToList();

            return View(rol);
        }

        public ActionResult RolEkle()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RolEkle(Rol rol)
        {
            rol.DurumId = 1;
            blogContext.Rol.Add(rol);
            blogContext.SaveChanges();

            return Redirect("/Admin/Rol");
        }

        public ActionResult RolSil(int? id)
        {
            Rol rol = blogContext.Rol.Where(x => x.RolId == id).SingleOrDefault();
            
            rol.DurumId = 2;

            blogContext.SaveChanges();

            return Redirect("/Admin/Rol");
        }
        #endregion

        #region Slider İşlemleri
        public ActionResult Slider()
        {
            var slider = blogContext.Slider.Where(x => x.DurumId == 1).ToList();

            return View(slider);
        }

        public ActionResult SliderEkle()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SliderEkle(Slider slider)
        {
            slider.DurumId = 1;

            if (Path.GetFileName(Request.Files[0].FileName) != "")
            {
                string filename = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Uploads/images/" + filename;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                slider.FotografKonum = "/Uploads/images/" + filename;
            }

            blogContext.Slider.Add(slider);
            blogContext.SaveChanges();

            return Redirect("/Admin/Slider");
        }

        public ActionResult SliderSil(int? id)
        {
            if (id != null)
            {
                Slider slider = blogContext.Slider.Where(x => x.SliderId == id).SingleOrDefault();

                slider.DurumId = 2;

                blogContext.SaveChanges();
            }

            return Redirect("/Admin/Slider");
        }
        #endregion

        #region Yazı İşlemleri
        public ActionResult Yazi()
        {
            var yazilar = blogContext.Yazi.Where(x => x.DurumId == 1).ToList();

            return View(yazilar);
        }

        public ActionResult YaziEkle()
        {
            YaziViewModel yaziview = new YaziViewModel();

            yaziview.Kategoriler = (from x in blogContext.Kategori select x).Where(x => x.DurumId == 1).ToList();

            return View(yaziview);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult YaziEkle(Yazi yazi)
        {
            if (yazi != null)
            {
                int kategoriId = Convert.ToInt32(Request.Form["kategoriler"]);

                Kategori kategori = blogContext.Kategori.Where(x => x.KategoriId == kategoriId).FirstOrDefault();
                kategori.YaziSayisi++;

                yazi.DurumId = 1;
                yazi.YayinlanmaTarihi = DateTime.Now;
                yazi.KullaniciId = (int)Session["UserId"];
                yazi.YorumSayisi = 0;
                yazi.KategoriId = kategoriId;

                if (Path.GetFileName(Request.Files[0].FileName) != "")
                {
                    string filename = Path.GetFileName(Request.Files[0].FileName);
                    string uzanti = Path.GetExtension(Request.Files[0].FileName);
                    string yol = "~/Uploads/images/" + filename;
                    Request.Files[0].SaveAs(Server.MapPath(yol));
                    yazi.FotografKonum = "/Uploads/images/" + filename;
                }

                blogContext.Yazi.Add(yazi);
                blogContext.SaveChanges();
            }
            return Redirect("/Admin/Yazi");
        }

        public ActionResult YaziSil(int? id)
        {
            if (id != null)
            {
                Yazi yazi = blogContext.Yazi.Where(x => x.YaziId == id).SingleOrDefault();

                yazi.DurumId = 2;

                blogContext.SaveChanges();
            }
            return Redirect("/Admin/Yazi");
        }
        #endregion

        #region Yorum İşlemleri
        public ActionResult Yorum()
        {
            var yorum = blogContext.Yorum.Where(x => x.DurumId == 1).ToList();

            return View(yorum);
        }

        public ActionResult YorumSil(int? id)
        {
            Yorum yorum = blogContext.Yorum.Where(x => x.YorumId == id).SingleOrDefault();
                
            yorum.DurumId = 2;

            blogContext.SaveChanges();

            return Redirect("/Admin/Yorum");
        }
        #endregion
    }
}