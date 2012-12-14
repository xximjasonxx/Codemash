using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codemash.Client.Common.Result
{
    public class RegistrationResult
    {
        public bool IsSuccess { get; private set; }
        public int? ClientId { get; private set; }

        public RegistrationResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public RegistrationResult(bool isSuccess, int id)
        {
            IsSuccess = isSuccess;

            if (isSuccess)
                ClientId = id;
        }
    }
}
