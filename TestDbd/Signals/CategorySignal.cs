using System;
using System.Collections.Generic;
using System.Text;
using ServerDb.ServerData;

namespace ServerDb.Signals
{
    public class CategorySignal : ISignal
    {
        public const string ActionTypeGet = "category_get";
        public const string ActionTypeCreate = "category_create";
        public const string ActionTypeUpdate = "category_update";
        public const string ActionTypeDelete = "category_delete";


        public bool HandleCreate(string Payload, StateObject state)
        {
            try
            {
                var payload = Newtonsoft.Json.JsonConvert.DeserializeObject<ServerData.RequestAll>(Payload);
                var category = Newtonsoft.Json.JsonConvert.DeserializeObject<SignalsData.CategoryData>(payload.RequestBody);

                NetworkData.ReponseAllRequests response;

                var requset = DatabaseMiddlware.Workers.CategoryHandler.CreateNewCategory(category.Name);

                return ActionWorker.FinishingTaskSolve(ActionTypeCreate, requset, requset.ToString(), state);
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
                var request = Newtonsoft.Json.JsonConvert.DeserializeObject<ServerData.RequestAll>(Payload);
                var action_request = Newtonsoft.Json.JsonConvert.DeserializeObject<SignalsData.Payload>(request.RequestBody);

                NetworkData.ReponseAllRequests response;

                if(action_request.Count <= 0)
                {
                    return ActionWorker.FinishingTaskSolve(ActionTypeGet, false, "amount must be more then 0", state);
                }
                else
                {
                    var listWithResponse = DatabaseMiddlware.Workers.CategoryHandler.GetCategories(action_request.Offset, action_request.Count);
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
