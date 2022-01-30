using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MinLanguage.Data;
using MinLanguage.Services;

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
            return View(await _context.Vocabs.ToListAsync());
        }

        // GET: Vocabs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vocabs = await _context.Vocabs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vocabs == null)
            {
                return NotFound();
            }

            return View(vocabs);
        }

        // GET: Vocabs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vocabs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,hanji,englishTranslation,regionUsed,exampleSentences,wordClass,category")] Vocabs vocabs)
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
            if (id != vocabs.Id)
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
                    if (!VocabsExists(vocabs.Id))
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
                .FirstOrDefaultAsync(m => m.Id == id);
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
            return _context.Vocabs.Any(e => e.Id == id);
        }
    }
}
