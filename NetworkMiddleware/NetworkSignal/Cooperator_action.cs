using AdminPanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.NetworkMiddleware.NetworkSignal
{
    public class Cooperator_action : ISignal
    {
        public const string ActionTypeGet       = "cooperator_get";
        public const string ActionTypeCreate    = "cooperator_create";
        public const string ActionTypeUpdate    = "cooperator_update";
        public const string ActionTypeDelete    = "cooperator_delete";


        public bool Handle(StateObject state, params object[] param)
        {
            try
            {
                Payload payload;
                if (param.Length != 1)
                    throw new ArgumentException("Must be 1 parameter: count cooperators need get!");

                payload = new Payload { Count = (int)param[0], Offset = 0 };

                var messageForSend = Newtonsoft.Json.JsonConvert.SerializeObject(payload);

                return new Action_worker().HandleWorker(state, messageForSend, NetworkResponseCodes.CooperatorCodes.COOPERATOR_GET_CODE);
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool HandleDelete(StateObject state, params object[] param)
        {
            try
            {
                if (param.Length != 1)
                    throw new ArgumentException("error lenght!");

                var id = (Guid)param[0];

                return new Action_worker().HandleWorker(state, id.ToString(), NetworkResponseCodes.CooperatorCodes.COOPERATOR_DELETE_CODE);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool HandleSet(StateObject state, params object[] param)
        {
            throw new NotImplementedException();
        }

        public bool HandleUpdate(StateObject state, params object[] param)
        {
            throw new NotImplementedException();
        }
    }
}
