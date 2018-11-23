using System;
using System.Collections.Generic;
using System.Text;

namespace ServerDB.HandlersForRequest
{
    class NullHandler : IHandler
    {
        public void hand()
        {
            throw new NotImplementedException();
        }
    }
}
