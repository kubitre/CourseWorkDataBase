using System;
using System.Collections.Generic;
using System.Text;

namespace ServerDB.Signals
{
    class GetDishSignal : IPayload
    {
        public const string ActionType = "getdish";

        public void Setting(string payload)
        {
            throw new NotImplementedException();
        }
    }
}
