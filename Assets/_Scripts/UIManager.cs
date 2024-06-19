using UnityEngine;
using System;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Variables

    [Header("Display Time")]
    [SerializeField] private TMP_Text txt_Clock;
    [SerializeField] private TMP_Text txt_Date;

    #endregion

    #region Built-In Methods

    private void Update()
    {
        DisplayTime();
    }

    #endregion

    #region Current Time

    /* <summary>
     * Display real clock and date.
     */
    private void DisplayTime()
    {
        txt_Clock.text = DateTime.Now.ToShortTimeString();       //Display time.
        txt_Date.text = DateTime.Now.ToLongDateString();        //Display date.
    }

    #endregion
}