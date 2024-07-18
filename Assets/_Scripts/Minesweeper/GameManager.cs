using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ilumisoft.Minesweeper
{
    public class GameManager : MonoBehaviour, ITileClickListener
    {
        #region Variables

        [Header("Grid")]
        [SerializeField] private TileGrid tileGrid;
        [SerializeField] private Tile normalTilePrefab, bombTilePrefab;
        [SerializeField] private int bombCount = 5;

        [Header("UI")]
        [SerializeField] private GameObject gameMenu;
        [SerializeField] private TextMeshProUGUI txt_Flag;
        [SerializeField] private GameObject img_GameOver, img_OnTileClick, img_Win;

        [Header("Timer")]
        [SerializeField] private TextMeshProUGUI txt_Timer;

        //Grid.
        private GameObject _tileContainer = null;
        private List<Tile> _tiles = new List<Tile>();

        //UI.
        private bool _isGameOver = false;

        //Timer.
        private float _elapsedTime = 0f;
        private bool _gameRunning = true;

        //Singleton.
        private static GameManager _instance;

        #endregion

        #region Properties

        public static GameManager Instance => _instance;

        #endregion

        #region Built_In Methods

        private void Awake()
        {
            //Grid.
            _tileContainer = new GameObject("Tile Container");

            //Singleton.
            if (_instance) Destroy(this);
            _instance = this;
        }

        void Start()
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
            AddNormalTilesToGrid();
            AssignBombNumbers();
        }

        /**
         * <summary>
         * Adds the bomb tiles to the grid.
         * </summary>
         */
        private void AddBombsToGrid()
        {
            // Make sure the number of bombs is not larger than the grid size.
            bombCount = Mathf.Min(bombCount, tileGrid.Width * tileGrid.Height);

            for (int i = 0; i < bombCount; i++)
            {
                // Calculate a random position.
                int x = Random.Range(0, tileGrid.Width);
                int y = Random.Range(0, tileGrid.Height);

                // Add a bomb if no tile has been assigned yet to the position.
                if (tileGrid.TryGetTile(x, y, out _) == false)
                {
                    AddTileToGrid(x, y, bombTilePrefab);
                }
                // Otherwise our random position is already in use and we need to get a new one.
                else
                {
                    i--;
                }
            }
        }

        /**
         * <summary>
         * Fills all empty grid cells with normal tiles.
         * </summary>
         */
        private void AddNormalTilesToGrid()
        {
            for (int y = 0; y < tileGrid.Height; y++)
            {
                for (int x = 0; x < tileGrid.Width; x++)
                {
                    // If no (bomb) tile is already set, add a normal tile.
                    if (tileGrid.TryGetTile(x, y, out _) == false)
                    {
                        AddTileToGrid(x, y, normalTilePrefab);
                    }
                }
            }
        }

        /**
         * <summary>
         * Assigns to each tile the number of surrounding bombs.
         * </summary>
         */
        private void AssignBombNumbers()
        {
            foreach (var tile in _tiles)
            {
                if (tile.TryGetComponent<TileNumber>(out var tileNumber))
                {
                    tileNumber.SetNumberOfBombs(tileGrid.GetNumberOfSurroundingBombs(tile));
                }
            }
        }

        /**
         * <summary>
         * Creates an instance of the given tile prefab and adds it to the given grid position.
         * </summary>
         */
        void AddTileToGrid(int x, int y, Tile prefab)
        {
            var position = tileGrid.GetWorldPosition(x, y);

            var tile = Instantiate(prefab, position, Quaternion.identity);
            tile.transform.SetParent(_tileContainer.transform);
            tileGrid.SetTile(x, y, tile);

            _tiles.Add(tile);
        }

        /**
         * <summary>
         * On click behaviour.
         * </summary>
         */
        public void OnTileClick(Tile tile)
        {
            if (_isGameOver)
            {
                return;
            }

            StartCoroutine(SmileyState());

            if (tile.State == TileState.Hidden)
            {
                TileRevealer tileRevealer = new TileRevealer(tileGrid);

                tileRevealer.Reveal(tile);

                if (tile.CompareTag(Bomb.Tag))
                {
                    CheckingGameOver(won: false);
                }
                else if (HasRevealedAllSafeTiles())
                {
                    CheckingGameOver(won: true);
                }
            }
        }
        bool HasRevealedAllSafeTiles()
        {
            foreach (var tile in _tiles)
            {
                if (tile.CompareTag(Bomb.Tag) == false && tile.State != TileState.Revealed)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region UI

        /**
         * <summary>
         * Stop game if it's game over.
         * </summary>
         */
        private void CheckingGameOver(bool won = true)
        {
            if (won)
            {
                //Do nothing.
            }
            else
            {
                GameOver();
            }
        }

        /**
         * <summary>
         * Stop game if it's game over.
         * </summary>
         */
        public void GameOver()
        {
            StopTimer();
            img_GameOver.SetActive(true);
            _isGameOver = true;
        }

        /**
         * <summary>
         * Change smiley state on click.
         * </summary>
         */
        private IEnumerator SmileyState()
        {
            img_OnTileClick.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            img_OnTileClick.SetActive(false);
        }

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
                    GameOver();
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

        //game over
        //button
    }
}