using ServerDb.ServerData;
using System;
using System.Text;

namespace ServerDb.Signals
{
    public class DishSiganl : ISignal
    {
        public const string ActionTypeGet = "dish_get";
        public const string ActionTypeCreate = "dish_create";
        public const string ActionTypeUpdate = "dish_update";
        public const string ActionTypeDelete = "dish_delete";


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

                if (action_payload.Count <= 0)
                {
                    return ActionWorker.FinishingTaskSolve(ActionTypeGet, false, "amount must be more then 0", state);
                }
                else
                {
                    var reponseListWithData = DatabaseMiddlware.Workers.Dishes.DishHandler.GetDishes(action_payload.Offset, action_payload.Count);
                    return ActionWorker.FinishingTaskSolve(ActionTypeGet, true, Newtonsoft.Json.JsonConvert.SerializeObject(reponseListWithData), state);
                }
            }
            catch (Exception ex)
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
