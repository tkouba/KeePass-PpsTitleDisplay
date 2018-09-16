using System;
using System.Runtime.InteropServices;

public static class Taskbar
{
    public enum TaskbarStates
    {
        NoProgress = 0,
        Indeterminate = 0x1,
        Normal = 0x2,
        Error = 0x4,
        Paused = 0x8
    }

    [ComImport()]
    [Guid("56fdf344-fd6d-11d0-958a-006097c9a090")]
    [ClassInterface(ClassInterfaceType.None)]
    private class TaskbarInstance
    {
    }

    [ComImport()]
    [Guid("c43dc798-95d1-4bea-9030-bb99e2983a1a")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface ITaskbarList4
    {
        // ITaskbarList
        [PreserveSig]
        void HrInit();
        [PreserveSig]
        void AddTab(IntPtr hwnd);
        [PreserveSig]
        void DeleteTab(IntPtr hwnd);
        [PreserveSig]
        void ActivateTab(IntPtr hwnd);
        [PreserveSig]
        void SetActiveAlt(IntPtr hwnd);

        // ITaskbarList2
        [PreserveSig]
        void MarkFullscreenWindow(
            IntPtr hwnd,
            [MarshalAs(UnmanagedType.Bool)] bool fFullscreen);

        // ITaskbarList3
        [PreserveSig]
        void SetProgressValue(IntPtr hwnd, UInt64 ullCompleted, UInt64 ullTotal);
        [PreserveSig]
        void SetProgressState(IntPtr hwnd, TaskbarStates tbpFlags);
        [PreserveSig]
        void RegisterTab(IntPtr hwndTab, IntPtr hwndMDI);
        [PreserveSig]
        void UnregisterTab(IntPtr hwndTab);
        [PreserveSig]
        void SetTabOrder(IntPtr hwndTab, IntPtr hwndInsertBefore);
        [PreserveSig]
        void SetTabActive(IntPtr hwndTab, IntPtr hwndMDI, uint dwReserved);
        //[PreserveSig]
        //HRESULT ThumbBarAddButtons(
        //    IntPtr hwnd,
        //    uint cButtons,
        //    [MarshalAs(UnmanagedType.LPArray)] THUMBBUTTON[] pButtons);
        //[PreserveSig]
        //HRESULT ThumbBarUpdateButtons(
        //    IntPtr hwnd,
        //    uint cButtons,
        //    [MarshalAs(UnmanagedType.LPArray)] THUMBBUTTON[] pButtons);
        [PreserveSig]
        void ThumbBarSetImageList(IntPtr hwnd, IntPtr himl);
        [PreserveSig]
        void SetOverlayIcon(
          IntPtr hwnd,
          IntPtr hIcon,
          [MarshalAs(UnmanagedType.LPWStr)] string pszDescription);
        [PreserveSig]
        void SetThumbnailTooltip(
            IntPtr hwnd,
            [MarshalAs(UnmanagedType.LPWStr)] string pszTip);
        //[PreserveSig]
        //void SetThumbnailClip(
        //    IntPtr hwnd,
        //    ref Microsoft.WindowsAPICodePack.CoreNativeMethods.RECT prcClip);

        // ITaskbarList4
        //void SetTabProperties(IntPtr hwndTab, STPFLAG stpFlags);
    }

    private static ITaskbarList4 taskbarInstance = (ITaskbarList4)new TaskbarInstance();
    private static bool taskbarSupported = Environment.OSVersion.Version >= new Version(6, 1);

    static Taskbar()
    {
        taskbarInstance.HrInit();
    }

    public static void SetState(IntPtr windowHandle, TaskbarStates taskbarState)
    {
        if (taskbarSupported)
            taskbarInstance.SetProgressState(windowHandle, taskbarState);
    }

    public static void SetValue(IntPtr windowHandle, double progressValue, double progressMax)
    {
        if (taskbarSupported)
            taskbarInstance.SetProgressValue(windowHandle, (ulong)progressValue, (ulong)progressMax);
    }

    public static void SetOverlayIcon(IntPtr windowHandle, IntPtr iconHandle, string description)
    {
        if (taskbarSupported)
            taskbarInstance.SetOverlayIcon(windowHandle, iconHandle, description);
    }
}
