using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MinLanguage.Data;
using MinLanguage.Models;

namespace MinLanguage.Controllers
{
    public class VocabsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VocabsController(ApplicationDbContext context)
        {
            _context = context;
            
        }

        // GET: Vocabs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vocabs.Include(m => m.regionalPronunciations).ToListAsync());
        }

        // GET: Vocabs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vocabs = await _context.Vocabs
                .Include(m => m.regionalPronunciations)
                .FirstOrDefaultAsync(m => m.key == id);
            if (vocabs == null)
            {
                return NotFound();
            }

            return View(vocabs);
        }

        // GET: Vocabs/Create
        [Authorize(Roles = "test")]
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult RandomSite()
        {
            var test = _context.Vocabs.FirstOrDefault();
            
            var regionA = new regionalPronunciation
            {
                name = "someregion",
                pronunciation = "AHHHH1"
            };
            var regionalPronunciation = new List<regionalPronunciation>() { regionA };

            if (test == null)
            {
                test = new Vocabs
                {
                    hanji = "AHAHAHAHAH",
                    englishTranslation = "",
                    regionUsed = "",
                    exampleSentences = "",
                    wordClass = "",
                    category = "",
                    regionalPronunciations = regionalPronunciation
                };
            }

            Debug.Write(test.hanji);

            return View();
        }

        // POST: Vocabs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "test")]
        public async Task<IActionResult> Create([Bind("Id,hanji,englishTranslation,regionUsed,exampleSentences,wordClass,category,regionalPronunciations")] Vocabs vocabs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vocabs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vocabs);
        }

        // GET: Vocabs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vocabs = await _context.Vocabs.FindAsync(id);
            if (vocabs == null)
            {
                return NotFound();
            }
            return View(vocabs);
        }

        // POST: Vocabs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,hanji,englishTranslation,regionUsed,exampleSentences,wordClass,category")] Vocabs vocabs)
        {
            if (id != vocabs.key)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vocabs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VocabsExists(vocabs.key))
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
            return View(vocabs);
        }

        // GET: Vocabs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vocabs = await _context.Vocabs
                .FirstOrDefaultAsync(m => m.key == id);
            if (vocabs == null)
            {
                return NotFound();
            }

            return View(vocabs);
        }

        // POST: Vocabs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vocabs = await _context.Vocabs.FindAsync(id);
            _context.Vocabs.Remove(vocabs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VocabsExists(int id)
        {
            return _context.Vocabs.Any(e => e.key == id);
        }
    }
}
