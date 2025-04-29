using Entity_Framework_Core.Data;
using Entity_Framework_Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace Entity_Framework_Core.Controllers
{
    public class KursController : Controller
    {
        private readonly DataContext _context;
        public KursController(DataContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var kurslar = await _context.Kurslar.Include(k=>k.Ogretmen).ToListAsync();
            return View(kurslar);
        }

        public async Task<IActionResult> Create()
        {
        ViewBag.Ogretmenler =new SelectList(await _context.Ogretmenler.ToListAsync(),"OgretmenId","AdSoyad");
          
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KursViewModel model)
        {
            if(ModelState.IsValid)
            {
            _context.Kurslar.Add(new Kurs(){KursId = model.KursId,Baslik=model.Baslik,OgretmenId=model.OgretmenId});
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
            }
            ViewBag.Ogretmenler =new SelectList(await _context.Ogretmenler.ToListAsync(),"OgretmenId","AdSoyad");
            return View(model);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var krs = await _context.Kurslar.Include(k=>k.KursKayitlari).ThenInclude(k=>k.Ogrenci).Select(k=> new KursViewModel{
                KursId=k.KursId,
                Baslik=k.Baslik,
                OgretmenId=k.OgretmenId,
                KursKayitlari=k.KursKayitlari
            }).FirstOrDefaultAsync(k=>k.KursId==id);
            if (krs == null)
            {
                return NotFound();
            }
               ViewBag.Ogretmenler =new SelectList(await _context.Ogretmenler.ToListAsync(),"OgretmenId","AdSoyad");
          
            return View(krs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, KursViewModel model)
        {
            if (id != model.KursId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(new Kurs(){KursId = model.KursId,Baslik=model.Baslik,OgretmenId=model.OgretmenId});
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (!_context.Kurslar.Any(o => o.KursId == model.KursId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(model);

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var krs = await _context.Kurslar.FindAsync(id);

            if (krs == null)
            {
                return NotFound();
            }
            return View(krs);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {

            var krs = await _context.Kurslar.FirstOrDefaultAsync(k => k.KursId == id);
            if (krs == null)
            {
                return NotFound();
            }
            _context.Remove(krs);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}