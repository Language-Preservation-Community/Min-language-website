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
    public class VocabularyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VocabularyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vocabulary
        public async Task<IActionResult> Index()
        {
            return View(await _context.VocabularyModel.ToListAsync());
        }

        // GET: Vocabulary/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vocabularyModel = await _context.VocabularyModel
                .Include("RegionalWords")
                .FirstOrDefaultAsync(m => m.Key == id);
            if (vocabularyModel == null)
            {
                return NotFound();
            }

            return View(vocabularyModel);
        }

        // GET: Vocabulary/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vocabulary/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Key,EnglishMeaning,ExampleSentences,AdditionalNote")] VocabularyModel vocabularyModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vocabularyModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vocabularyModel);
        }

        // GET: Vocabulary/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vocabularyModel = await _context.VocabularyModel.FindAsync(id);
            if (vocabularyModel == null)
            {
                return NotFound();
            }
            return View(vocabularyModel);
        }

        // POST: Vocabulary/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Key,EnglishMeaning,ExampleSentences,AdditionalNote")] VocabularyModel vocabularyModel)
        {
            if (id != vocabularyModel.Key)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vocabularyModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VocabularyModelExists(vocabularyModel.Key))
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
            return View(vocabularyModel);
        }

        // GET: Vocabulary/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vocabularyModel = await _context.VocabularyModel
                .FirstOrDefaultAsync(m => m.Key == id);
            if (vocabularyModel == null)
            {
                return NotFound();
            }

            return View(vocabularyModel);
        }

        // POST: Vocabulary/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vocabularyModel = await _context.VocabularyModel.FindAsync(id);
            _context.VocabularyModel.Remove(vocabularyModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VocabularyModelExists(int id)
        {
            return _context.VocabularyModel.Any(e => e.Key == id);
        }
    }
}
