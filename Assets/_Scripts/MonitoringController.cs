using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitoringController : MonoBehaviour
{
    #region Variables

    [Header("Scenes Manager")]
    [SerializeField] private float timeBeforeLoad = 0.5f;

    [Header("Monitoring Software")]
    [SerializeField] private GameObject monitoringSoftware;

    [Header("Video Monitoring")]
    [SerializeField] private List<GameObject> allVideos;

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
            video.SetActive(video == videoToPlay);
        }
    }

    public void PlayVidTechnicalArea() => PlayVideo(allVideos.Find(v => v.name == "VID_TechnicalArea"));
    public void PlayVidStorage() => PlayVideo(allVideos.Find(v => v.name == "VID_Storage"));
    public void PlayVidHousehold() => PlayVideo(allVideos.Find(v => v.name == "VID_Household"));
    public void PlayVidLocker() => PlayVideo(allVideos.Find(v => v.name == "VID_Locker"));
    public void PlayVidSupermarket() => PlayVideo(allVideos.Find(v => v.name == "VID_Supermarket"));
    public void PlayVidBar() => PlayVideo(allVideos.Find(v => v.name == "VID_Bar"));
    public void PlayVidStock() => PlayVideo(allVideos.Find(v => v.name == "VID_Stock"));
    public void PlayVidDesk() => PlayVideo(allVideos.Find(v => v.name == "VID_Desk"));
    public void PlayVidFridge() => PlayVideo(allVideos.Find(v => v.name == "VID_Fridge"));

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