using System.ComponentModel.DataAnnotations;
using Entity_Framework_Core.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Entity_Framework_Core
{
    public class KursKayit
    {
        [Key]
        public int KursKayitId { get; set; }
        public int OgrenciId { get; set; }
        public Ogrenci Ogrenci { get; set; }=null!;
        public int KursId { get; set; }
        public Kurs Kurs { get; set; }=null!;
        public DateTime KayitTarihi { get; set; }
    }
}