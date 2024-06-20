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

    #endregion
}