using System;
using System.Collections.Generic;
using System.Text;
using ServerDb.ServerData;

namespace ServerDb.Signals
{
    public class PositionSignal : ISignal
    {
        public const string ActionTypeGet = "position_get";
        public const string ActionTypeCreate = "position_create";
        public const string ActionTypeUpdate = "position_update";
        public const string ActionTypeDelete = "position_delete";


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
                var request = Newtonsoft.Json.JsonConvert.DeserializeObject<ServerData.RequestAll>(Payload);
                var action_request = Newtonsoft.Json.JsonConvert.DeserializeObject<SignalsData.Payload>(request.RequestBody);

                if (action_request.Count <= 0)
                {
                    return ActionWorker.FinishingTaskSolve(ActionTypeGet, false, "amount must be more then 0", state);
                }
                else
                {
                    var listWithResponse = DatabaseMiddlware.Workers.PositionHandler.GetPositions(action_request.Offset, action_request.Count);
                    return ActionWorker.FinishingTaskSolve(ActionTypeGet, true, Newtonsoft.Json.JsonConvert.SerializeObject(listWithResponse), state);
                }                
            }
            catch(Exception ex)
            {
                return ActionWorker.FinishingTaskSolve(ActionTypeGet, false, ex.Message, state);
            }
        }

        public bool HandleUpdate(string Payload, StateObject state)
        {
            throw new NotImplementedException();
        }
    }
}
