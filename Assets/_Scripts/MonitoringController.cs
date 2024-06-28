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
    [SerializeField] private GameObject planDownstairs, planUpstairs;

    [Header("Video Manager")]
    [SerializeField] private Slider slid_TimeProgress;
    [SerializeField] private TextMeshProUGUI txt_elapsedTime, txt_remainingTime;
    [SerializeField] private GameObject vid_AudioVisualizer;
    [SerializeField] private List<GameObject> allVideos;
    public Slider timeSlotSlider;
    private Dictionary<int, GameObject> timeSlotToVideoMap;
    public int correctTimeSlot;
    private bool iscorect = false;
    public GameObject correctVideo;

    //Monitoring Software.
    private VideoPlayer _currentVideoPlayer;
    private VideoPlayer _audioVisualizerPlayer;

    //Time Code.
    private string _input;

    //Singleton.
    private static MonitoringController _instance;

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
        InitiliazedVideo();
        InitializeTimeSlotToVideoMap();
        //Initialize the slider.
        if (slid_TimeProgress != null)
        {
            slid_TimeProgress.minValue = 0;
        }
        // Add listener to timeSlotSlider
        if (timeSlotSlider != null)
        {
            timeSlotSlider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });
        }
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
        yield return new WaitForSeconds(timeBeforeLoad);
        monitoringSoftware.SetActive(true);
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

    #endregion

    #region Video Monitoring
    private void InitializeTimeSlotToVideoMap()
    {
        timeSlotToVideoMap = new Dictionary<int, GameObject>
        {
            { 0, null }, // 00:00 - 02:00
            { 1, null }, // 02:00 - 04:00
            { 2, null }, // 04:00 - 06:00
            { 3, null }, // 06:00 - 08:00
            { 4, null }, // 08:00 - 10:00
            { 5, null }, // 10:00 - 12:00
            { 6, correctVideo }, // 12:00 - 14:00
            { 7, null }, // 14:00 - 16:00
            { 8, null }, // 16:00 - 18:00
            { 9, null }, // 18:00 - 20:00
            { 10, null }, // 20:00 - 22:00
            { 11, null }  // 22:00 - 00:00
        };

        // Set the correct video in the map based on the inspector setting
        if (timeSlotToVideoMap.ContainsKey(correctTimeSlot))
        {
            timeSlotToVideoMap[correctTimeSlot] = correctVideo;
        }
    }
    // Method to be called when the slider value changes
    public void OnSliderValueChanged()
    {
        int selectedTimeSlot = Mathf.FloorToInt(timeSlotSlider.value / 2);

        if (timeSlotToVideoMap.TryGetValue(selectedTimeSlot, out GameObject selectedVideo))
        {
            PlayVideo(selectedVideo);
        }
    }
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

            if (txt_elapsedTime != null)
            {
                txt_elapsedTime.text = FormatTime(elapsedTime);
            }

            if (txt_remainingTime != null)
            {
                txt_remainingTime.text = FormatTime(remainingTime);
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
    public void PlayVidTechnicalArea() => PlayVideo(allVideos.Find(v => v.name == "VID_TechnicalArea"));
    public void PlayVidStorage() => PlayVideo(allVideos.Find(v => v.name == "VID_Storage"));
    public void PlayVidHousehold() => PlayVideo(allVideos.Find(v => v.name == "VID_Household"));
    public void PlayVidLocker() => PlayVideo(allVideos.Find(v => v.name == "VID_Locker"));
    public void PlayVidSupermarket() => PlayVideo(allVideos.Find(v => v.name == "VID_Supermarket"));
    public void PlayVidBar() => PlayVideo(allVideos.Find(v => v.name == "VID_Bar"));
    public void PlayVidStock() => PlayVideo(allVideos.Find(v => v.name == "VID_Stock"));
    public void PlayVidDesk() => PlayVideo(allVideos.Find(v => v.name == "VID_Desk"));
    public void PlayVidFridge() => PlayVideo(allVideos.Find(v => v.name == "VID_Fridge"));

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

    #region Time Code

    /**
     * <summary>
     * Read any changes on input field.
     * </summary>
     */
    public void ReadStringInput(string inputEntry)
    {
        _input = inputEntry;
    }

    #endregion
}