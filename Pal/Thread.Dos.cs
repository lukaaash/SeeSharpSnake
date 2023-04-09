using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Threading
{
    static class Thread
    {
        [DllImport("main", EntryPoint = "_halt")]
        private static extern void halt();

        public static unsafe void Sleep(int delayMs)
        {
            long expected = Environment.TickCount64 + delayMs;
            while (Environment.TickCount64 < expected)
            {
                halt();
            }
        }
    }
}
