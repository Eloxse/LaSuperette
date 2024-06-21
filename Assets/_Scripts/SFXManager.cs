using UnityEngine;

public class SFXManager : MonoBehaviour
{
    #region Variable

    [Header("SFX")]
    [SerializeField] private AudioSource sfxWindowsLaunch;

    //Singleton.
    private static SFXManager _instance;

    #endregion

    #region Properties

    //Singleton.
    public static SFXManager Instance => _instance;

    //SFX.
    public AudioSource SfxWindowsLaunch => sfxWindowsLaunch;

    #endregion

    #region Built-In Methods

    private void Awake()
    {
        // Singleton.
        if (_instance) Destroy(this);
        _instance = this;
    }

    #endregion
}
