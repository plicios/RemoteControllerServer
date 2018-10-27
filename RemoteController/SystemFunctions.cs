using System;
using System.Runtime.InteropServices;
using System.Text;

namespace RemoteController
{
    public class SystemFunctions
    {
        const uint KeyDown = 0x0100;
        const uint KeyUp = 0x0101;
        public const int Space = 32; //0x20
        public const int UpArrow = 38;
        public const int DownArrow = 40;
        //public const int LeftArrow = 37;
        //public const int RightArrow = 39;

        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, uint msg, int wParam, int lParam);

        //[DllImport("user.32.dll")]
        //public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, IntPtr dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern int SetForegroundWindow(IntPtr hWnd);

        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        //[DllImport("user32.dll")]
        //public static extern IntPtr FindWindow(IntPtr zeroOnly, string lpWindowName);

        private delegate bool EnumWindowsProc(IntPtr hWnd, int lParam);

        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        public static void BringToForeground(IntPtr handle)
        {
            SetForegroundWindow(handle);
        }

        public static IntPtr GetWindowConstains(string textFragment)
        {
            IntPtr pointer = IntPtr.Zero;
            EnumWindows(delegate (IntPtr hWnd, int lParam)
            {
                int length = GetWindowTextLength(hWnd);
                if (length > 0)
                {
                    StringBuilder builder = new StringBuilder(length);
                    GetWindowText(hWnd, builder, length + 1);
                    string name = builder.ToString();
                    if (name.IndexOf(textFragment, StringComparison.InvariantCultureIgnoreCase) > -1)
                    {
                        pointer = hWnd;
                        return false;
                    }
                }

                return true;

            }, 0);

            return pointer;
        }

        public static void SendKey(IntPtr hWnd, int key, int howMany = 1)
        {
            for (int i = 0; i < howMany; i++)
            {
                PostMessage(hWnd, KeyDown, key, 0);
                PostMessage(hWnd, KeyUp, key, 0);
            }
        }
    }
}
