using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.NetworkMiddleware.CheckingInformation
{
    public class AuthentificationCheck
    {
        public delegate void NewAuthentificationRequest(string message);
        public static event NewAuthentificationRequest AuthPass;

        public AuthentificationCheck()
        {
            AuthPass += AuthTest;
        }

        public void AuthTest(string message)
        {

        }
    }
}
