using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;

namespace AlarmAutomation.Pages
{
    public class AlarmPage
    {
        private readonly FlaUI.Core.Application? App;
        private readonly UIA3Automation? Automation;

        public AlarmPage(FlaUI.Core.Application? app, UIA3Automation? automation)
        {
            App = app;
            Automation = automation;
        }

        public void CreateAlarm(int hour, int minute, string meridian, bool repeat, string message, List<string> repeatDays, string music, string alarmSound)
        {
            // Click the "Add Alarm" button
            if (App != null && Automation != null)
            {
                var mainWindow = App.GetMainWindow(Automation);
                var addButton = mainWindow.FindFirstDescendant(cf => cf.ByName("Add an alarm"));
                addButton?.Click();

                // Set hour, minute, median
                Thread.Sleep(1000);

                var hourBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("HourPicker"))?.AsDateTimePicker();
                var minuteBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("MinutePicker"))?.AsDateTimePicker();
                var meridianBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TwelveHourPicker"))?.AsDateTimePicker();
                var repeatCheckBox = mainWindow.FindFirstDescendant(cf => cf.ByName("Repeat alarm"))?.AsCheckBox();
                var messageBox = mainWindow.FindFirstDescendant(cf => cf.ByName("Alarm name"))?.AsTextBox();
                var saveButton = mainWindow.FindFirstDescendant(cf => cf.ByName("Save"))?.AsButton();

                if (hourBox != null)
                {
                    var hourValuePattern = hourBox.Patterns.Value.Pattern;
                    hourValuePattern?.SetValue(hour.ToString());
                }

                if (minuteBox != null)
                {
                    var minuteValuePattern = minuteBox.Patterns.Value.Pattern;
                    minuteValuePattern?.SetValue(minute.ToString("D2"));
                }

                if (meridianBox != null)
                {
                    var meridianValuePattern = meridianBox.Patterns.Value.Pattern;
                    meridianValuePattern?.SetValue(meridian);
                }

                messageBox?.Enter(message);

                if (repeat)
                {
                    if (repeatCheckBox != null && repeatCheckBox.IsChecked != true)
                    {
                        repeatCheckBox.IsChecked = true;
                    }

                    foreach (var day in repeatDays)
                    {
                        var dayCheckBox = mainWindow.FindFirstDescendant(cf => cf.ByName(day))?.AsCheckBox();
                        if (dayCheckBox != null && dayCheckBox.IsChecked != true)
                        {
                            dayCheckBox.IsChecked = true;
                        }
                    }
                }
                Thread.Sleep(3000);
                // Set alarm sound, snooze time
                var soundComboBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("AlarmSoundsOptionText")).AsComboBox();
                var snoozeComboBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("SnoozeComboBox")).AsComboBox();

                soundComboBox?.Select(music);
                snoozeComboBox?.Select(alarmSound);
                Thread.Sleep(1000);

                // Save the alarm
                saveButton?.Click();
            }
            
        }
        public bool ValidateAlarm(string message)
        {
            // Ensure that the main window is properly retrieved
            var mainWindow = App?.GetMainWindow(Automation);
            if (mainWindow == null)
                return false;  // Return false if the main window is not found
            Thread.Sleep(3000);
            var alarmElements = mainWindow.FindAllDescendants(cf => cf.ByName(message));
            if (alarmElements == null || alarmElements.Length == 0)
            {
                return false;  // Return false if no alarms are found
            }

            // Iterate through found alarms
            foreach (var alarmElement in alarmElements)
            {
                // Safely check if the element is a TextBox and compare its text
                var textPattern = alarmElement?.Patterns.Text.Pattern;
                if (textPattern != null && textPattern.DocumentRange.GetText(-1) == message)
                {
                    return true; // Return true if a matching alarm is found
                }
            }

            // Return false if no matching alarm is found after checking all
            return false;
        }


        public void DeleteAlarm(string message)
        {
            var mainWindow = App?.GetMainWindow(Automation);
            Thread.Sleep(3000);
            var alarmElements = mainWindow.FindAllDescendants(cf => cf.ByName(message));
            if (alarmElements != null && alarmElements.Length > 0)
            {
                foreach (var alarmElement in alarmElements)
                {
                    alarmElement.RightClick();
                    Thread.Sleep(1000);
                    var deleteOption = mainWindow.FindFirstDescendant(cf => cf.ByName("Delete"))?.AsButton();
                    if (deleteOption != null)
                    {
                        deleteOption.DoubleClick();
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        Console.WriteLine("Delete button not found.");
                    }
                }

            }
            else
            {
                Console.WriteLine($"No alarms found with message '{message}'.");
            }
        }
        public bool IsAlarmDeleted(string message)
        {
            var mainWindow = App?.GetMainWindow(Automation);
            if (mainWindow == null)
                return false;
            var alarm = mainWindow.FindFirstDescendant(cf => cf.ByName(message));
            return alarm == null;
        }
    }
}
