using UnityEngine;
using TMPro;

public class LocalizedText : MonoBehaviour
{
    #region Variable

    [SerializeField] private string key;

    #endregion

    #region Built-In Methods

    private void Start()
    {
        UpdateText();
    }

    #endregion

    #region Update Text

    /**
     * <summary>
     * Update text with language selected.
     * </summary>
     */
    public void UpdateText()
    {
        TMP_Text text = GetComponent<TMP_Text>();

        if (text != null)
        {
            string localizedValue = LanguageManager.instance.GetLocalizedValue(key);
            if (localizedValue != null)
            {
                text.text = localizedValue;
            }
        }
    }

    #endregion
}