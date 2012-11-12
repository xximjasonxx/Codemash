using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Codemash.Api.Data.Entities;

namespace Codemash.DeltaApi.Models
{
    public class SpeakerViewModel
    {
        public string Biography { get; set; }

        public string BlogUrl { get; set; }

        public string EmailAddress { get; set; }

        public string Name { get; set; }

        public int SpeakerId { get; set; }

        public string Twitter { get; set; }
    }
}