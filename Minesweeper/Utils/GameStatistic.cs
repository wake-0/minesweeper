using System;
using System.Xml.Serialization;

namespace Minesweeper.Utils
{
    [Serializable]
    public class GameStatistic
    {
        public int Mines { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        [XmlIgnore]
        public DateTime Date { get; private set; }

        [XmlElement("Date")]
        public string XmlDate
        {
            get { return Date.ToString("yyyy-MM-dd HH:mm:ss"); }
            set { Date = DateTime.Parse(value); }
        }

        [XmlIgnore]
        public TimeSpan Time { get; set; }
        
        [XmlElement("Time")]
        public string XmlTime
        {
            get { return Time.ToString(@"dd\.hh\:mm\:ss"); }
            set { Time = TimeSpan.Parse(value); }
        }

        public bool HasWon { get; set; }

        public GameStatistic()
        {
            Date = DateTime.Now;
        }
    }
}
