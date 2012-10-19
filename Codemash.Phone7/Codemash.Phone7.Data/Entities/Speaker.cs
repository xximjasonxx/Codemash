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
    public class Speaker : EntityBase
    {
        public int SpeakerId { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public string Twitter { get; set; }
        public string BlogUrl { get; set; }
    }
}
