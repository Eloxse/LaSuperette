using UnityEngine;

public class TileGrid : MonoBehaviour
{
    #region Variables

    [Header("Grid Manager")]
    [SerializeField] private int width = 5;
    [SerializeField] private int height = 5;
    [SerializeField] private float cellSize = 1.0f;

    //Singleton.
    private Tile[,] _tiles;

    #endregion

    #region Properties

    public int Width { get => this.width; }
    public int Height { get => this.height; }
    public float CellSize { get => this.cellSize; set => this.cellSize = value; }
    public Vector3 BottomLeftCorner => transform.position - new Vector3(Width - 1, Height - 1, 0) / 2 * CellSize;

    #endregion

    #region Built-In Method

    private void Awake()
    {
        _tiles = new Tile[width, height];
    }

    #endregion

    #region Grid Manager

    /**
     * <summary>
     * Checking if it's a valid position.
     * </summary>
     */
    public bool IsValidTilePosition(int x, int y)
    {
        return x >= 0 && x < Width && y >= 0 && y < Height;
    }

    /**
     * <summary>
     * Instantiate tile.
     * </summary>
     */
    public void SetTile(int x, int y, Tile tile)
    {
        _tiles[x, y] = tile;
    }

    /**
     * <summary>
     * Get tile informations.
     * </summary>
     */
    public bool TryGetTile(Vector2Int gridPos, out Tile tile)
    {
        return TryGetTile(gridPos.x, gridPos.y, out tile);
    }
    public bool TryGetTile(int x, int y, out Tile tile)
    {
        tile = null;

        if (!IsValidTilePosition(x, y))
        {
            return false;
        }

        if (_tiles[x, y] != null)
        {
            tile = _tiles[x, y];
            return true;
        }

        return false;
    }

    /**
     * <summary>
     * Get grid position.
     * </summary>
     */
    public bool TryGetGridPosition(Tile tile, out Vector2Int gridPosition)
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (tile == _tiles[x, y])
                {
                    gridPosition = new Vector2Int(x, y);

                    return true;
                }
            }
        }

        gridPosition = default;
        return false;
    }
    public Vector3 GetWorldPosition(int x, int y)
    {
        return BottomLeftCorner + new Vector3(x, y, 0) * CellSize;
    }
    private void OnDrawGizmos()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                Vector3 position = BottomLeftCorner + new Vector3(x, y, 0);

                Gizmos.DrawWireCube(position, Vector3.one * CellSize);
            }
        }
    }

    #endregion
}