using System;
using System.Collections.Generic;
using System.Text;

namespace ServerDB.Signals
{
    class TestConnectionSignal : IPayload
    {
        public const string ActionType = "testconnection";

        public void Setting(string payload)
        {
            throw new NotImplementedException();
        }
    }
}
