using System.Collections;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    #region Variables

    [Header("Cursor Texture")]
    [SerializeField] private Texture2D defaultCursor;
    [SerializeField] private Texture2D loadingCursor;

    [Header("Loading Time")]
    [SerializeField] private float loadingTime = 1f;

    //Singleton.
    private static CursorManager _instance;

    #endregion

    #region Properties

    public static CursorManager Instance => _instance;

    #endregion

    #region Built-In Method

    private void Awake()
    {
        //Singleton.
        if(_instance) Destroy(this);
        _instance = this;
    }

    private void Start()
    {
        SetDefaultCursor();
    }

    #endregion

    #region Cursor
    
    /**
     * <summary>
     * Set appearance of default cursor.
     * </summary>
     */
    private void SetDefaultCursor()
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

    /**
     * <summary>
     * Set appearance of cursor when loading.
     * </summary>
     */
    public void SetLoadingCursor()
    {
        StartCoroutine(LoadingDelay());
    }

    private IEnumerator LoadingDelay()
    {
        Cursor.SetCursor(loadingCursor, Vector2.zero, CursorMode.Auto);
        yield return new WaitForSeconds(loadingTime);
        SetDefaultCursor();
    }

    #endregion

}