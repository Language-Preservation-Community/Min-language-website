using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> _userManager;

        public VocabsSuggestsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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

        // GET: VocabsSuggests/SuggestNew
        public IActionResult SuggestNew()
        {
            return View();
        }

        // POST: VocabsSuggests/SuggestNew
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuggestNew(VocabsSuggest vocabsSuggest)
        {
            if (!ModelState.IsValid)
            {
                return View(vocabsSuggest);
            }
            vocabsSuggest.VocabsKey = null;
            vocabsSuggest.UserId = (await _userManager.GetUserAsync(HttpContext.User)).Id;
            _context.Add(vocabsSuggest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> SuggestChange(int id)
        {
            var vocab = await GetVocabsWithRegional(id);
            if (vocab == null)
            {
                return NotFound();
            }
            return View(VocabsSuggest.FromVocabs(vocab));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuggestChange(VocabsSuggest vocabsSuggest)
        {
            if (!ModelState.IsValid || !await IsVocabValid(vocabsSuggest))
            {
                return View(vocabsSuggest);
            }
            vocabsSuggest.UserId = await GetUserId();
            _context.Add(vocabsSuggest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: VocabsSuggests/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var vocabsSuggest = await GetVocabsSuggestWithRegional(id);
            if (vocabsSuggest == null || vocabsSuggest.UserId != await GetUserId())
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
        public async Task<IActionResult> Edit(int id, VocabsSuggest vocabsSuggest)
        {
            if (!ModelState.IsValid || id != vocabsSuggest.Key)
            {
                return View(vocabsSuggest);
            }
            var original = await GetVocabsSuggestWithRegional(id);
            if (original == null || original.UserId != await GetUserId() || !await IsVocabValid(vocabsSuggest))
            {
                return View(vocabsSuggest);
            }

            vocabsSuggest.UserId = await GetUserId();
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

        // GET: VocabsSuggests/Approve/5
        public async Task<IActionResult> Approve(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vocabsSuggest = await GetVocabsSuggestWithRegional((int)id);
            if (vocabsSuggest == null)
            {
                return NotFound();
            }

            return View(vocabsSuggest);
        }

        // POST: VocabsSuggests/Approve/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id)
        {
            var vocabsSuggest = await GetVocabsSuggestWithRegional(id);
            if (vocabsSuggest.VocabsKey == null)
            {
                _context.Add(vocabsSuggest.GetVocabs());
            }
            else
            {
                _context.Update(vocabsSuggest.GetVocabs());
            }
            _context.RemoveRange(vocabsSuggest.RegionalPronunciations);
            _context.Remove(vocabsSuggest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> IsVocabValid(VocabsSuggest vocabsSuggest)
        {
            if (vocabsSuggest.VocabsKey == null)
            {
                return false;
            }
            var vocab = await GetVocabsWithRegional((int)vocabsSuggest.VocabsKey);
            if (vocab == null)
            {
                return false;
            }
            var regionalKeys = vocab.RegionalPronunciations.ConvertAll(r => r.Key).ToHashSet();
            foreach (var regional in vocabsSuggest.RegionalPronunciations)
            {
                if (!regionalKeys.Contains(regional.RegionalKey))
                {
                    return false;
                }
            }
            return true;
        }

        private Task<Vocabs> GetVocabsWithRegional(int id) => _context.Vocabs
            .Include(m => m.RegionalPronunciations)
            .FirstOrDefaultAsync(m => m.Key == id);

        private Task<VocabsSuggest> GetVocabsSuggestWithRegional(int id) => _context.VocabsSuggest
            .Include(m => m.RegionalPronunciations)
            .FirstOrDefaultAsync(m => m.Key == id);

        private bool VocabsSuggestExists(int id) => _context.VocabsSuggest.Any(e => e.Key == id);

        private async Task<string> GetUserId() => (await _userManager.GetUserAsync(HttpContext.User)).Id;
    }
}
