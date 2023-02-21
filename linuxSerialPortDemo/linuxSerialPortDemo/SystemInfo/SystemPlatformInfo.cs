using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace linuxSerialPortDemo.SystemInfo
{
    internal class SystemPlatformInfo
    {
        public static void showWelcome()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            var Version = version.ToString();
            var buildDateTime = System.IO.File.GetLastWriteTime(new Program().GetType().Assembly.Location).ToString();

            Console.WriteLine($"hello {Environment.OSVersion.Platform}");
            Console.WriteLine($"This is .net application.Verson:{Version}");
            Console.WriteLine($"System info:{Environment.OSVersion}");
            Console.WriteLine($"Environment.Version{Environment.Version}");
            Console.WriteLine($"Environment Directory{Environment.CurrentDirectory}");
            Console.WriteLine($"Appliction Directory{AppDomain.CurrentDomain.BaseDirectory}");
            Console.WriteLine();
            Console.WriteLine("Now time:{0:yyyy/MM/dd HH:mm:ss.fff}\r\n",DateTime.Now);

        }
    }
}
