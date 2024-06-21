using UnityEngine;
using System;
using System.Runtime.InteropServices;
//using X11;

#if UNITY_STANDALONE_LINUX || UNITY_EDITOR

public class LinuxKeyInterceptor : MonoBehaviour
{
    /**
     * <summary>
     * This code intercept Windows key command and do nothing on Linux OS.
     * </summary>
     */
    [DllImport("libX11")]
    private static extern IntPtr XOpenDisplay(IntPtr display);

    [DllImport("libX11")]
    private static extern int XGrabKey(IntPtr display, int keycode, uint modifiers, IntPtr grab_window, int owner_events, int pointer_mode, int keyboard_mode);

    [DllImport("libX11")]
    private static extern int XUngrabKey(IntPtr display, int keycode, uint modifiers, IntPtr grab_window);

    [DllImport("libX11")]
    private static extern int XCloseDisplay(IntPtr display);

    private IntPtr display;
    private IntPtr rootWindow;

    void Start()
    {
        display = XOpenDisplay(IntPtr.Zero);
        if (display == IntPtr.Zero)
        {
            Debug.LogError("Unable to open X display");
            return;
        }

        rootWindow = XRootWindow(display, 0);

        // Grab left Windows key (Super_L).
        int keycodeSuperL = XKeysymToKeycode(display, 0xFFEB); // XK_Super_L.
        XGrabKey(display, keycodeSuperL, 0, rootWindow, 1, 1, 1);

        // Grab right Windows key (Super_R).
        int keycodeSuperR = XKeysymToKeycode(display, 0xFFEC); // XK_Super_R.
        XGrabKey(display, keycodeSuperR, 0, rootWindow, 1, 1, 1);
    }

    void OnDestroy()
    {
        if (display != IntPtr.Zero)
        {
            int keycodeSuperL = XKeysymToKeycode(display, 0xFFEB);
            XUngrabKey(display, keycodeSuperL, 0, rootWindow);

            int keycodeSuperR = XKeysymToKeycode(display, 0xFFEC);
            XUngrabKey(display, keycodeSuperR, 0, rootWindow);

            XCloseDisplay(display);
        }
    }

    private static int XKeysymToKeycode(IntPtr display, uint keysym)
    {
        // This function converts a keysym to a keycode using X11
        // You may need to implement this function based on X11 documentation or using X11.Net
        // For now, it's just a placeholder
        return (int)keysym;
    }

    private static IntPtr XRootWindow(IntPtr display, int screen_number)
    {
        // This function gets the root window for a given screen number
        // You may need to implement this function based on X11 documentation or using X11.Net
        // For now, it's just a placeholder
        return IntPtr.Zero;
    }
}
#endif