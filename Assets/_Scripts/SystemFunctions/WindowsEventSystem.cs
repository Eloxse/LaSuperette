using UnityEngine;

#if UNITY_STANDALONE_WIN
using System;
using System.Runtime.InteropServices;
#endif

public class WindowsEventInterceptor : MonoBehaviour
{

#if UNITY_STANDALONE_WIN
    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll", SetLastError = true)]
    private static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

    private const int WM_CLOSE = 0x0010;
    private const int WM_SYSCOMMAND = 0x0112;
    private const int SC_CLOSE = 0xF060;

    private delegate IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
    private static WndProc newWndProc = new WndProc(HookCallback);
    private static IntPtr oldWndProc = IntPtr.Zero;
    private static IntPtr hwnd = IntPtr.Zero;

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, WndProc dwNewLong);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

    private const int GWL_WNDPROC = -4;

    void Start()
    {
        hwnd = GetForegroundWindow();
        oldWndProc = SetWindowLongPtr(hwnd, GWL_WNDPROC, newWndProc);
    }

    private static IntPtr HookCallback(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
    {
        if (msg == WM_SYSCOMMAND && wParam.ToInt32() == SC_CLOSE)
        {
            // Intercept Alt+F4 and do nothing
            return IntPtr.Zero;
        }

        return CallWindowProc(oldWndProc, hWnd, msg, wParam, lParam);
    }

    void OnApplicationQuit()
    {
        // Restore the original window procedure when the application quits
        if (hwnd != IntPtr.Zero && oldWndProc != IntPtr.Zero)
        {
            SetWindowLongPtr(hwnd, GWL_WNDPROC, (WndProc)Marshal.GetDelegateForFunctionPointer(oldWndProc, typeof(WndProc)));
        }
    }
#endif

}

public class KeyEventInterceptor : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.F4))
        {
            Debug.Log("Alt+F4 intercepted and ignored.");
            // Do nothing, effectively ignoring Alt+F4
        }
    }
}