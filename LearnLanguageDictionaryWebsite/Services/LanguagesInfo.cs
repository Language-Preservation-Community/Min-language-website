using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LearnLanguagesWebsite.Services
{
    public class LanguagesInfo
    {
        // The language name are needs to be constant until we made something new
        // Enum are used for faster iteration
        public enum AllAvailableLanguages
        {
            // English will be the home page. Whenever the value is chosen, it will return to home
            English,
            Hokkien,
            Teochew,
            Hainanese,
            Cantonese,
            Hinghwa,
            Hokchew
        }

        public static AllAvailableLanguages SelectedLanguage { get; set; }

        public static IEnumerable<SelectListItem> GetAllLanguages { 
            get
            {
                return ProcessLanguage();
            }
        }

        public static string GetSomething
        {
            get
            {
                return "AHHHH";
            }
        }

        public static IEnumerable<SelectListItem> ProcessLanguage()
        {
            IEnumerable<AllAvailableLanguages> values = Enum.GetValues(typeof(AllAvailableLanguages)).Cast<AllAvailableLanguages>();

            IEnumerable<SelectListItem> items = from value in values
                                                select new SelectListItem
                                                {
                                                    Text = value.ToString(),
                                                    Value = value.ToString(),
                                                    Selected = value == SelectedLanguage,
                                                };

            return items;
        }

        public static IEnumerable<SelectListItem> SetSelectedLanguage(AllAvailableLanguages selectedLanguage)
        {
            // IEnumerable are Containers
            IEnumerable<AllAvailableLanguages> values = Enum.GetValues(typeof(AllAvailableLanguages)).Cast<AllAvailableLanguages>();

            SelectedLanguage = selectedLanguage;

            IEnumerable<SelectListItem> items = from value in values
                                                select new SelectListItem
                                                {
                                                    Text = value.ToString(),
                                                    Value = value.ToString(),
                                                    Selected = value == SelectedLanguage,
                                                };

            return items;
        }
    }
}