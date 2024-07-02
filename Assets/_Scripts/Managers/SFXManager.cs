using UnityEngine;

public class SFXManager : MonoBehaviour
{
    #region Variable

    [Header("Windows Desk SFX")]
    [SerializeField] private AudioSource sfx_WindowsStartup;
    [SerializeField] private AudioSource sfx_NavigationStart;
    [SerializeField] private AudioSource sfx_ErrorPopUp;

    //Singleton.
    private static SFXManager _instance;

    #endregion

    #region Properties

    //Windows Desk SFX.
    public AudioSource Sfx_WindowsStartup => sfx_WindowsStartup;
    public AudioSource Sfx_NavigationStart => sfx_NavigationStart;
    public AudioSource Sfx_ErrorPopUp => sfx_ErrorPopUp;

    //Singleton.
    public static SFXManager Instance => _instance;

    #endregion

    #region Built-In Method

    private void Awake()
    {
        // Singleton.
        if (_instance) Destroy(this);
        _instance = this;
    }

    #endregion
}