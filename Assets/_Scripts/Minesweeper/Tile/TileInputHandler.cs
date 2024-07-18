using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ilumisoft.Minesweeper
{
    [RequireComponent(typeof(Tile))]
    public class TileInputHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler
    {
        #region Variables

        private Tile _tile;
        private float timePointerDown = 0.0f;
        private bool isDrag;

        #endregion

        #region Built-In Method

        private void Awake()
        {
            _tile = GetComponent<Tile>();
        }

        #endregion

        #region Pointer

        public void OnPointerDown(PointerEventData eventData)
        {
            timePointerDown = Time.time;

            StopAllCoroutines();
            StartCoroutine(DetectLongPressCoroutine());
        }

        IEnumerator DetectLongPressCoroutine()
        {
            float elapsedTime = 0.0f;

            while (true)
            {
                elapsedTime += Time.deltaTime;

                if (isDrag == false && elapsedTime > 0.5f)
                {
                    _tile.SwitchFlag();
                    yield break;
                }

                yield return null;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            StopAllCoroutines();

            if (isDrag)
            {
                isDrag = false;
                return;
            }

            float currentTime = Time.time;
            float elapsed = currentTime - timePointerDown;

            //Normal click.
            if (elapsed < 0.5f)
            {
                MessageSystem.Send<ITileClickListener>(listener =>
                {
                    listener.OnTileClick(_tile);
                });
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            isDrag = true;
        }

        public void OnDrag(PointerEventData eventData) { }

        #endregion
    }
}