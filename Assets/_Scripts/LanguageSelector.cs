using UnityEngine;

public class LanguageSelector : MonoBehaviour
{
    #region Custom Method

    public void SetLanguage(string languageCode)
    {
        LanguageManager.instance.LoadLocalizedText(languageCode + ".json");
        PlayerPrefs.SetString("selectedLanguage", languageCode); 
        UpdateAllLocalizedTexts();
    }

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