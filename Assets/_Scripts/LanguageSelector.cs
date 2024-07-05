using UnityEngine;

public class LanguageSelector : MonoBehaviour
{
    #region Custom Method

    /**
     * <summary>
     * Select language.
     * </summary>
     */
    public void SetLanguage(string languageCode)
    {
        LanguageManager.instance.LoadLocalizedText(languageCode + ".json");
        PlayerPrefs.SetString("selectedLanguage", languageCode);
        UpdateAllLocalizedTexts();
    }

    /**
     * <summary>
     * Update text with language selected.
     * </summary>
     */
    private void UpdateAllLocalizedTexts()
    {
        LocalizedText[] localizedTexts = FindObjectsOfType<LocalizedText>();
        foreach (LocalizedText localizedText in localizedTexts)
        {
            localizedText.UpdateText();
        }
    }

    #endregion
}