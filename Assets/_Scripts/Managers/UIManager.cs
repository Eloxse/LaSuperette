using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Variables

    [Header("Scenes Manager")]
    [SerializeField] private float timeBeforeLoad = 0.5f;

    [Header("Display Time")]
    [SerializeField] private TMP_Text[] txt_Clocks;

    [Header("Screen Saver")]
    [SerializeField] private GameObject screenSaver;
    [SerializeField] private GameObject unlockScreen;
    [SerializeField] private GameObject desk;

    //Singleton.
    private SFXManager _sfxManager;

    #endregion

    #region Built-In Methods

    private void Start()
    {
        //Singleton.
        _sfxManager = SFXManager.Instance;
    }

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
        string currentTime = DateTime.Now.ToShortTimeString();
        foreach (TMP_Text txt_Clock in txt_Clocks)
        {
            txt_Clock.text = currentTime;
        }
    }

    #endregion

    #region Screen Saver

    /**
     * <summary>
     * Button: switch from screen saver to unlock screen.
     * Coroutine alows time before executing.
     * </summary>
     */
    public void ExitScreenSaver()
    {
        StartCoroutine(DelayExitScreenSaver());
    }

    private IEnumerator DelayExitScreenSaver()
    {
        yield return new WaitForSeconds(timeBeforeLoad);
        screenSaver.SetActive(false);
    }

    /**
     * <summary>
     * Button: connection to the desk.
     * </summary>
     */
    public void DeskConnection()
    {
        StartCoroutine(DelayDeskConnection());
    }

    private IEnumerator DelayDeskConnection()
    {
        _sfxManager.Sfx_WindowsStartup.Play();
        yield return new WaitForSeconds(timeBeforeLoad);

        unlockScreen.SetActive(false);
        desk.SetActive(true);
    }

    #endregion
}