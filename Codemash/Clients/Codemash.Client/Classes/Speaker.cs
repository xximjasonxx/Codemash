using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codemash.Client.Classes
{
    public class Speaker
    {
        public string FirstName { get { return "Jason"; } }
        public string LastName { get { return "Farrell"; } }
        public string PictureUrl { get { return "http://a0.twimg.com/profile_images/2389827999/ysucpo92mvusycslomz4.jpeg"; } }
        public string Twitter { get { return "jfarrell"; } }
        public string EmailAddress { get { return "jason.farrell@centare.com"; } }
        public string BlogUrl { get { return "http://www.jfarrell.net"; } }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }
}
