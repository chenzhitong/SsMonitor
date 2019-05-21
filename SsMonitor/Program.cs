using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace SsMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<Process>();
            while (true)
            {
                list.Clear();
                foreach (Process p in Process.GetProcesses(System.Environment.MachineName))
                {
                    if (p.MainWindowHandle != IntPtr.Zero)
                        list.Add(p);
                }
                if (list.Count(p => p.ProcessName == "cmd") >= 3)
                {
                    Console.Clear();
                    Console.WriteLine("Shadowsocks 运行正常");
                    Thread.Sleep(10000);
                    continue;
                }
                list.ForEach(p =>
                {
                    if (p.ProcessName == "cmd")
                    {
                        Console.WriteLine("cmd 进程已关闭");
                        p.CloseMainWindow();
                    }
                });
                Thread.Sleep(500);

                Process.Start(new ProcessStartInfo { FileName = "ssserver.cmd" });
                Thread.Sleep(500);
                Console.WriteLine("ssserver.cmd 已启动");

                Process.Start(new ProcessStartInfo { FileName = "ssmgr.cmd" });
                Thread.Sleep(500);
                Console.WriteLine("ssmgr.cmd 已启动");

                Process.Start(new ProcessStartInfo { FileName = "webgui.cmd" });
                Thread.Sleep(500);
                Console.WriteLine("webgui.cmd 已启动");
            }
        }
    }
}
