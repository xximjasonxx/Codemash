using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Phone.Notification;

namespace Codemash.Phone.Shared.Services
{
    public interface IAppService
    {
        HttpNotificationChannel NotificationChannel { get; }
    }
}
