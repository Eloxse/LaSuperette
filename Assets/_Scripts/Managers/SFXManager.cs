using UnityEngine;

public class SFXManager : MonoBehaviour
{
    #region Variable

    [Header("Video Monitoring SFX")]
    [SerializeField] private AudioSource sfx_Windows10Startup;
    [SerializeField] private AudioSource sfx_NavigationStart;
    [SerializeField] private AudioSource sfx_ErrorPopUp;

    [Header("Nobert PC SFX")]
    [SerializeField] private AudioSource sfx_Windows3Startup;
    [SerializeField] private AudioSource sfx_ErrorPopUp1, sfx_CorrectPopUp;
    [SerializeField] private AudioSource sfx_MsDos, sfx_MsDosBoot;
    [SerializeField] private AudioSource sfx_Computer;

    //Singleton.
    private static SFXManager _instance;

    #endregion

    #region Properties

    //Video Monitoring SFX.
    public AudioSource Sfx_Windows10Startup => sfx_Windows10Startup;
    public AudioSource Sfx_NavigationStart => sfx_NavigationStart;
    public AudioSource Sfx_ErrorPopUp => sfx_ErrorPopUp;

    //Norbert PC SFX.
    public AudioSource Sfx_Windows3Startup => sfx_Windows3Startup;
    public AudioSource Sfx_ErrorPopUp1 => sfx_ErrorPopUp1;
    public AudioSource Sfx_CorrectPopUp => sfx_CorrectPopUp;
    public AudioSource Sfx_MsDos => sfx_MsDos;
    public AudioSource Sfx_MsDosBoot => sfx_MsDosBoot;
    public AudioSource Sfx_Computer => sfx_Computer;

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