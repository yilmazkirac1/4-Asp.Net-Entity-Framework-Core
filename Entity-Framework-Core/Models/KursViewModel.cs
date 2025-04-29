
using System.ComponentModel.DataAnnotations;

namespace Entity_Framework_Core.Models
{
    public class KursViewModel
    {
         public int KursId { get; set; }
         [Required]
         [StringLength(50)]
         [Display(Name ="Kurs Basligi")]
        public string? Baslik { get; set; }
        public int OgretmenId { get; set; }      
       public ICollection<KursKayit> KursKayitlari { get; set; }=new List<KursKayit>();
    }
}