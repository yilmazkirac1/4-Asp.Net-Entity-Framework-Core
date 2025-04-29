using System.ComponentModel.DataAnnotations;

namespace Entity_Framework_Core.Data
{
    public class Ogretmen
    {
        [Key]
        public int OgretmenId { get; set; }
        [Required(ErrorMessage = "Lutfen Gecerli Bir Ad Girin")]

        public string? Ad { get; set; }
        [Required(ErrorMessage = "Lutfen Gecerli Bir Soyad Girin")]
        public string? Soyad { get; set; }
        public string AdSoyad
        {
            get
            {
                return this.Ad + " " + this.Soyad;
            }
        }
        [EmailAddress]
        [Required(ErrorMessage = "Lutfen Gecerli Bir Email Girin")]
        public string? Eposta { get; set; }
        [Phone]
        [Required(ErrorMessage = "Lutfen Gecerli Bir Telefon Girin")]
        public string? Telefon { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime BaslamaTarihi { get; set; }
        public ICollection<Kurs> Kurslar { get; set; } = new List<Kurs>();
    }
}