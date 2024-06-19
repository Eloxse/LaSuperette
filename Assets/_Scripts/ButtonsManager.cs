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

    #endregion

    #region Screen Saver

    /* <summary>
     * Button: switch from screen saver to unlock screen.
     * Coroutine alow a time before executing.
     */
    public void ExitScreenSaver()
    {
        StartCoroutine(DelayExitScreenSaver());
    }

    private IEnumerator DelayExitScreenSaver()
    {
        yield return new WaitForSeconds(timeBeforeLoad);
        screenSaver.SetActive(false);
        unlockScreen.SetActive(true);
    }

    /* <summary>
     * Button: connection to the desk.
     * Coroutine alow a time before executing.
     */
    public void DeskConnection()
    {
        StartCoroutine(DelayDeskConnection());
    }

    private IEnumerator DelayDeskConnection()
    {
        yield return new WaitForSeconds(timeBeforeLoad);

    }

    #endregion
}