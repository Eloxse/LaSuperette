using UnityEngine;
using System;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Variables

    [Header("Display Time")]
    [SerializeField] private TMP_Text txt_Clock;
    [SerializeField] private TMP_Text txt_Clock1;
    [SerializeField] private TMP_Text txt_Clock2;

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
        txt_Clock.text = DateTime.Now.ToShortTimeString();
        txt_Clock1.text = DateTime.Now.ToShortTimeString();
        txt_Clock2.text = DateTime.Now.ToShortTimeString();
    }

    #endregion
}