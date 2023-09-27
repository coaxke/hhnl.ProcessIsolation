using System;
using hhnl.ProcessIsolation;
using hhnl.ProcessIsolation.Windows;




namespace RunIsolatedProcess
{
    internal class Program
    {
        static void Main(string[] args)
        { 
            var desktopPath = Environment.ExpandEnvironmentVariables("D:\\Temp\\browser\\App\\Firefox64");
            var desktopFileAccess = new hhnl.ProcessIsolation.FileAccess(desktopPath, hhnl.ProcessIsolation.FileAccess.Right.Execute 
                | hhnl.ProcessIsolation.FileAccess.Right.Write
                | hhnl.ProcessIsolation.FileAccess.Right.Read
                | hhnl.ProcessIsolation.FileAccess.Right.All);
           
            IProcessIsolator isolator = new AppContainerIsolator();
            //isolator.StartIsolatedProcess("MyIsolatedProcess", "c:\\windows\\notepad.exe", makeApplicationDirectoryReadable: false);

            var processArgs = new[] { @"-browser"};
            Environment.SetEnvironmentVariable("MOZ_FORCE_DISABLE_E10S", "114.0.1");

            isolator.StartIsolatedProcess("AppContainer.Launcher", "D:\\Temp\\browser\\App\\Firefox64\\firefox.exe",
                commandLineArguments: processArgs, makeApplicationDirectoryReadable: true, lowPrivAppContainer: true, workingDirectory: desktopPath, fileAccess: new[] { desktopFileAccess });

            Console.ReadKey();
        }
    }
}