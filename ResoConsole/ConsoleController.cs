using Elements.Core;

namespace ResoConsole
{
    internal class ConsoleController : IDisposable
    {
        private static IntPtr hConsoleWindow;

        public ConsoleController(bool AttachUniLog)
        {
            if (hConsoleWindow != IntPtr.Zero) {
                return;
            }

            UniLog.Log("Initializing ResoConsole Window");

            bool Attached = WinAPI.AttachConsole(WinAPI.ATTACH_PARRENT_PROCESS); // Tries to attach an existing console
                                                                                 //int LastError = Marshal.GetLastWin32Error(); // if the last error is ACCESS_DENIED then the process has a console
            hConsoleWindow = WinAPI.GetConsoleWindow();

            if (hConsoleWindow == IntPtr.Zero && !WinAPI.AllocConsole()) // Check first for a console
            {
                UniLog.Log("Unable to Allocate Console. Abort Initilization");
                return; // Well I don't know what to tell ya holmes
            }

            hConsoleWindow = WinAPI.GetConsoleWindow();
            WinAPI.DeleteMenu(WinAPI.GetSystemMenu(hConsoleWindow, false), WinAPI.SC_CLOSE, WinAPI.MF_BYCOMMAND); // Exit button privileges have been revoked
            WinAPI.SetConsoleCtrlHandler(); // no no no. no more console controls for you.
            Console.Title = "ResoConsole";
            UniLog.Log("Console Attached! [" + hConsoleWindow + " -> " + Console.Title + "]");

            InitializeOutStream();
            InitializeInStream();

            if (AttachUniLog)
            {
                // Register to also output to console
                UniLog.OnLog += WriteLineL; // lel
                UniLog.OnWarning += WriteLineW;
                UniLog.OnError += WriteLineE;
            }
        }

        public IntPtr ConsoleHandle { get { return hConsoleWindow; } }

        private static void InitializeOutStream()
        {
            StreamWriter e = new StreamWriter(Console.OpenStandardOutput());
            e.AutoFlush = true;
            Console.SetOut(e);
        }

        private static void InitializeInStream()
        {
            StreamReader f = new StreamReader(Console.OpenStandardInput());
            Console.SetIn(f);
        }

        public static void Clear()
        {
            uint Length = 0;
            string Cls = "\x1b[J";
            IntPtr StdHandle = WinAPI.GetStdHandle(WinAPI.STD_OUTPUT_HANDLE);
            WinAPI.WriteConsole(StdHandle, Cls, (uint)Cls.Length, out Length, (IntPtr)null);
        }

        public static void WriteLine(object obj)
        {
            if (hConsoleWindow == IntPtr.Zero) {
                return;
            }
            Console.WriteLine(obj);
        }

        public static void WriteLineL(string obj)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(obj);
        }

        public static void WriteLineW(string obj)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(obj);
        }

        public static void WriteLineE(string obj)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(obj);
        }

        public override string ToString()
        {
            return "Console Handle: " + hConsoleWindow;
        }

        public void Dispose()
        {
            UniLog.OnWarning -= WriteLineL;
            UniLog.OnError -= WriteLineW;
            UniLog.OnLog -= WriteLineL;

            Console.Out.Dispose();
            Console.In.Dispose();

            WinAPI.FreeConsole();

            hConsoleWindow = IntPtr.Zero;
        }
    }
}
