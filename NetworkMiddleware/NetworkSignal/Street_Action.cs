using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.NetworkMiddleware.NetworkSignal
{
    public class Street_Action : ISignal
    {
        public const string ActionTypeGet = "street_get";
        public const string ActionTypeCreate = "street_create";
        public const string ActionTypeUpdate = "street_update";
        public const string ActionTypeDelete = "street_delete";

        public bool Handle(StateObject state, params object[] param)
        {
            try
            {
                Models.Payload payload;
                if (param.Length == 1)
                    payload = new Models.Payload { Count = (int)param[0], Offset = 0 };
                else if (param.Length == 2)
                    payload = new Models.Payload { Count = (int)param[0], Offset = (int)param[1] };
                else 
                    payload = new Models.Payload { Count = 0, Offset = 0 };

                var messageForSend = Newtonsoft.Json.JsonConvert.SerializeObject(payload);

                return new Action_worker().HandleWorker(state, messageForSend, NetworkResponseCodes.StreetCodes.STREET_GET_CODE);
            }
            catch (Exception ex)
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

                return new Action_worker().HandleWorker(state, id.ToString(), NetworkResponseCodes.StreetCodes.STREET_DELETE_CODE);
            }
            catch (Exception ex)
            {
                return new Action_worker().HandleWorker(state, "error handled!", NetworkResponseCodes.StreetCodes.STREET_DELETE_CODE);
            }
        }

        public bool HandleSet(StateObject state, params object[] param)
        {
            try
            {
                var payload = param[0];
                var messageForSend = Newtonsoft.Json.JsonConvert.SerializeObject(payload);

                return new Action_worker().HandleWorker(state, messageForSend, NetworkResponseCodes.StreetCodes.STREET_CREATE_CODE);
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool HandleUpdate(StateObject state, params object[] param)
        {
            try
            {
                var payload = (string)param[0];

                return new Action_worker().HandleWorker(state, payload, NetworkResponseCodes.StreetCodes.STREET_UPDATE_CODE);
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
