using LearnLanguageDictionaryWebsite.Data;
using LearnLanguagesDictionaryWebsite.Models;
using LearnLanguagesDictionaryWebsite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnLanguagesDictionaryWebsite.Controllers
{
    public class DictionaryController : Controller
    {
        private readonly ApplicationDbContext _context;

        private List<DictionaryModel> dictionaryList;

        // Constructor
        public DictionaryController(ApplicationDbContext context)
        {
            // var dictionary = new DictionaryModel();
            _context = context;

            dictionaryList = _context.DictionaryModel
                .Include(x => x.AllRegion)
                .Include(x => x.VocabulariesList)
                .ThenInclude(y => y.RegionalWords)
                .Include(x => x.VocabulariesList)
                .ThenInclude(x => x.WordCategory).ToList();
        }

        // We show the list of dictionaries we have
        public IActionResult Index()
        {
            List<DictionaryModel> ListOfDictionaries = _context.DictionaryModel.ToList();

            if (ListOfDictionaries.Count == 0)
            {

            }

            return View(_context.DictionaryModel.ToList());
        }

        // Just a test on putting an own made model to make sure it display
        public IActionResult Random()
        {
            // the code below is just for testing
            var dictionary = new DictionaryModel() { 
                LanguageName = AllAvailableLanguages.Hokkien.ToString()
            };

            var allRegions = new List<RegionModel>() { 
                new RegionModel() { RegionName="RegionA" }, 
                new RegionModel() {RegionName= "RegionB" }, 
                new RegionModel() {RegionName="RegionC" } };

            dictionary.AllRegion = allRegions;

            var regionalPronunciationA = new List<RegionalPronunciationModel>();
            var regionalPronunciationB = new List<RegionalPronunciationModel>();

            var wordCategoryA = new List<CategoryModel>() { new CategoryModel() { Category = "verb" } };
            var wordCategoryB = new List<CategoryModel>() { new CategoryModel() { Category = "adjective" } };

            // Regional Pronunciation for vocab A
            for (int i=0; i< allRegions.Count; i++ )
            {
                regionalPronunciationA.Add(new RegionalPronunciationModel() { Hanji = "哭", Pronunciation="khau"+i });
                regionalPronunciationB.Add(new RegionalPronunciationModel() { Hanji = "哭", Pronunciation = "hau" + i*2 });
            }

            var vocabularyA = new VocabularyModel() 
            { AdditionalNote = "", EnglishMeaning = "VocabA", ExampleSentences = "哭父，恁父母無共汝是毋是?", WordCategory = wordCategoryA, RegionalWords = regionalPronunciationA};
            var vocabularyB = new VocabularyModel() 
            { AdditionalNote = "", EnglishMeaning ="VocabB", ExampleSentences= "伊个頭毛足歹看", WordCategory = wordCategoryB, RegionalWords= regionalPronunciationB};

            var vocabularyList = new List<VocabularyModel>();

            vocabularyList.Add(vocabularyA);
            vocabularyList.Add(vocabularyB);

            dictionary.VocabulariesList = vocabularyList;

            DictionaryModel dictionaryToPass = _context.DictionaryModel.Find(1);

            if (dictionaryToPass != null)
            {
                _context.DictionaryModel.Remove(dictionaryToPass);
                
                // _context.SaveChanges();
            }

            dictionaryToPass = _context.DictionaryModel.Find(1);

            if (dictionaryToPass == null)
            {
                _context.DictionaryModel.Add(dictionary);
                _context.SaveChanges();
            }

            var ehhh = _context.DictionaryModel
                .Include(x => x.AllRegion)
                .Include(x => x.VocabulariesList)
                .ThenInclude(y => y.RegionalWords).ToList();

            dictionaryToPass = ehhh.FirstOrDefault(x => x.LanguageName == dictionary.LanguageName);

            var ahh = _context.DictionaryModel.Select(x => x.VocabulariesList);
            

            return View("ViewDictionary" , dictionaryToPass);
        }

        // Viewing all vocab in a dictionary
        // We might not need this
        public ActionResult ViewDictionary(int id)
        {
            //var dictionaryList = _context.DictionaryModel
            //    .Include(x => x.AllRegion)
            //    .Include(x => x.VocabulariesList)
            //    .ThenInclude(y => y.RegionalWords)
            //    .Include(x => x.VocabulariesList)
            //    .ThenInclude(x => x.WordCategory).ToList();

            var dictionaryToPass = _context.DictionaryModel
                .FirstOrDefault(m => m.Key == id);

            return View("ViewDictionary", dictionaryToPass);
        }

        // Viewing the vocabs
        // This will lead to each individual vocabs
        // We only need the key to reach the ID
        public ActionResult ViewVocabulary(string language, int id)
        {
            var dictionaryToPass = dictionaryList.FirstOrDefault(x => x.LanguageName == language);

            VocabularyModel vocabulary = dictionaryToPass.VocabulariesList.Single(x => x.Key == id);

            return View("ViewDictionary", vocabulary);
        }

        // 
        public ActionResult EditDictionary(string language, int id)
        {

            // Check whether there are a new region
            var dictionaryToPass = dictionaryList.FirstOrDefault(x => x.LanguageName == language);

            VocabularyModel vocabulary = dictionaryToPass.VocabulariesList.Single(x => x.Key == id);

            return View("ViewDictionary", vocabulary);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVocabulary(string language, VocabularyModel vocabulary)
        {
            // Check whether there are a new region
            var dictionaryToEdit = dictionaryList.FirstOrDefault(x => x.LanguageName == language);

            // Check whether it has a new region
            dictionaryToEdit.VocabulariesList.Add(vocabulary);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dictionaryToEdit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                }
            }

            return View("ViewDictionary", vocabulary);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditVocabulary(int id, VocabularyModel vocabulary)
        {
            if (id != vocabulary.Key)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vocabulary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                }
            }

            return View("ViewDictionary", vocabulary);
        }


        // Searching the Dictionary
        // Dictionary/language?=Hokkien+englishMeaning?=Default+hanji?=Default+?pronunciation=?ah1
        // The logic should be wrapped in Services but doing it here will do for now
        public ActionResult SearchDictionary(string language, string englishMeaning, string hanji, string pronunciation)
        {
            List<VocabularyModel> vocabulariesList = new List<VocabularyModel>();

            if (!String.IsNullOrEmpty(englishMeaning))
            {

            }

            if (!String.IsNullOrEmpty(hanji))
            {

            }

            if (!String.IsNullOrEmpty(pronunciation))
            {

            }

            // We will return a result view here
            return View();
        }

        [HttpPost]
        public ActionResult SuggestVocabulary(VocabularyModel vocabulary)
        {


            return View();
        }

    }
}
