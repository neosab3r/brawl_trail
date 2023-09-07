using System;
using OLS_HyperCasual;
using UnityEngine;

namespace OLS_HyperCasual
{
    public class LocalizationController : BaseController
    {
        private Locales locale;

        public LocalizationController()
        {
            locale = BaseEntryPoint.Get<ResourcesController>().GetResource<Locales>(ResourceConstants.Localization, false);
        }

        public static string GetLocaleString(string key)
        {
            return BaseEntryPoint.Get<LocalizationController>().GetLocalizationString(key);
        }

        public string GetLocalizationString(string key)
        {
            var value = FindValue(key);
            if (value == null)
            {
                return key;
            }

            if (Application.systemLanguage == SystemLanguage.Russian && string.IsNullOrEmpty(value.RU) == false)
            {
                return value.RU;
            }

            return value.EN;
        }

        private LocalizationExcelData FindValue(string key)
        {
            foreach (var data in locale.AllLocales)
            {
                if (string.Equals(data.Key, key, StringComparison.CurrentCultureIgnoreCase))
                {
                    return data;
                }
            }

            return null;
        }
    }
}