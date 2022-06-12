using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LearnLanguageDictionaryWebsite.Data;
using LearnLanguagesDictionaryWebsite.Models;

namespace LearnLanguagesDictionaryWebsite.Controllers
{
    public class DictionaryModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DictionaryModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DictionaryModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.DictionaryModel.ToListAsync());
        }

        // GET: DictionaryModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dictionaryModel = await _context.DictionaryModel
                .FirstOrDefaultAsync(m => m.Key == id);
            if (dictionaryModel == null)
            {
                return NotFound();
            }

            return View(dictionaryModel);
        }

        // GET: DictionaryModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DictionaryModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Key,LanguageName")] DictionaryModel dictionaryModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dictionaryModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dictionaryModel);
        }

        // GET: DictionaryModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dictionaryModel = await _context.DictionaryModel.FindAsync(id);
            if (dictionaryModel == null)
            {
                return NotFound();
            }
            return View(dictionaryModel);
        }

        // POST: DictionaryModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Key,LanguageName")] DictionaryModel dictionaryModel)
        {
            if (id != dictionaryModel.Key)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dictionaryModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DictionaryModelExists(dictionaryModel.Key))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(dictionaryModel);
        }

        // GET: DictionaryModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dictionaryModel = await _context.DictionaryModel
                .FirstOrDefaultAsync(m => m.Key == id);
            if (dictionaryModel == null)
            {
                return NotFound();
            }

            return View(dictionaryModel);
        }

        // POST: DictionaryModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dictionaryModel = await _context.DictionaryModel.FindAsync(id);
            _context.DictionaryModel.Remove(dictionaryModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DictionaryModelExists(int id)
        {
            return _context.DictionaryModel.Any(e => e.Key == id);
        }
    }
}
