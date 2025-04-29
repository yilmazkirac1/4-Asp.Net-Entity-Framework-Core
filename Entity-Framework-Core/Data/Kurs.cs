using System.ComponentModel.DataAnnotations;

namespace Entity_Framework_Core.Data
{
    public class Kurs
    {
        public int KursId { get; set; }
        [Required(ErrorMessage ="Lutfen Gecerli Kurs Adi Girin")]        
        public string? Baslik { get; set; }
        public int OgretmenId { get; set; }
        public Ogretmen Ogretmen { get; set; }=null!;
        public ICollection<KursKayit> KursKayitlari { get; set; }=new List<KursKayit>();
    }
}