using System.ComponentModel.DataAnnotations;

namespace Entity_Framework_Core.Data
{
    public class Ogrenci
    {
        [Key]
        public int OgrenciId { get; set; }
        [Required(ErrorMessage = "Lutfen Gecerli Bir Ad Girin")]
        public string? OgrenciAd { get; set; }
        [Required(ErrorMessage = "Lutfen Gecerli Bir Soyad Girin")]
        public string? OgrenciSoyad { get; set; }
        public string AdSoyad
        {
            get
            {
                return this.OgrenciAd + " " + this.OgrenciSoyad;
            }
        }

        [EmailAddress]
        [Required(ErrorMessage = "Lutfen Gecerli Bir Email Girin")]
        public string? Eposta { get; set; }
        [Phone]
        [Required(ErrorMessage = "Lutfen Gecerli Bir Telefon Girin")] public string? Telefon { get; set; }

        public ICollection<KursKayit> KursKayitlari { get; set; } = new List<KursKayit>();
    }
}