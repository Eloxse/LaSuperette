using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    #region Variables

    [Header("Video Monitoring")]
    [SerializeField] private VideoClip video;

    //Singleton.
    private static VideoManager _instance;

    #endregion

    #region Properties

    //Video Monitoring
    public VideoClip Video => video;

    //Singleton.
    public static VideoManager Instance => _instance;

    #endregion

    #region Built-In Method

    private void Awake()
    {
        //Singleton.
        if (_instance) Destroy(this);
        _instance = this;
    }

    #endregion
}