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

            var vocabsSuggest = await GetVocabsSuggestWithRegionalNoTrack((int)id);
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
                if (vocabsSuggest.RegionalPronunciations == null)
                {
                    vocabsSuggest.RegionalPronunciations = new List<RegionalSuggest>();
                }
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
            var vocab = await GetVocabsWithRegionalNoTrack(id);
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
                if (vocabsSuggest.RegionalPronunciations == null)
                {
                    vocabsSuggest.RegionalPronunciations = new List<RegionalSuggest>();
                }
                return View(vocabsSuggest);
            }

            vocabsSuggest.UserId = await GetUserId();
            _context.Add(vocabsSuggest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> SuggestRegional(int id)
        {
            var vocabsSuggest = await GetVocabsSuggestNoTrack(id);
            if (vocabsSuggest == null || vocabsSuggest.UserId != await GetUserId())
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuggestRegional(int id, RegionalSuggest regionalSuggest)
        {
            var vocabsSuggest = await GetVocabsSuggestWithRegional(id);
            if (vocabsSuggest == null || vocabsSuggest.UserId != await GetUserId())
            {
                return NotFound();
            }
            regionalSuggest.RegionalKey = null;
            vocabsSuggest.RegionalPronunciations.Add(regionalSuggest);
            _context.Update(vocabsSuggest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: VocabsSuggests/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var vocabsSuggest = await GetVocabsSuggestWithRegionalNoTrack(id);
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
            if (!ModelState.IsValid || id != vocabsSuggest.Key || vocabsSuggest.RegionalPronunciations == null)
            {
                return View(vocabsSuggest);
            }
            // tracking will result in both original and vocabsSuggest being tracked
            var original = await GetVocabsSuggestWithRegionalNoTrack(id);
            if (original == null || original.UserId != await GetUserId())
            {
                return View(vocabsSuggest);
            }
            var origKeys = original.RegionalPronunciations.ConvertAll(r => r.Key);
            var newKeys = vocabsSuggest.RegionalPronunciations.ConvertAll(r => r.Key);
            if (!ListValid(origKeys, newKeys, false))
            {
                return View(vocabsSuggest);
            }

            vocabsSuggest.UserId = await GetUserId();
            var originalRegionalKeys = original.RegionalPronunciations.ToDictionary(r => r.Key, r => r.RegionalKey);
            foreach (var regional in vocabsSuggest.RegionalPronunciations)
            {
                regional.RegionalKey = originalRegionalKeys[regional.Key];
            }
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

            var vocabsSuggest = await GetVocabsSuggestWithRegionalNoTrack((int)id);
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
            var vocab = await GetVocabsWithRegionalNoTrack((int)vocabsSuggest.VocabsKey);
            if (vocab == null)
            {
                return false;
            }
            var originalKeys = vocab.RegionalPronunciations.ConvertAll<int?>(r => r.Key);
            var newKeys = vocabsSuggest.RegionalPronunciations.ConvertAll(r => r.RegionalKey);
            if (!ListValid(originalKeys, newKeys, true))
            {
                return false;
            }
            return true;
        }

        // Checks if each element in new is (either null or) in original
        // and if new has no repeats
        private static bool ListValid<E>(IEnumerable<E> origList, IEnumerable<E> newList, bool nullAllowed)
        {
            var origSet = origList.ToHashSet();
            var newSet = new HashSet<E>();
            foreach (var newItem in newList)
            {
                if (newItem == null)
                {
                    if (!nullAllowed)
                    {
                        return false;
                    }
                }
                else
                {
                    if (!origSet.Contains(newItem) || newSet.Contains(newItem))
                    {
                        return false;
                    }
                    newSet.Add(newItem);
                }
            }
            return true;
        }

        private Task<VocabsSuggest> GetVocabsSuggestNoTrack(int id) => _context.VocabsSuggest
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Key == id);

        private Task<VocabsSuggest> GetVocabsSuggestWithRegional(int id) => _context.VocabsSuggest
            .Include(m => m.RegionalPronunciations)
            .FirstOrDefaultAsync(m => m.Key == id);

        private Task<VocabsSuggest> GetVocabsSuggestWithRegionalNoTrack(int id) => _context.VocabsSuggest
            .AsNoTracking()
            .Include(m => m.RegionalPronunciations)
            .FirstOrDefaultAsync(m => m.Key == id);

        private Task<Vocabs> GetVocabsWithRegionalNoTrack(int id) => _context.Vocabs
            .AsNoTracking()
            .Include(m => m.RegionalPronunciations)
            .FirstOrDefaultAsync(m => m.Key == id);

        private bool VocabsSuggestExists(int id) => _context.VocabsSuggest.Any(e => e.Key == id);

        private async Task<string> GetUserId() => (await _userManager.GetUserAsync(HttpContext.User)).Id;
    }
}
