using MVCBlogTez.Context;
using MVCBlogTez.Func;
using MVCBlogTez.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCBlogTez.App_Start
{
    public class MyInitializer : CreateDatabaseIfNotExists<BlogContext> // Veritabanı yoksa çalış
    {
        protected override void Seed(BlogContext context) // Veritabanını besle
        {
            #region Durum
            Durum durumAktif = new Durum()
            {
                DurumAd = "Aktif"
            };
            Durum durumPasif = new Durum()
            {
                DurumAd = "Pasif"
            };
            #endregion

            #region Rol
            Rol rolAdmin = new Rol()
            {
                RolAd = "Admin",
                DurumId = 1
            };
            Rol rolÜye = new Rol()
            {
                RolAd = "Üye",
                DurumId = 1
            };
            #endregion

            #region Cinsiyet
            Cinsiyet cinsiyetErkek = new Cinsiyet()
            {
                CinsiyetAd = "Erkek",
            };
            Cinsiyet cinsiyetKadin = new Cinsiyet()
            {
                CinsiyetAd = "Kadın",
            };
            #endregion

            #region Admin
            Kullanici kullaniciAdmin = new Kullanici()
            {
                Ad = "Admin",
                Soyad = "Admin",
                Email = "admin@gmail.com",
                Parola = Tool.GenerateMd5("1234"),
                KayitTarihi = DateTime.Now,
                SonGirisTarihi = DateTime.Now,
                CinsiyetId = 1,
                DogumTarihi = DateTime.Now,
                DurumId = 1,
                RolId = 1
            };
            #endregion

            #region Slider
            Slider slider1 = new Slider()
            {
                FotografKonum = "/Uploads/images/Slider.jpg",
                Siralama = 1,
                DurumId = 1
            };
            Slider slider2 = new Slider()
            {
                FotografKonum = "/Uploads/images/Slider.jpg",
                Siralama = 2,
                DurumId = 1
            };
            Slider slider3 = new Slider()
            {
                FotografKonum = "/Uploads/images/Slider.jpg",
                Siralama = 3,
                DurumId = 1
            };
            #endregion


            context.Durum.Add(durumAktif);
            context.Durum.Add(durumPasif);
            context.SaveChanges();

            context.Slider.Add(slider1);
            context.Slider.Add(slider2);
            context.Slider.Add(slider3);

            context.Rol.Add(rolAdmin);
            context.Rol.Add(rolÜye);

            context.Cinsiyet.Add(cinsiyetErkek);
            context.Cinsiyet.Add(cinsiyetKadin);
            context.SaveChanges();

            context.Kullanici.Add(kullaniciAdmin);

            context.SaveChanges();
        }
    }
}