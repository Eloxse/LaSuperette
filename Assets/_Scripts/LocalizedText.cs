using UnityEngine;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour
{
    #region Variable

    public string key;

    #endregion

    #region Built-In Methods

    private void Start()
    {
        UpdateText();
    }
    public void UpdateText()
    {
        Text text = GetComponent<Text>();
        if (text != null)
        {
            string localizedValue = LanguageManager.instance.GetLocalizedValue(key);
            if (localizedValue != null)
            {
                text.text = localizedValue;
            }
            else
            {
                Debug.LogWarning("Localized value not found for key: " + key);
            }
        }
        else
        {
            Debug.LogError("Text component is null on " + gameObject.name);
        }
    }
    #endregion
}