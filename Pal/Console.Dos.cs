using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
    static class Console
    {
        static ushort s_consoleAttribute;
        static ushort s_cursorX, s_cursorY;

        public static unsafe string Title
        {
            set
            {
            }
        }

        public static unsafe bool CursorVisible
        {
            set
            {
            }
        }

        public static ConsoleColor ForegroundColor
        {
            set
            {
                s_consoleAttribute = (ushort)value;
            }
        }


        [DllImport("main", EntryPoint = "_keyavail")]
        private static extern byte keyavail();

        public static unsafe bool KeyAvailable => keyavail() != 0;

        [DllImport("main", EntryPoint = "_readkey")]
        private static extern byte readkey();

        public static unsafe ConsoleKeyInfo ReadKey(bool intercept)
        {
            char c = (char)(int)readkey();

            // Interpret WASD as arrow keys.
            ConsoleKey k = default;
            if (c == 'w')
                k = ConsoleKey.UpArrow;
            else if (c == 'd')
                k = ConsoleKey.RightArrow;
            else if (c == 's')
                k = ConsoleKey.DownArrow;
            else if (c == 'a')
                k = ConsoleKey.LeftArrow;
            else if (c == 'x')
                k = ConsoleKey.X;

            return new ConsoleKeyInfo(c, k, false, false, false);
        }

        public static unsafe void SetWindowSize(int x, int y)
        {
        }

        public static void SetBufferSize(int x, int y)
        {
        }

        public static void SetCursorPosition(int x, int y)
        {
            s_cursorX = (ushort)x;
            s_cursorY = (ushort)y;
        }

        public static unsafe void Write(char c)
        {
            byte* biosDataArea = (byte*)0x400;

            // Find the start of video RAM by reading the BIOS data area
            byte* vram = (byte*)0xB8000;
            if (*(biosDataArea+0x63) == 0xB4)
                vram = (byte*)0xB0000;

            // Get the offset of the active video page
            vram += *(ushort*)(biosDataArea + 0x4E);

            // Translate some unicode characters into the IBM hardware codepage
            byte b = c switch
            {
                '│' => (byte)0xB3,
                '┌' => (byte)0xDA,
                '┐' => (byte)0xBF,
                '─' => (byte)0xC4,
                '└' => (byte)0xC0,
                '┘' => (byte)0xD9,
                _ => (byte)c,
            };

            // TODO: read number of columns from the bios data area
            vram[(s_cursorY * 80 * 2) + (s_cursorX * 2)] = b;
            vram[(s_cursorY * 80 * 2) + (s_cursorX * 2) + 1] = (byte)s_consoleAttribute;

            // TODO: wrap/scroll?
            s_cursorX++;
        }
    }
}
