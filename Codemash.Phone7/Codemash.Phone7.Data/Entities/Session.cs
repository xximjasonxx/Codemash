using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Codemash.Phone7.Data.Entities
{
    public class Session : EntityBase
    {
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string Difficulty { get; set; }
        public string Technology { get; set; }
        public string EventType { get; set; }

        public string Room { get { return "Indigo"; } }
        public DateTime Starts { get { return DateTime.Now; } }

        public int SessionId { get; set; }
        public int SpeakerId { get; set; }
    }
}
