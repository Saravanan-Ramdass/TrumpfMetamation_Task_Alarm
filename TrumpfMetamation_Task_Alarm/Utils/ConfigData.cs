
namespace AlarmAutomation.Config
{
    public class ConfigData
    {
        public List<AlarmData> Alarms { get; set; }
    }

    public class AlarmData
    {
        public int Hour { get; set; }
        public int Minute { get; set; }
        public string Meridian { get; set; }
        public bool Repeat { get; set; }
        public string Message { get; set; }
        public List<string> RepeatDays { get; set; }
        public string Music { get; set; }
        public string AlarmSound { get; set; }
        public int SnoozeTime { get; set; }
    }
}
