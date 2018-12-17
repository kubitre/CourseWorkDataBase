using ServerDb.ServerData;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerDb.Signals
{
    public class StreetSignal : ISignal
    {
        public const string ActionTypeGet = "street_get";
        public const string ActionTypeCreate = "street_create";
        public const string ActionTypeUpdate = "street_update";
        public const string ActionTypeDelete = "street_delete";


        public bool HandleCreate(string Payload, StateObject state)
        {
            try
            {
                var payload = Newtonsoft.Json.JsonConvert.DeserializeObject<ServerDb.ServerData.RequestAll>(Payload);
                var street = payload.RequestBody;

                var requestStatus = DatabaseMiddlware.Workers.StreetHandler.AddStreet(street);

                return ActionWorker.FinishingTaskSolve(ActionTypeCreate, requestStatus, requestStatus.ToString(), state);
            }
            catch(Exception ex)
            {
                return ActionWorker.FinishingTaskSolve(ActionTypeCreate, false, ex.Message, state);
            }
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
                    return ActionWorker.FinishingTaskSolve(ActionTypeGet, false, "an request count must be more then 0!", state);
                }
                else
                {
                    var listWithStreets = DatabaseMiddlware.Workers.StreetHandler.GetStreets(action_payload.Offset, action_payload.Count);
                    return ActionWorker.FinishingTaskSolve(ActionTypeGet, true, Newtonsoft.Json.JsonConvert.SerializeObject(listWithStreets), state);
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
