using System;
using System.Collections.Generic;
using System.Text;

namespace ServerDB.Signals
{
    class GetMenuSignal : IPayload
    {
        public const string ActionType = "getmenu";

        public void Setting(string payload)
        {
            throw new NotImplementedException();
        }
    }
}
