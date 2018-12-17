using ServerDb.ServerData;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerDb.Signals
{
    public class ProductSignal : ISignal
    {
        public const string ActionTypeGet = "product_get";
        public const string ActionTypeCreate = "product_create";
        public const string ActionTypeUpdate = "product_update";
        public const string ActionTypeDelete = "product_delete";

        public bool HandleCreate(string Payload, StateObject state)
        {
            try
            {
                var request_payload = Newtonsoft.Json.JsonConvert.DeserializeObject<ServerData.RequestAll>(Payload);
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<SignalsData.ProductData>(request_payload.RequestBody);

                var requestStatus = DatabaseMiddlware.Workers.Products.ProductHandler.AddProduct(new DatabaseMiddlware.ObjectsAfterDb.Product
                {
                    Name = data.Name,
                    Price = data.Price
                });

                return ActionWorker.FinishingTaskSolve(ActionTypeCreate, requestStatus, requestStatus.ToString(), state);
            }
            catch (Exception ex)
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

                if (action_payload.Count <= 0)
                {
                    return ActionWorker.FinishingTaskSolve(ActionTypeGet, false, "amount must be more then 0", state);
                }
                else
                {
                    var reponseListWithData = DatabaseMiddlware.Workers.Products.ProductHandler.GetProducts(action_payload.Offset, action_payload.Count);
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
