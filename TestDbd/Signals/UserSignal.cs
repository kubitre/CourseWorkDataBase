using ServerDb.ServerData;
using System;
using System.Text;

namespace ServerDb.Signals
{
    public class UserSignal : ISignal
    {
        public const string ActionTypeGet = "user_get";
        public const string ActionTypeCreate = "user_create";
        public const string ActionTypeUpdate = "user_update";
        public const string ActionTypeDelete = "user_delete";


        public bool HandleCreate(string Payload, StateObject state)
        {
            throw new NotImplementedException();
        }

        public bool HandleDelete(string Payload, StateObject state)
        {
            throw new NotImplementedException();
        }

        public bool HandleGet(string Payload, StateObject state)
        {
            try
            {
                var payload = Newtonsoft.Json.JsonConvert.DeserializeObject<RequestAll>(Payload);
                var action_payload = Newtonsoft.Json.JsonConvert.DeserializeObject<SignalsData.Payload>(payload.RequestBody);

                if(action_payload.Count <= 0)
                {
                    return ActionWorker.FinishingTaskSolve(ActionTypeGet, false, "an count for request must be more then 0", state);
                }
                else
                {
                    var listWithUsers = DatabaseMiddlware.Workers.Users.UserHandler.GetUsers(action_payload.Offset, action_payload.Count);
                    return ActionWorker.FinishingTaskSolve(ActionTypeGet, true, Newtonsoft.Json.JsonConvert.SerializeObject(listWithUsers), state);
                }
            }
            catch (Exception ex)
            {
                return ActionWorker.FinishingTaskSolve(ActionTypeGet, true, ex.Message, state);
            }
        }

        public bool HandleUpdate(string Payload, StateObject state)
        {
            throw new NotImplementedException();
        }
    }
}
