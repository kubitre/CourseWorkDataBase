using System;
using System.Collections.Generic;
using System.Text;

namespace ServerDB.Signals
{
    interface IPayload
    {
        void Setting(string payload);
    }
}
