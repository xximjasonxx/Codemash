using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codemash.Client.Data.Entities
{
    public class Speaker : EntityBase
    {
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public string PictureUrl { get; internal set; }
        public string Company { get; internal set; }
        public string Twitter { get; internal set; }
        public string Biography { get; internal set; }
        public string BlogUrl { get; internal set; }
        public int SpeakerId { get; internal set; }

        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }
    }
}
