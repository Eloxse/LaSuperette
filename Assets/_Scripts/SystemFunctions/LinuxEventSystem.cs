using UnityEngine;

#if UNITY_STANDALONE_LINUX || UNITY_EDITOR

public class LinuxEventSystem : MonoBehaviour
{
    /**
     * <summary>
     * This code intercept Alt+F4 command and do nothing on Linux OS.
     * </summary>
     */
    #region Built-In Methods

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.F4))
        {
            Debug.Log("Alt+F4 intercepted and ignored.");
            // Do nothing, effectively ignoring Alt+F4.
        }
    }

    #endregion
}

#endif