using System.Collections;
using UnityEngine;

public class ButtonsManager : MonoBehaviour
{
    #region Variables

    [Header("Manager")]
    [SerializeField] private float timeBeforeLoad = 0.5f;

    [Header("Screen Saver")]
    [SerializeField] private GameObject screenSaver;
    [SerializeField] private GameObject unlockScreen;
    [SerializeField] private GameObject desk;

    [Header("Video Monitoring")]
    [SerializeField] private GameObject monitoringSoftware;
    [SerializeField] private GameObject vidTechnicalArea;
    [SerializeField] private GameObject vidNorbertDesk;
    [SerializeField] private GameObject vidSupermarket;

    //Singleton.
    private SFXManager _sfxManager;

    #endregion

    #region Built-In Method

    private void Start()
    {
        //Singleton.
        _sfxManager = SFXManager.Instance;
    }

    #endregion

    #region Screen Saver

    /**
     * <summary>
     * Button: switch from screen saver to unlock screen.
     * Coroutine alows time before executing.
     * </summary>
     */
    public void ExitScreenSaver()
    {
        StartCoroutine(DelayExitScreenSaver());
    }

    private IEnumerator DelayExitScreenSaver()
    {
        yield return new WaitForSeconds(timeBeforeLoad);
        screenSaver.SetActive(false);
    }

    /**
     * <summary>
     * Button: connection to the desk.
     * </summary>
     */
    public void DeskConnection()
    {
        StartCoroutine(DelayDeskConnection());
    }

    private IEnumerator DelayDeskConnection()
    {
        _sfxManager.SfxWindowsLaunch.Play();
        yield return new WaitForSeconds(timeBeforeLoad);

        unlockScreen.SetActive(false);
        desk.SetActive(true);
    }

    #endregion

    #region Video Monitoring

    /**
     * <summary>
     * Button: open video monitoring software.
     * </summary>
     */
    public void OpenVideoMonitoring()
    {
        StartCoroutine(DelayOpenSoftware());
    }

    private IEnumerator DelayOpenSoftware()
    {
        yield return new WaitForSeconds(timeBeforeLoad);
        monitoringSoftware.SetActive(true);
    }

    public void PlayVidTechnicalArea()
    {
        vidTechnicalArea.SetActive(true);

        vidNorbertDesk.SetActive(false);
        vidSupermarket.SetActive(false);
    }

    public void PlayVidNorbertDesk()
    {
        vidNorbertDesk.SetActive(true);

        vidTechnicalArea.SetActive(true);
        vidSupermarket.SetActive(false);
    }

    public void PlayVidSupermarket()
    {
        vidSupermarket.SetActive(true);

        vidTechnicalArea.SetActive(false);
        vidNorbertDesk.SetActive(false);
    }
    #endregion
}