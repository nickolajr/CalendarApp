using System;

namespace CalendarApp.Classes
{
    public class workLog
    {
        public int id { get; set; }
        public int Accid { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Title { get; set; }
        public string Descs { get; set; }
        public DateTime Cday { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsOpen { get; set; }

        public workLog()
        {
            Cday = DateTime.Now.Date;
            IsOpen = false;
        }
    }
}
