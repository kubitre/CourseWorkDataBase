using AdminPanel.NetworkMiddleware.NetworkData.Payloads;
using System;

namespace AdminPanel.NetworkMiddleware.NetworkSignal
{
    public class User_action : ISignal
    {
        public const string ActionTypeGet = "user_get";
        public const string ActionTypeCreate = "user_create";
        public const string ActionTypeUpdate = "user_update";
        public const string ActionTypeDelete = "user_delete";


        public bool Handle(StateObject state, params object[] param)
        {
            try
            {
                if (param.Length != 1)
                    throw new ArgumentException("Need 1 arguments for request!");

                var payload = new Models.Payload
                {
                    Count = (int)param[0],
                    Offset = 0
                };

                var messageForSend = Newtonsoft.Json.JsonConvert.SerializeObject(payload);

                return new Action_worker().HandleWorker(state, messageForSend, NetworkResponseCodes.UserCodes.USER_GET_CODE);
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

                return new Action_worker().HandleWorker(state, id.ToString(), NetworkResponseCodes.UserCodes.USER_DELETE_CODE);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool HandleSet(StateObject state, params object[] param)
        {
            try
            {
                var objectForSend = (NetworkData.User)param[0];

                var message = Newtonsoft.Json.JsonConvert.SerializeObject(objectForSend);

                return new Action_worker().HandleWorker(state, message, NetworkResponseCodes.UserCodes.USER_CREATE_CODE);
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
                var objectForSend = (NetworkData.User)param[0];

                var message = Newtonsoft.Json.JsonConvert.SerializeObject(objectForSend);

                return new Action_worker().HandleWorker(state, message, NetworkResponseCodes.UserCodes.USER_UPDATE_CODE);
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
