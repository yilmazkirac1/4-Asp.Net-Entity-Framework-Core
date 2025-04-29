using Entity_Framework_Core.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Entity_Framework_Core.Controllers
{
    public class OgrenciController : Controller
    {
        private readonly DataContext _context;
        public OgrenciController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var ogrenciler = await _context.Ogrenciler.ToListAsync();
            return View(ogrenciler);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Ogrenci model)
        {
            if (ModelState.IsValid)
            {
                _context.Ogrenciler.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ogr = await _context.Ogrenciler.Include(o => o.KursKayitlari).ThenInclude(o => o.Kurs).FirstOrDefaultAsync(o => o.OgrenciId == id);
            if (ogr == null)
            {
                return NotFound();
            }
            return View(ogr);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ogrenci model)
        {
            if (id != model.OgrenciId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Ogrenciler.Any(o => o.OgrenciId == model.OgrenciId))
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

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ogr = await _context.Ogrenciler.FirstOrDefaultAsync(p => p.OgrenciId == id);

            if (ogr == null)
            {
                return NotFound();
            }
            return View(ogr);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {

            var ogr = await _context.Ogrenciler.FirstOrDefaultAsync(p => p.OgrenciId == id);

            if (ogr == null)
            {
                return NotFound();
            }
            _context.Ogrenciler.Remove(ogr);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}