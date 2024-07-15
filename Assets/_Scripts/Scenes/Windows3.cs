using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Windows3 : MonoBehaviour
{
    #region Variables

    [Header("Startup")]
    [SerializeField] private GameObject encryption;
    [SerializeField] private GameObject startupBackground;
    [SerializeField] private GameObject startup, desk;

    [Header("Password Window")]
    [SerializeField] private GameObject passwordWindow;
    [SerializeField] private GameObject errorPopUp, correctPopUp;
    [SerializeField] private Button btn_ExitDoor, btn_validatePassword;

    [Header("Password")]
    [SerializeField] private TMP_InputField if_Password;
    [SerializeField] private string correctCode = "8524";

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
    public void PasswordWindow()
    {
        StartCoroutine(LoadingWindow());
    }

    private IEnumerator LoadingWindow()
    {
        _cursorManager.SetLoadingCursor();
        btn_ExitDoor.interactable = false;
        yield return new WaitForSeconds(_cursorManager.LoadingTime);
        passwordWindow.SetActive(true);
    }

    /**
     * <summary>
     * Button: Closed windows.
     * </summary>
     */
    public void ClosePasswordWindow()
    {
        passwordWindow.SetActive(false);
        btn_ExitDoor.interactable = true;
    }
    public void CloseErrorWindow()
    {
        errorPopUp.SetActive(false);
        btn_validatePassword.interactable = true;
    }

    /**
     * <summary>
     * Button: Validate the password.
     * </summary>
     */
    public void ValidatePasseword()
    {
        _cursorManager .SetLoadingCursor();
        btn_validatePassword.interactable = false;

        //Checking if password is correct.
        string enteredCode = if_Password.text;

        if (enteredCode == correctCode)
        {
            StartCoroutine(LoadCorrectPopUp());
        }
        else
        {
            StartCoroutine(LoadErrorPopUp());
        }
    }

    /**
     * <summary>
     * Loading error pop up.
     * </summary>
     */
    private IEnumerator LoadErrorPopUp()
    {
        yield return new WaitForSeconds(_cursorManager.LoadingTime);
        errorPopUp.SetActive(true);
    }

    /**
     * <summary>
     * Loading correct pop up.
     * </summary>
     */
    private IEnumerator LoadCorrectPopUp()
    {
        yield return new WaitForSeconds(_cursorManager.LoadingTime);
        correctPopUp.SetActive(true);
    }

    #endregion
}