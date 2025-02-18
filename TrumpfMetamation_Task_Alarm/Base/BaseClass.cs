using FlaUI.UIA3;
using System.Diagnostics;

namespace AlarmAutomation.Base
{
    public class BaseClass
    {
        public FlaUI.Core.Application? App;
        public UIA3Automation? Automation;

        public void LaunchApplication()
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = "explorer.exe",
                Arguments = "shell:AppsFolder\\Microsoft.WindowsAlarms_8wekyb3d8bbwe!App",
                UseShellExecute = true
            };
            Process? process = Process.Start(processStartInfo);
            Thread.Sleep(5000);

            Console.WriteLine("Clock app opened.");
            Automation = new UIA3Automation();
            App = FlaUI.Core.Application.Attach("ApplicationFrameHost");
        }

        public void CloseApp()
        {
            App?.Close();
            Console.WriteLine("Application closed.");
        }
    }
}
