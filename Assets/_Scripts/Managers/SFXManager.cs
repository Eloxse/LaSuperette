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

    //SFX.
    public AudioSource SfxWindowsLaunch => sfxWindowsLaunch;

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