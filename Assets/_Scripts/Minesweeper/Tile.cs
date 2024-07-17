using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    #region Variables

    public UnityAction<TileState> OnStateChanged { get; set; } = null;
    public TileState State { get; private set; }

    #endregion

    #region Tile Manager

    /**
     * <summary>
     * Reveal tile.
     * </summary>
     */
    public virtual void Reveal()
    {
        State = TileState.Revealed;
        OnStateChanged?.Invoke(State);
    }

    /**
     * <summary>
     * Flag the tile.
     * </summary>
     */
    public void Flag()
    {
        State = TileState.Flagged;
        OnStateChanged?.Invoke(State);
    }

    /**
     * <summary>
     * Switch flag on tile.
     * </summary>
     */
    public void Unflag()
    {
        State = TileState.Hidden;
        OnStateChanged?.Invoke(State);
    }

    public void SwitchFlag()
    {
        if (State != TileState.Revealed)
        {
            if (State == TileState.Flagged)
            {
                Unflag();
            }
            else
            {
                Flag();
            }
        }
    }

    #endregion
}