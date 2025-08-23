using System.Runtime.InteropServices;

namespace ResoConsole
{
    internal class WinAPI
    {
        [DllImport("kernel32.dll",
                EntryPoint = "AllocConsole",
                SetLastError = true,
                CharSet = CharSet.Auto,
                CallingConvention = CallingConvention.StdCall)]
        internal static extern bool AllocConsole();

        [DllImport("kernel32.dll",
            EntryPoint = "AttachConsole",
            SetLastError = true,
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern bool AttachConsole(UInt32 dwProcessId);

        [DllImport("kernel32.dll",
            EntryPoint = "FreeConsole",
            SetLastError = true,
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern bool FreeConsole();

        [DllImport("kernel32.dll",
            EntryPoint = "WriteConsole",
            SetLastError = true,
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern bool WriteConsole(IntPtr hConsoleOutput, string lpBuffer, uint nNumberOfCharsToWrite, out uint lpNumberOfCharsWritten, IntPtr lpReserved);

        [DllImport("kernel32.dll",
           EntryPoint = "GetConsoleWindow",
           SetLastError = true,
           CharSet = CharSet.Auto,
           CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll",
            EntryPoint = "SetWindowTextW",
            BestFitMapping = false,
            CharSet = CharSet.Unicode,
            ExactSpelling = true,
            PreserveSig = true,
            SetLastError = true)]
        public static extern bool SetWindowText(IntPtr hWnd, string pszText);

        //[DllImport("user32.dll",
        //    EntryPoint = "GetWindowTextW",
        //    BestFitMapping = false,
        //    CharSet = CharSet.Unicode,
        //    ExactSpelling = true,
        //    PreserveSig = true,
        //    SetLastError = true)]
        //public static extern bool GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        //[DllImport("user32.dll",
        //    EntryPoint = "GetWindowTextLengthW",
        //    SetLastError = true,
        //    ExactSpelling = true,
        //    PreserveSig = true,
        //    CallingConvention = CallingConvention.StdCall)]
        //public static extern int GetWindowTextLength(IntPtr hWin);

        [DllImport("kernel32.dll",
           EntryPoint = "GetStdHandle",
           SetLastError = true)]
        internal static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("user32.dll",
            EntryPoint = "ShowWindow",
            ExactSpelling = true)]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        //[DllImport("kernel32.dll",
        //    EntryPoint = "SetLastError",
        //    ExactSpelling = true)]
        //internal static extern void SetLastError(int dwErrCode);

        //[DllImport("kernel32.dll",
        //    EntryPoint = "GetLastError",
        //    ExactSpelling = true)]
        //internal static extern int GetLastError();

        [DllImport("user32.dll",
            EntryPoint = "GetSystemMenu",
            ExactSpelling = true,
            SetLastError = true)]
        internal static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll",
            EntryPoint = "DeleteMenu",
            ExactSpelling = true,
            SetLastError = true)]
        internal static extern bool DeleteMenu(IntPtr hMenu, uint uPosition, uint uFlags);

        //[DllImport("user32.dll",
        //    EntryPoint = "DrawMenuBar",
        //    ExactSpelling = true,
        //    SetLastError = true)]
        //internal static extern IntPtr DrawMenuBar(IntPtr hWnd);

        [DllImport("kernel32.dll", 
            EntryPoint = "SetConsoleCtrlHandler",
            ExactSpelling = true,
            SetLastError = true)]
        internal static extern bool SetConsoleCtrlHandler(ConsoleEvent callback = null, bool add = true);

        internal delegate bool ConsoleEvent(int eventType); // A relic for the future

        internal const UInt32 ERROR_ACCESS_DENIED = 0x50;
        internal const UInt32 ERROR_INVALID_PARAMETER = 0x57;
        internal const int STD_INPUT_HANDLE = -10;
        internal const int STD_OUTPUT_HANDLE = -11;
        internal const int STD_ERROR_HANDLE = -12;
        internal const int SW_HIDE = 0;
        internal const int SW_SHOW = 5;
        internal const int SC_CLOSE = 0xF060;
        internal const uint MF_BYCOMMAND = 0;
        internal const UInt32 ATTACH_PARRENT_PROCESS = 0xFFFFFFFF;
    }
}
