using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class MonitoringController : MonoBehaviour
{
    #region Variables

    [Header("Scenes Manager")]
    [SerializeField] private float timeBeforeLoad = 0.5f;

    [Header("Monitoring Software")]
    [SerializeField] private GameObject monitoringSoftware;
    [SerializeField] private GameObject planDownstairs;
    [SerializeField] private GameObject planUpstairs;

    [Header("Video Monitoring")]
    [SerializeField] private Slider slid_TimeProgress;
    [SerializeField] private Text txt_elapsedTime;
    [SerializeField] private GameObject vid_AudioVisualizer;
    [SerializeField] private List<GameObject> allVideos;

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

        //Initialize the slider.
        if (slid_TimeProgress != null)
        {
            slid_TimeProgress.minValue = 0;
        }
    }

    private void Update()
    {
        UpdateSlider();
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

                //Slider.
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