using MVCBlogTez.Models;
using System.Collections.Generic;
using MVCBlogTez.Models.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVCBlogTez.Func;

namespace MVCBlogTez.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var gelen = blogContext.Yazi
                .Include("Kategori").Where(x=>x.DurumId == 1).ToList();

            return View(gelen);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string Email, string Parola)
        {
            Parola = Tool.GenerateMd5(Parola);

            var kullanici = blogContext.Kullanici.Include("Rol").Where(
                m => m.Email == Email && m.Parola == Parola).FirstOrDefault();
            if (kullanici != null && ModelState.IsValid)
            {
                FormsAuthentication.SetAuthCookie(kullanici.Email, false);
                Session["LoginUser"] = kullanici.Email;
                Session["UserId"] = kullanici.KullaniciId;
                Session["Rol"] = (string)kullanici.Rol.RolAd;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Err = "Bilgiler hatalı!";
                return View();
            }
        }

        public ActionResult Register()
        {
            RegisterViewModel register = new RegisterViewModel();

            register.Cinsiyetler = (from x in blogContext.Cinsiyet select x).ToList();

            return View(register);
        }

        [HttpPost]
        public ActionResult Register(Kullanici kullanici)
        {
            if (kullanici.Ad == null || kullanici.Soyad == null || kullanici.Email == null || kullanici.Parola == null || kullanici.Telefon == null || kullanici.CinsiyetId == null || kullanici.DogumTarihi == null)
            {
                TempData["bos"] = "Tüm alanları doldurun.";
                return Redirect("/Home/Register");
            }
            else if (blogContext.Kullanici.Where(x => x.Email == kullanici.Email).Count() > 0)
            {
                TempData["bos"] = "Girilen email adresi kullanımda.";
                return Redirect("/Home/Register");
            }
            else
            {
                kullanici.KayitTarihi = DateTime.Now;
                kullanici.RolId = 2;
                kullanici.DurumId = 1;
                kullanici.SonGirisTarihi = DateTime.Now;
                kullanici.Parola = Tool.GenerateMd5(kullanici.Parola);

                blogContext.Kullanici.Add(kullanici);
                blogContext.SaveChanges();
            }
            return Redirect("/Home/Login");
        }

        [Authorize]
        public ActionResult Cikis()
        {
            Session["LoginUser"] = null;
            Session["Rol"] = null;
            Session.Clear();

            FormsAuthentication.SignOut();

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now);

            return RedirectToAction("Index", "Home");
        }

        [ChildActionOnly]
        public ActionResult SagMenu()
        {
            SagMenuViewModel sagmenu = new SagMenuViewModel();
            sagmenu.Duyurular = (from x in blogContext.Duyuru select x).OrderByDescending(x => x.YayinlanmaTarihi).Where(x=>x.DurumId == 1).Take(5).ToList();
            sagmenu.Kategoriler = (from x in blogContext.Kategori select x).OrderByDescending(x => x.YaziSayisi).Where(x => x.DurumId == 1).Take(5).ToList();
            sagmenu.Yazilar = (from x in blogContext.Yazi select x).Where(x => x.DurumId == 1).OrderBy(x => x.YaziId).Take(5).ToList();

            return PartialView("_SagMenu", sagmenu);
        }

        [ChildActionOnly]
        public ActionResult Slider()
        {
            var slider = blogContext.Slider.Where(x=> x.DurumId == 1).ToList();

            return PartialView("_Slider", slider);
        }

        public ActionResult Yazi(int? id)
        {
            YaziViewModel yazi = new YaziViewModel();

            yazi.Yazilar = (from x in blogContext.Yazi select x).Where(x => x.YaziId == id).ToList();
            yazi.Etiketler = (from x in blogContext.Etiket select x).Where(x => x.YaziId == id).ToList();
            yazi.Yorumlar = (from x in blogContext.Yorum select x).Where(x => x.YaziId == id).Where(x=>x.DurumId == 1).ToList();
            Session["YaziId"] = id;

            return View(yazi);
        }

        public ActionResult Contact()
        {
            return View();
        }

        [Authorize(Roles = "Üye, Admin")]
        [HttpPost]
        public ActionResult Yorum(Yorum yorum)
        {
            if (yorum.YorumIcerik == null)
            {
                TempData["bos"] = "Boş yorum yapılamaz.";
                return Redirect("/Home/Yazi/" + Session["YaziId"]);
            }
            else
            {
                yorum.KullaniciId = (int)Session["UserId"];
                yorum.DurumId = 1;
                yorum.YayinlanmaTarihi = DateTime.Now;
                yorum.YaziId = (int)Session["YaziId"];
                blogContext.Yorum.Add(yorum);

                int YaziId = (int)Session["YaziId"];

                Yazi yazi = blogContext.Yazi.Where(x => x.YaziId == YaziId).SingleOrDefault();
                yazi.YorumSayisi++;

                blogContext.SaveChanges();
            }

            return Redirect("/Home/Yazi/" + Session["YaziId"]);
        }

        [Authorize(Roles = "Üye, Admin")]
        public ActionResult YorumSil(int? id, int? userId)
        {
            if (Session["UserId"] == null)
                return Redirect("/Home/Yazi/" + Session["YaziId"]);

            if (id != null && userId == (int)Session["UserId"]) 
            {
                Yorum yorum = blogContext.Yorum.Where(x => x.YorumId == id).SingleOrDefault();

                blogContext.Yorum.Remove(yorum);

                int YaziId = (int)Session["YaziId"];

                Yazi yazi = blogContext.Yazi.Where(x => x.YaziId == YaziId).SingleOrDefault();
                yazi.YorumSayisi--;

                blogContext.SaveChanges();
            }

            return Redirect("/Home/Yazi/" + Session["YaziId"]);
        }
        
        [Authorize (Roles = "Üye, Admin")]
        public ActionResult Profil()
        {
            int UserId = (int)Session["UserId"];

            ProfilViewModel profil = new ProfilViewModel();

            profil.Kullanicilar = (from x in blogContext.Kullanici select x).Where(x => x.KullaniciId == UserId).ToList();
            profil.Cinsiyetler = (from x in blogContext.Cinsiyet select x).ToList();

            return View(profil);
        }

        [Authorize(Roles = "Üye, Admin")]
        [HttpPost]
        public ActionResult ProfilDuzenle(Kullanici kullanici)
        {
            int UserId = (int)Session["UserId"];

            Kullanici kullanici1 = blogContext.Kullanici.Where(x => x.KullaniciId == UserId).SingleOrDefault();
            kullanici1.Ad = kullanici.Ad;
            kullanici1.Soyad = kullanici.Soyad;
            kullanici1.Email = kullanici.Email;
            kullanici1.Parola = kullanici.Parola;
            kullanici1.Telefon = kullanici.Telefon;

            if (Path.GetFileName(Request.Files[0].FileName) != "")
            {
                string filename = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Uploads/images/" + filename;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                kullanici1.FotografKonum = "/Uploads/images/" + filename;
            }
            kullanici1.CinsiyetId = kullanici.CinsiyetId;
            kullanici1.AcikAdres = kullanici.AcikAdres;

            blogContext.SaveChanges();
            return Redirect("/Home/Profil");
        }
        
        [HttpPost]
        public ActionResult Iletisim(Iletisim iletisim)
        {
                var body = new StringBuilder();
                body.AppendLine("Ad Soyad: " + iletisim.AdSoyad);
                body.AppendLine("Email: " + iletisim.Email);
                body.AppendLine("Tel: " + iletisim.Telefon);
                body.AppendLine("Konu: " + iletisim.Konu);
                body.AppendLine("Mesaj: " + iletisim.Mesaj);

                Gmail.SendMail(body.ToString());
                TempData["Success"] = "Mesajınız başarıyla gönderilmiştir.";
            return Redirect("/Home/Contact");
        }
    }
}