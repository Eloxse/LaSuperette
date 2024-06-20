using UnityEngine;
using System;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Variables

    [Header("Display Time")]
    [SerializeField] private TMP_Text txt_Clock;

    #endregion

    #region Built-In Methods

    private void Update()
    {
        DisplayTime();
    }

    #endregion

    #region Current Time

    /**
     * <summary>
     * Display real time.
     * </summary>
     */
    private void DisplayTime()
    {
        txt_Clock.text = DateTime.Now.ToShortTimeString();       //Display time.
    }

    #endregion
}