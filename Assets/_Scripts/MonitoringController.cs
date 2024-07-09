using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

public class MonitoringController : MonoBehaviour
{
    #region Variables

    [Header("Scenes Manager")]
    [SerializeField] private float timeBeforeLoad = 0.5f;

    [Header("Monitoring Software")]
    [SerializeField] private GameObject monitoringSoftware;
    [SerializeField] private Button btn_VideoMonitoring;
    [SerializeField] private GameObject planDownstairs, planUpstairs;

    [Header("Video Manager")]
    [SerializeField] private Slider slid_TimeProgress;
    [SerializeField] private TextMeshProUGUI txt_ElapsedTime, txt_RemainingTime;
    [SerializeField] private GameObject vid_AudioVisualizer, vid_HintNorbert, vid_HintPlanet;
    [SerializeField] private List<GameObject> allVideos;

    [Header("Clock")]
    [SerializeField] private List<GameObject> allClocks;
    [SerializeField] private Button nextButton, previousButton;
    [SerializeField] private GameObject errorPopUp;

    //Monitoring Software.
    private VideoPlayer _currentVideoPlayer;
    private VideoPlayer _audioVisualizerPlayer;

    //Video Manager.
    private bool _isHint = false;
    private bool _hasPlayedTechnicalArea = false;
    private bool _hasPlayedHousehold = false;
    private bool _isHintPlanet = false;
    private bool _isHintNorbertTools = false;

    //Clock.
    private int _currentClockIndex = 0;

    //Singleton.
    private static MonitoringController _instance;
    private SFXManager _sfxManager;

    #endregion

    #region Properties

    //Video Monitoring.
    public List<GameObject> AllVideos => allVideos;

    //Singleton.
    public static MonitoringController Instance => _instance;

    #endregion

    #region Built-In Methods

    private void Awake()
    {
        if (_instance) Destroy(this);
        _instance = this;
    }

    private void Start()
    {
        //Singleton.
        _sfxManager = SFXManager.Instance;

        //Initialize video.
        InitiliazedVideo();

        //Initialize the slider.
        if (slid_TimeProgress != null)
        {
            slid_TimeProgress.minValue = 0;
        }

        //Initialize clock.
        nextButton.onClick.AddListener(NextClock);
        previousButton.onClick.AddListener(PreviousClock);
        UpdateClockDisplay();
    }

    private void Update()
    {
        UpdateSlider();
        UpdateTimeTexts();
    }

    #endregion

    #region Software

    /**
     * <summary>
     * Button: open video monitoring software.
     * </summary>
     */
    public void OpenVideoMonitoring()
    {
        StartCoroutine(DelayOpenSoftware());
    }

    private IEnumerator DelayOpenSoftware()
    {
        _sfxManager.Sfx_NavigationStart.Play();
        btn_VideoMonitoring.interactable = false;
        yield return new WaitForSeconds(timeBeforeLoad);

        monitoringSoftware.SetActive(true);
        btn_VideoMonitoring.interactable = true;
    }

    /**
     * <summary>
     * Button: Change plan view.
     * </summary>
     */
    public void PlanDownstairs()
    {
        planDownstairs.SetActive(true);
        planUpstairs.SetActive(false);
    }

    public void PlanUpstairs()
    {
        planUpstairs.SetActive(true);
        planDownstairs.SetActive(false);
    }
    
    /**
     * <summary>
     * Display error pop up.
     * </summary>
     */
    public void ErrorPopUp()
    {
        _sfxManager.Sfx_ErrorPopUp.Play();
        errorPopUp.SetActive(true);
        _currentVideoPlayer.Pause();
        _audioVisualizerPlayer.Pause();
    }

    /**
     * <summary>
     * Unable error pop up.
     * </summary>
     */
    public void ExitPopUp()
    {
        errorPopUp.SetActive(false);
        _currentVideoPlayer.Play();
    }

    #endregion

    #region Video Monitoring

    /**
     * <summary>
     * Activates the specified video and deactivates all others.
     * </summary>
     */
    private void PlayVideo(GameObject videoToPlay)
    {
        foreach (GameObject video in allVideos)
        {
            bool isActive = video == videoToPlay;
            video.SetActive(isActive);

            if (isActive)
            {
                _currentVideoPlayer = video.GetComponent<VideoPlayer>();
                _currentVideoPlayer.Play();

                //Time slider.
                if (slid_TimeProgress != null)
                {
                    slid_TimeProgress.maxValue = (float)_currentVideoPlayer.length;
                }
            }
        }
    }

    /**
     * <summary>
     * Update slider based on time progression of videos.
     * </summary>
     */
    private void UpdateSlider()
    {
        if (_currentVideoPlayer != null && _currentVideoPlayer.isPlaying && slid_TimeProgress != null)
        {
            slid_TimeProgress.value = (float)_currentVideoPlayer.time;
        }
    }

    /**
     * <summary>
     * Update time video.
     * </summary>
     */
    private void UpdateTimeTexts()
    {
        if (_currentVideoPlayer != null && slid_TimeProgress != null)
        {
            double elapsedTime = _currentVideoPlayer.time;
            double remainingTime = _currentVideoPlayer.length - elapsedTime;

            if (txt_ElapsedTime != null)
            {
                txt_ElapsedTime.text = FormatTime(elapsedTime);
            }

            if (txt_RemainingTime != null)
            {
                txt_RemainingTime.text = FormatTime(remainingTime);
            }
        }
    }

    /**
     * <summary>
     * Timer format.
     * </summary>
     */
    private string FormatTime(double time)
    {
        int minutes = Mathf.FloorToInt((float)time / 60F);
        int seconds = Mathf.FloorToInt((float)time - minutes * 60);
        return string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    /**
     * <summary>
     * Make this functions accessible for buttons.
     * </summary>
     */
    public void PlayVidStorage()
    {
        _audioVisualizerPlayer.Play();
        PlayVideo(allVideos.Find(v => v.name == "VID_Storage"));
    }
    public void PlayVidHousehold()
    {
        _audioVisualizerPlayer.Play();
        _isHint = true;
        _hasPlayedHousehold = true;
        PlayVideo(allVideos.Find(v => v.name == "VID_Household"));
    }
    public void PlayVidLocker()
    {
        _audioVisualizerPlayer.Play();
        PlayVideo(allVideos.Find(v => v.name == "VID_Locker"));
    }
    public void PlayVidSupermarket()
    {
        _audioVisualizerPlayer.Play();
        PlayVideo(allVideos.Find(v => v.name == "VID_Supermarket"));
    }
    public void PlayVidBar()
    {
        _audioVisualizerPlayer.Play();
        PlayVideo(allVideos.Find(v => v.name == "VID_Bar"));
    }
    public void PlayVidStock()
    {
        _audioVisualizerPlayer.Play();
        PlayVideo(allVideos.Find(v => v.name == "VID_Stock"));
    }
    public void PlayVidDesk()
    {
        _audioVisualizerPlayer.Play();
        PlayVideo(allVideos.Find(v => v.name == "VID_Desk"));
    }
    public void PlayVidFridge()
    {
        _audioVisualizerPlayer.Play();
        PlayVideo(allVideos.Find(v => v.name == "VID_Fridge"));
    }
    public void PlayVidTechnicalArea()
    {
        _audioVisualizerPlayer.Play();
        _isHint = true;
        _hasPlayedTechnicalArea = true;
        PlayVideo(allVideos.Find(v => v.name == "VID_TechnicalArea"));
    }
    public void PlayVidHint()
    {
        //Activate error pop up.
        if (_isHint)
        {
            _audioVisualizerPlayer.Play();
            _isHint = false;

            //Play hint video only if the correct place has been chosen.
            if (_hasPlayedTechnicalArea)
            {
                if (_isHintPlanet)
                {
                    //Play video of planets.
                    PlayVideo(allVideos.Find(v => v.name == "VID_HintPlanet"));
                    _isHintPlanet = false;
                }
                else
                {
                    //Play video of Norbert.
                    PlayVideo(allVideos.Find(v => v.name == "VID_HintNorbert"));
                    _hasPlayedTechnicalArea = false;
                }
            }
        
            if (_hasPlayedHousehold || _isHintNorbertTools)
            {
                //Play video of Norbert with tools if player is efficient.
                PlayVideo(allVideos.Find(v => v.name == "VID_HintNorbertTools"));
                vid_HintNorbert.SetActive(false);
                vid_HintPlanet.SetActive(false);
                _hasPlayedHousehold = false;
                _isHintNorbertTools = false;
            }
        }
        else
        {
            ErrorPopUp();
        }
    }

    /**
     * <summary>
     * Initialize video: audio visualizer.
     * </summary>
     */
    private void InitiliazedVideo()
    {
        if (vid_AudioVisualizer != null)
        {
            _audioVisualizerPlayer = vid_AudioVisualizer.GetComponent<VideoPlayer>();
            _audioVisualizerPlayer.Play();
        }
    }

    /**
     * <summary>
     * Controls the video player based on the specified action.
     * </summary>
     */
    private void ControlVideo(VideoPlayer videoPlayer, string action)
    {
        if (videoPlayer != null)
        {
            switch (action)
            {
                case "Pause":
                    if (videoPlayer.isPlaying)
                        videoPlayer.Pause();
                    break;
                case "Resume":
                    if (!videoPlayer.isPlaying)
                        videoPlayer.Play();
                    break;
                case "Restart":
                    videoPlayer.Stop();
                    videoPlayer.Play();
                    break;
            }
        }
    }

    /**
     * <summary>
     * Make this functions accessible for buttons.
     * </summary>
     */
    public void PauseCurrentVideo() => ControlVideo(_currentVideoPlayer, "Pause");
    public void ResumeCurrentVideo() => ControlVideo(_currentVideoPlayer, "Resume");
    public void RestartCurrentVideo() => ControlVideo(_currentVideoPlayer, "Restart");

    public void PauseAudioVisualizer() => ControlVideo(_audioVisualizerPlayer, "Pause");
    public void ResumeAudioVisualizer() => ControlVideo(_audioVisualizerPlayer, "Resume");
    public void RestartAudioVisualizer() => ControlVideo(_audioVisualizerPlayer, "Restart");

    #endregion

    #region Clock

    /**
     * <summary>
     * Button: Display next clock.
     * </summary>
     */
    public void NextClock()
    {
        _currentClockIndex = (_currentClockIndex + 1) % allClocks.Count;
        UpdateClockDisplay();
    }

    /**
     * <summary>
     * Button: Display previous clock.
     * </summary>
     */
    public void PreviousClock()
    {
        _currentClockIndex = (_currentClockIndex - 1 + allClocks.Count) % allClocks.Count;
        UpdateClockDisplay();
    }

    /**
     * <summary>
     * Initialize clock method.
     * </summary>
     */
    private void UpdateClockDisplay()
    {
        // Disable all clocks.
        foreach (GameObject clock in allClocks)
        {
            clock.SetActive(false);
        }

        // Display actual clock.
        if (allClocks.Count > 0)
        {
            allClocks[_currentClockIndex].SetActive(true);
        }
    }

    #endregion

    #region Queen Board Command

    /**
     * <summary>
     * This function will be called by Queen board if player is efficient.
     * It will play a specific hint video rather than the actual hint video.
     * </summary>
     */
    public void PlayPlanetVideo()
    {
        _isHintPlanet = true;
    }

    /**
     * <summary>
     * This function will be called by Queen board if player is efficient.
     * It will play an additional hint video.
     * </summary>
     */
    public void PlayNorbertToolsVideo()
    {
        _isHintNorbertTools = true;
    }

    #endregion
}