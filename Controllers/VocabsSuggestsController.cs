using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MinLanguage.Data;
using MinLanguage.Models;

namespace MinLanguage.Controllers
{
    public class VocabsSuggestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VocabsSuggestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VocabsSuggests
        public async Task<IActionResult> Index()
        {
            return View(await _context.VocabsSuggest.ToListAsync());
        }

        // GET: VocabsSuggests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vocabsSuggest = await _context.VocabsSuggest
                .FirstOrDefaultAsync(m => m.Key == id);
            if (vocabsSuggest == null)
            {
                return NotFound();
            }

            return View(vocabsSuggest);
        }

        // GET: VocabsSuggests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VocabsSuggests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VocabsKey,EnglishTranslation,RegionUsed,ExampleSentences,WordClass,Category,Key,UserId")] VocabsSuggest vocabsSuggest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vocabsSuggest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vocabsSuggest);
        }

        // GET: VocabsSuggests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vocabsSuggest = await _context.VocabsSuggest.FindAsync(id);
            if (vocabsSuggest == null)
            {
                return NotFound();
            }
            return View(vocabsSuggest);
        }

        // POST: VocabsSuggests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VocabsKey,EnglishTranslation,RegionUsed,ExampleSentences,WordClass,Category,Key,UserId")] VocabsSuggest vocabsSuggest)
        {
            if (id != vocabsSuggest.Key)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vocabsSuggest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VocabsSuggestExists(vocabsSuggest.Key))
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
            return View(vocabsSuggest);
        }

        // GET: VocabsSuggests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vocabsSuggest = await _context.VocabsSuggest
                .FirstOrDefaultAsync(m => m.Key == id);
            if (vocabsSuggest == null)
            {
                return NotFound();
            }

            return View(vocabsSuggest);
        }

        // POST: VocabsSuggests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vocabsSuggest = await _context.VocabsSuggest.FindAsync(id);
            _context.VocabsSuggest.Remove(vocabsSuggest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VocabsSuggestExists(int id)
        {
            return _context.VocabsSuggest.Any(e => e.Key == id);
        }
    }
}
