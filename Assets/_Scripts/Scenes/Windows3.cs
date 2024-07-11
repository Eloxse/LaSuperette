using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Windows3 : MonoBehaviour
{
    #region Variables

    [Header("Startup")]
    [SerializeField] private GameObject encryption;
    [SerializeField] private GameObject startupBackground;
    [SerializeField] private GameObject startup, desk;

    [Header("Wordpass Window")]
    [SerializeField] private GameObject window;
    [SerializeField] private Button btn_ExitDoor, btn_Ok;

    //Singleton.
    private static CursorManager _cursorManager;

    #endregion

    #region Built-In Methods

    private void Start()
    {
        //Singleton.
        _cursorManager = CursorManager.Instance;
    }

    #endregion

    #region Windows Startup

    /**
     * <summary>
     * Resume animation encryption.
     * </summary>
     */
    public void ContinueEncryption()
    {
        encryption.SetActive(true);
    }

    /**
     * <summary>
     * Active startup background after animation encryption.
     * </summary>
     */
    public void ActiveStartupBackground()
    {
        startupBackground.SetActive(true);
        encryption.SetActive(false);
    }

    #endregion

    #region Desk

    /**
     * <summary>
     * Unable desk and disable startup.
     * </summary>
     */
    public void Desk()
    {
        desk.SetActive(true);
        _cursorManager.SetLoadingCursor();
        startup.SetActive(false);
    }

    /**
     * <summary>
     * Button: Active wordpass window.
     * </summary>
     */
    public void WordpassWindow()
    {
        StartCoroutine(LoadingWindow());
    }

    private IEnumerator LoadingWindow()
    {
        _cursorManager.SetLoadingCursor();
        btn_ExitDoor.interactable = false;
        yield return new WaitForSeconds(2.5f);
        window.SetActive(true);
    }

    /**
     * <summary>
     * Button: Closed window.
     * </summary>
     */
    public void CloseWindow()
    {
        window.SetActive(false);
        btn_ExitDoor.interactable = true;
    }

    public void OkButton()
    {
        _cursorManager .SetLoadingCursor();
        btn_Ok.interactable = false;
        //if true = porte
        //if false = error pop up
        //btn ok interactable trues
    }

    #endregion
}