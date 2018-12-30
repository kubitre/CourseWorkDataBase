using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.NetworkMiddleware.NetworkSignal
{
    public class ChangePassword : ISignal
    {
        public bool Handle(StateObject state, params object[] param)
        {
            throw new NotImplementedException();
        }

        public bool HandleDelete(StateObject state, params object[] param)
        {
            throw new NotImplementedException();
        }

        public bool HandleSet(StateObject state, params object[] param)
        {
            try
            {
                return new Action_worker().HandleWorker(state, Newtonsoft.Json.JsonConvert.SerializeObject((NetworkData.PasswordChange)param[0]), NetworkResponseCodes.PasswordChangeCodes.ActionType);
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool HandleUpdate(StateObject state, params object[] param)
        {
            throw new NotImplementedException();
        }
    }
}
