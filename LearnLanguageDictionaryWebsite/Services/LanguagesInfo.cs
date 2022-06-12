using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LearnLanguagesDictionaryWebsite.Services
{
    public enum AllAvailableLanguages
    {
        // English will be the home page. Whenever the value is chosen, it will return to home
        English,
        Hokkien,
        Hakka,
        Teochew,
        Hainanese,
        Cantonese,
        Hinghwa,
        Hokchew,
        Shanghainese,
    }

    // Most of the function have to be static because it will be shared between controllers and the views
    // It will act as utility
    // We can't do it with depedency injection cause is required all around and it will be in layout.cshtml
    public class LanguagesInfo
    {
        public static readonly string SessionKeySelectedLanguage = "Language";

        // We are not making a new HttpContext, we are just merely accessing the httpContext
        // This is for session. The session only stays per user
        private static HttpContext _httpContext => new HttpContextAccessor().HttpContext;

        // The language name are needs to be constant until we made something new
        // Enum are used for faster iteration

        public static AllAvailableLanguages SelectedLanguage
        {
            get
            {
                var selected = _httpContext.Session.GetString(SessionKeySelectedLanguage);

                // if there isn't anything set in the cache, make it 
                if (!String.IsNullOrEmpty(selected) && Enum.TryParse(selected.ToString(), out AllAvailableLanguages selectedLanguageEnum))
                {
                    return selectedLanguageEnum;
                }
                else
                {
                    _httpContext.Session.SetString(SessionKeySelectedLanguage, AllAvailableLanguages.English.ToString());
                }

                return AllAvailableLanguages.English;
            }
            set
            {
                _httpContext.Session.SetString(SessionKeySelectedLanguage, value.ToString());
            }
        }

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