using UnityEngine;

public class SFXManager : MonoBehaviour
{
    #region Variable

    [Header("SFX")]
    [SerializeField] private AudioSource sfxWindowsLaunch;

    //Singleton.

    #endregion

    #region Properties

    public AudioSource SfxWindowsLaunch => sfxWindowsLaunch;

    #endregion
}
