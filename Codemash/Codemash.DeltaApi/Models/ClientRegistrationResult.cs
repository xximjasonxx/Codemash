using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Codemash.DeltaApi.Models
{
    public class ClientRegistrationResult
    {
        public int ClientId { get; private set; }

        public ClientRegistrationResult(int id)
        {
            ClientId = id;
        }
    }
}