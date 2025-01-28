using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Threading;

namespace NaranjAcademicMagazine
{
    public class LanguageCol
    {
        public static List<Languages> AvailableLanguages = new List<Languages>
        {
            new Languages
            {
                LanguageFullName = "English", LanguageCultureName = "en"
            },
            new Languages
            {
                LanguageFullName = "Pashto", LanguageCultureName = "pa"
            },
        };

        public static bool IsLanguageAvailable(String lang)
        {
            return AvailableLanguages.Where(a => a.LanguageCultureName.Equals(lang)).FirstOrDefault() != null ? true : false;
        }

        public static string GetDefaultLaguage()
        {
            return AvailableLanguages[0].LanguageCultureName;
        }

        public void SetLanguage(String lang)
        {
            try
            {
                if (!IsLanguageAvailable(lang)) lang = GetDefaultLaguage();
                var cultureInfo = new CultureInfo(lang);
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
                HttpCookie langCookie = new HttpCookie("culture", lang);
                langCookie.Expires = DateTime.Now.AddYears(1);
                HttpContext.Current.Response.Cookies.Add(langCookie);
            }
            catch (Exception) { }
        }
    }

    public class Languages
    {
        public string LanguageFullName { get; set; }
        public string LanguageCultureName { get; set; }
    }
}