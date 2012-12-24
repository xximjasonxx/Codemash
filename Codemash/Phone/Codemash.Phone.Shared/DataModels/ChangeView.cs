using System;
using Codemash.Phone.Data.Common;

namespace Codemash.Phone.Shared.DataModels
{
    public class ChangeView
    {
        public long EntityId { get; set; }
        public string EntityDisplay { get; set; }
        public ActionType EntityChangeAction { get; set; }

        public string ActionTypeDisplay
        {
            get
            {
                if (EntityChangeAction == ActionType.Modify)
                    return "Modified";

                if (EntityChangeAction == ActionType.Add)
                    return "Added";

                return "Removed";
            }
        }
    }
}
