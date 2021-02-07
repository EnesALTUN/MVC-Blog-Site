using MVCBlogTez.App_Start;
using MVCBlogTez.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace MVCBlogTez.Context
{
    public class BlogContext : DbContext
    {
        public BlogContext() : base("BlogBaglanti")     // Web.config dosyasındaki connectionstring ile bağlantı yapıldı ve veritabanına bağlandı
        {
            Database.SetInitializer(new MyInitializer());   // FakeData için verilerin olduğunu belirttik.
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();      // Tablo isimlerini istediğimiz gibi oluşturmak için ekledik
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();   // İlişkili tablolarda silme işlemi yapmak için ekledik
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();    // İlişkili tablolarda silme işlemi yapmak için ekledik
        }

        public DbSet<Cinsiyet> Cinsiyet { get; set; }
        public DbSet<Durum> Durum { get; set; }
        public DbSet<Duyuru> Duyuru { get; set; }
        public DbSet<Etiket> Etiket { get; set; }
        public DbSet<Kategori> Kategori { get; set; }
        public DbSet<Kullanici> Kullanici { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Slider> Slider { get; set; }
        public DbSet<Yazi> Yazi { get; set; }
        public DbSet<Yorum> Yorum { get; set; }
    }
}