using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Codemash.DeltaApi.Models
{
    public class ClientUpdateModel
    {
        public string ChannelUri { get; set; }
        public int Changeset { get; set; }
    }
}