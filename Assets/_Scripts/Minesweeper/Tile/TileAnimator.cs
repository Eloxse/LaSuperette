using UnityEngine;

namespace Ilumisoft.Minesweeper
{
    [RequireComponent(typeof(Tile))]
    [RequireComponent(typeof(Animator))]
    public class TileAnimator : MonoBehaviour
    {
        #region Variables

        private Tile _tile;
        private Animator _animator;

        #endregion

        #region Built-In Method

        private void Awake()
        {
            _tile = GetComponent<Tile>();
            _animator = GetComponent<Animator>();

            _tile.OnStateChanged += OnTileStateChanged;
        }

        #endregion

        #region Change Tile State

        private void OnTileStateChanged(TileState state)
        {
            switch (state)
            {
                case TileState.Hidden:
                    _animator.SetTrigger("Unflag");
                    break;
                case TileState.Flagged:
                    _animator.SetTrigger("Flag");
                    break;
                case TileState.Revealed:
                    _animator.SetTrigger("Reveal");
                    break;
            }
        }

        #endregion
    }
}