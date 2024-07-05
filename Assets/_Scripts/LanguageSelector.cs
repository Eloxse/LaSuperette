using UnityEngine;

public class LanguageSelector : MonoBehaviour
{
    public static LanguageSelector instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #region Custom Method

    //Appeler la fonction SetLanguage par la Quuen Board.

    /**
     * <summary>
     * Select language.
     * </summary>
     */


    #endregion
}