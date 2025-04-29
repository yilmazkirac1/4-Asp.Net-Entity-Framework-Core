using System.Threading.Tasks;
using Entity_Framework_Core.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Entity_Framework_Core.Controllers
{
    public class KursKayitController : Controller
    {
        private readonly DataContext _context;
        public KursKayitController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var kursKayitlari = await _context.KursKayitlari.Include(x => x.Ogrenci).Include(x => x.Kurs).ToListAsync();
            return View(kursKayitlari);
        }


        public async Task<IActionResult> Create()
        {
            ViewBag.Ogrenciler = new SelectList(await _context.Ogrenciler.ToListAsync(), "OgrenciId", "AdSoyad");
            ViewBag.Kurslar = new SelectList(await _context.Kurslar.ToListAsync(), "KursId", "Baslik");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KursKayit model)
        {

            var kontrol= await _context.KursKayitlari.FirstOrDefaultAsync(m=>m.KursId==model.KursId&&m.OgrenciId==model.OgrenciId);
            if(kontrol!=null)
            {
                        ViewBag.Ogrenciler = new SelectList(await _context.Ogrenciler.ToListAsync(), "OgrenciId", "AdSoyad");
            ViewBag.Kurslar = new SelectList(await _context.Kurslar.ToListAsync(), "KursId", "Baslik");
            return View(model);
            }
            if (model.KursId!=0&&model.OgrenciId!=0)
            {
                model.KayitTarihi = DateTime.Now;
                _context.KursKayitlari.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

             ViewBag.Ogrenciler = new SelectList(await _context.Ogrenciler.ToListAsync(), "OgrenciId", "AdSoyad");
            ViewBag.Kurslar = new SelectList(await _context.Kurslar.ToListAsync(), "KursId", "Baslik");
            return View(model);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ogr = await _context.KursKayitlari.Include(o => o.Ogrenci).Include(o => o.Kurs).FirstOrDefaultAsync(o => o.OgrenciId == id);
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            return View(ogr);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
         
            var ogr = await _context.KursKayitlari.FirstOrDefaultAsync(o => o.OgrenciId == id);
            if(ogr==null)
            {
                return NotFound();
            }
            _context.KursKayitlari.Remove(ogr);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}