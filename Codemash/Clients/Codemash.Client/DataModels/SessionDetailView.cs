using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codemash.Client.Data.Entities;

namespace Codemash.Client.DataModels
{
    public class SessionDetailView : Session
    {
        public string SpeakerName { get; set; }
    }
}
