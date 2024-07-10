using System.Collections;
using UnityEngine;

public class Windows3 : MonoBehaviour
{
    #region Variables

    [Header("Startup")]
    [SerializeField] private GameObject startupBackground;
    [SerializeField] private GameObject startup, desk;

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
     * Active startup background after animation encryption.
     * </summary>
     */
    public void ActiveStartupBackground()
    {
        startupBackground.SetActive(true);
    }

    #endregion

    #region Desk

    public void Desk()
    {
        desk.SetActive(true);
        _cursorManager.SetLoadingCursor();
        startup.SetActive(false);
    }

    #endregion
}