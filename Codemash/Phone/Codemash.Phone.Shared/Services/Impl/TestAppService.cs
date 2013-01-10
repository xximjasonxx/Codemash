using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codemash.Phone.Shared.Common;

namespace Codemash.Phone.Shared.Services.Impl
{
    public class TestAppService : IAppService
    {
        public void InitializePushChannel(PhoneClientType clientType)
        {
            if (PushChannelInitialized != null)
                PushChannelInitialized(this, new EventArgs());
        }

        public event EventHandler PushChannelInitialized;
        public event EventHandler PushChannelInitializationFailure;
        public void ShowProgressMessage(string message)
        {
            
        }

        public void HideProgressMessage()
        {
            
        }

        public void ShowToast(string title, string message, Action onTapHandler)
        {
            onTapHandler.Invoke();
        }
    }
}
