using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codemash.Phone.Shared.Services.Impl
{
    public class TestNotificationRegistrationService : INotificationRegistrationService
    {
        public void Register(string channelUri, string clientTypeName)
        {
            if (RegistrationCompleted != null)
                RegistrationCompleted(this, new EventArgs());
        }

        public event EventHandler RegistrationCompleted;
        public event EventHandler RegistrationFailed;
    }
}
