using AlarmAutomation.Base;
using AlarmAutomation.Config;
using AlarmAutomation.Pages;
using Newtonsoft.Json;

namespace AlarmAutomation.Tests
{
    public class TestAlarm
    {
        private BaseClass _baseClass;
        private AlarmPage _alarmPage;

        public TestAlarm()
        {
            _baseClass = new BaseClass();
        }

        public void RunTest()
        {
            _baseClass.LaunchApplication();

            string configFilePath = "config/config.json";
            var configData = JsonConvert.DeserializeObject<ConfigData>(File.ReadAllText(configFilePath));

            foreach (var alarmData in configData.Alarms)
            {
                // Create the alarm
                _alarmPage = new AlarmPage(_baseClass.App, _baseClass.Automation);
                _alarmPage.CreateAlarm(
                    alarmData.Hour,
                    alarmData.Minute,
                    alarmData.Meridian,
                    alarmData.Repeat,
                    alarmData.Message,
                    alarmData.RepeatDays,
                    alarmData.Music,
                    alarmData.AlarmSound
                );

                // Validate the alarm
                if (_alarmPage.ValidateAlarm(alarmData.Message))
                {
                    Console.WriteLine($"Alarm '{alarmData.Message}' created and validated successfully.");
                }
                else
                {
                    Console.WriteLine($"Failed to validate alarm '{alarmData.Message}'.");
                }

                // Delete the alarm
                _alarmPage.DeleteAlarm(alarmData.Message);

                // Validate the alarm has been deleted
                if (_alarmPage.IsAlarmDeleted(alarmData.Message))
                {
                    Console.WriteLine($"Alarm '{alarmData.Message}' deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"Failed to delete alarm '{alarmData.Message}'.");
                }
            }

            _baseClass.CloseApp();
        }
    }
}
