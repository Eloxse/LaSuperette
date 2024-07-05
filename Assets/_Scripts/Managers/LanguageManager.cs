using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class LanguageManager : MonoBehaviour
{
    #region Variables

    //Singleton.
    public static LanguageManager instance;

    //Text localization.
    private Dictionary<string, string> _localizedText;
    private string _missingTextString = "Localized text not found";

    #endregion

    #region Built-In Methods

    private void Awake()
    {
        // Singleton.
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadLanguage();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    #region Loading Language

    /**
     * <summary>
     * Localize JSON file on the explorer.
     * </summary>
     */
    public void LoadLocalizedText(string fileName)
    {
        _localizedText = new Dictionary<string, string>();
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

            for (int i = 0; i < loadedData.items.Length; i++)
            {
                _localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
            }
        }
    }

    public string GetLocalizedValue(string key)
    {
        string result = _missingTextString;
        if (_localizedText.ContainsKey(key))
        {
            result = _localizedText[key];
        }

        return result;
    }

    private void LoadLanguage()
    {
        string languageCode = PlayerPrefs.GetString("selectedLanguage", "french"); // Default to French.
        LoadLocalizedText(languageCode + ".json");
    }
    #endregion
}


/**
 * <summary>
 * Deserialize JSON data.
 * </summary>
 */
[System.Serializable]
public class LocalizationData
{
    public LocalizationItem[] items;
}

[System.Serializable]
public class LocalizationItem
{
    public string key;
    public string value;
}