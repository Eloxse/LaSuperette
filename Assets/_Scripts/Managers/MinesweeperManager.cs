using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MinesweeperManager : MonoBehaviour
{
    #region Variables

    [Header("Grid")]
   // [SerializeField] private TileGrid tileGrid;
   // [SerializeField] private Tile normalTilePrefab, bombTilePrefab;
    [SerializeField] private int bombCount = 5;

    [Header("UI")]
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private TextMeshProUGUI txt_Score;
    [SerializeField] private GameObject img_GameOver, img_OnTileClick, img_Win;

    [Header("Timer")]
    [SerializeField] private TextMeshProUGUI txt_Timer;

    //Grid.
    private GameObject _tileContainer = null;
    //private List<Tile> _tiles = new List<Tile>();

    //UI.
    private bool _isGameOver = false;

    //Timer.
    private float _elapsedTime = 0f;
    private bool _gameRunning = true;

    //Singleton.
    private static MinesweeperManager _instance;

    #endregion

    #region Properties

    public static MinesweeperManager Instance => _instance;

    #endregion

    #region Built-In Methods

    private void Awake()
    {
        //Grid.
        _tileContainer = new GameObject("Tile Container");

        //Singleton.
        if (_instance) Destroy(this);
        _instance = this;
    }

    private void Start()
    {
        CreateBoard();
    }

    private void Update()
    {
        RunningTimer();
    }

    #endregion

    #region Grid

    /**
     * <summary>
     * Instantiate board.
     * </summary>
     */
    private void CreateBoard()
    {
        AddBombsToGrid();
        //AddNormalTilesToGrid();
        //AssignBombNumbers();
    }

    /**
     * <summary>
     * Adds the bomb tiles to the grid.
     * </summary>
     */
    private void AddBombsToGrid()
    {
        // Make sure the number of bombs is not larger than the grid size.
        //bombCount = Mathf.Min(bombCount, tileGrid.Width * tileGrid.Height);

        for (int i = 0; i < bombCount; i++)
        {
           // int x = Random.Range(0, tileGrid.Width);
           // int y = Random.Range(0, tileGrid.Height);

            // Add a bomb if no tile has been assigned yet to the position.
          /*  if (tileGrid.TryGetTile(x, y, out _) == false)
            {
                //AddTileToGrid(x, y, bombTilePrefab);
            }
            // Otherwise our random position is already in use and we need to get a new one.
            else
            {
                i--;
            }*/
        }
    }
    #endregion

    #region UI

    /**
     * <summary>
     * Restart the current game.
     * </summary>
     */
    public void Retry()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    /**
     * <summary>
     * Button: Game menu.
     * </summary>
     */
    public void EnableGameMenu()
    {
        gameMenu.SetActive(true);
    }
    public void DisableGameMenu()
    {
        gameMenu.SetActive(false);
    }

    /**
     * <summary>
     * Button: Change level difficulty.
     * </summary>
     */
    public void LoadBeginnerLevel()
    {
        SceneManager.LoadScene("Minesweeper_Easy", LoadSceneMode.Single);
    }
    public void LoadIntermediateLevel()
    {
        SceneManager.LoadScene("Minesweeper_Medium", LoadSceneMode.Single);
    }
    public void LoadExpertLevel()
    {
        SceneManager.LoadScene("Minesweeper_Hard", LoadSceneMode.Single);
    }

    /**
     * <summary>
     * Button: Close the game.
     * </summary>
     */
    public void CloseGame()
    {
        SceneManager.LoadScene("NorbertPc", LoadSceneMode.Single);
    }

    #endregion

    #region Timer

    /**
     * <summary>
     * Display and run timer.
     * </summary>
     */
    private void RunningTimer()
    {
        if (_gameRunning)
        {
            _elapsedTime += Time.deltaTime;
            int displayedTime = Mathf.Min((int)_elapsedTime, 999);
            txt_Timer.text = displayedTime.ToString("000");

            if (displayedTime >= 999)
            {
                //game over..
            }
        }
    }

    /**
     * <summary>
     * Stop timer.
     * </summary>
     */
    public void StopTimer()
    {
        _gameRunning = false;
    }


    #endregion
}