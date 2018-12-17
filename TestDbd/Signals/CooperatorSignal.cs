using ServerDb.ServerData;
using System;
using System.Text;

namespace ServerDb.Signals
{
    public class CooperatorSignal: ISignal
    {
        public const string ActionTypeGet = "cooperator_get";
        public const string ActionTypeCreate = "cooperator_create";
        public const string ActionTypeUpdate = "cooperator_update";
        public const string ActionTypeDelete = "cooperator_delete";


        public bool HandleCreate(string Payload, StateObject state)
        {
            throw new System.NotImplementedException();
        }

        public bool HandleDelete(string Payload, StateObject state)
        {
            throw new NotImplementedException();
        }

        public bool HandleGet(string Payload, StateObject state)
        {
            try
            {
                var request_parse = Newtonsoft.Json.JsonConvert.DeserializeObject<RequestAll>(Payload);
                var payload_parse = Newtonsoft.Json.JsonConvert.DeserializeObject<SignalsData.Payload>(request_parse.RequestBody);

                if (payload_parse.Count <= 0)
                {
                    return ActionWorker.FinishingTaskSolve(ActionTypeGet, false, "amount must be more then 0", state);
                }
                else
                {
                    var reponseListWithData = DatabaseMiddlware.Workers.Cooperators.CooperatorHandler.GetCooperators(payload_parse.Offset, payload_parse.Count);
                    return ActionWorker.FinishingTaskSolve(ActionTypeGet, true, Newtonsoft.Json.JsonConvert.SerializeObject(reponseListWithData), state);
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
