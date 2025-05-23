using Entity_Framework_Core.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Entity_Framework_Core.Controllers
{
    public class OgretmenController:Controller
    {
          private readonly DataContext _context;
        public OgretmenController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
             return View(await _context.Ogretmenler.ToListAsync());       
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
          public async Task<IActionResult> Create(Ogretmen model)
        {
            if(ModelState.IsValid)
            {
                 _context.Ogretmenler.Add(model);
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
            var ogrt = await _context.Ogretmenler.FirstOrDefaultAsync(o=>o.OgretmenId==id);
            if (ogrt == null)
            {
                return NotFound();
            }
            return View(ogrt);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ogretmen model)
        {
            if (id != model.OgretmenId)
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
                    if (!_context.Ogretmenler.Any(o => o.OgretmenId == model.OgretmenId))
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
            if(id==null)
            {
                return NotFound();
            }
            var ogrt = await _context.Ogretmenler.FirstOrDefaultAsync(o=>o.OgretmenId==id);
            if(ogrt==null)
            {
                 return NotFound();
            }
            return View(ogrt);
        }
        [HttpPost]
         public async Task<IActionResult> Delete(int id)
        {
           
            var ogrt = await _context.Ogretmenler.FirstOrDefaultAsync(o=>o.OgretmenId==id);
            if(ogrt==null)
            {
                 return NotFound();
            }
            _context.Remove(ogrt);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}