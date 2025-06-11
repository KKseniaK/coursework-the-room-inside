using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LanguageSwitcher : MonoBehaviour
{
    public void SetEnglish()
    {
        SetLocale("en-US");
    }

    public void SetRussian()
    {
        SetLocale("ru");
    }

    private void SetLocale(string localeCode)
    {
        var selectedLocale = LocalizationSettings.AvailableLocales.Locales
            .Find(locale => locale.Identifier.Code == localeCode);

        if (selectedLocale != null)
        {
            LocalizationSettings.SelectedLocale = selectedLocale;
            Debug.Log($"Locate is set - {selectedLocale}");
        }
        else
        {
            Debug.LogWarning("Locale not found: " + localeCode);
        }
    }
}

