using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileInputHandler : MonoBehaviour
{
    #region Variables

    //Handler.
    private float _timePointerDown = 0.0f;
    private bool _isDrag;

    //Singleton.
    private Tile _tile;

    #endregion

    #region Built-In Method

    private void Awake()
    {
        _tile = GetComponent<Tile>();
    }

    #endregion

    #region Handler

    /**
     * <summary>
     * Detect mouse click.
     * </summary>
     */
    public void OnPointerDown(PointerEventData eventData)
    {
        _timePointerDown = Time.time;

        StopAllCoroutines();
        StartCoroutine(DetectLongPressCoroutine());
    }

    /**
     * <summary>
     * Detect long press mouse.
     * </summary>
     */
    IEnumerator DetectLongPressCoroutine()
    {
        float elapsedTime = 0.0f;

        while (true)
        {
            elapsedTime += Time.deltaTime;

            if (_isDrag == false && elapsedTime > 0.5f)
            {
                _tile.SwitchFlag();
                yield break;
            }

            yield return null;
        }
    }

    /**
     * <summary>
     * Give comportement to tile when pointer up.
     * </summary>
     */
    public void OnPointerUp(PointerEventData eventData)
    {
        StopAllCoroutines();

        if (_isDrag)
        {
            _isDrag = false;
            return;
        }

        float currentTime = Time.time;
        float elapsed = currentTime - _timePointerDown;

        //Normal click.
        if (elapsed < 0.5f)
        {
            MessageSystem.Send<ITileClickListener>(listener =>
            {
                listener.OnTileClick(tile);
            });
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        _isDrag = true;
    }

    public void OnDrag(PointerEventData eventData) { }

    #endregion
}