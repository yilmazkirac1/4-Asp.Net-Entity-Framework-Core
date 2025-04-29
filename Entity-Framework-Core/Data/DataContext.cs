using Entity_Framework_Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Entity_Framework_Core
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
            
        }
        public DbSet<Kurs> Kurslar=>Set<Kurs>();
        public DbSet<Ogrenci> Ogrenciler=>Set<Ogrenci>();
        public DbSet<KursKayit> KursKayitlari=>Set<KursKayit>();
        public DbSet<Ogretmen> Ogretmenler=>Set<Ogretmen>();

    }   
}