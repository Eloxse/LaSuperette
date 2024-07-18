using UnityEngine;
using TMPro;

namespace Ilumisoft.Minesweeper
{
    [RequireComponent(typeof(Tile))]
    public class TileNumber : MonoBehaviour
    {
        #region Variable

        [SerializeField] private TextMeshProUGUI txt_Number = null;

        #endregion

        #region Set Bomb Number

        public void SetNumberOfBombs(int count)
        {
            if (txt_Number != null)
            {
                this.txt_Number.text = count == 0 ? "" : count.ToString();
            }
        }

        #endregion
    }
}
//tmpro
// txt couleur