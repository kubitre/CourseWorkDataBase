using ServerDb.ServerData;
using System;
using System.Text;

namespace ServerDb.Signals
{
    public class MenuSignal : ISignal
    {
        public const string ActionTypeGet = "menu_get";
        public const string ActionTypeCreate = "menu_create";
        public const string ActionTypeUpdate = "menu_update";
        public const string ActionTypeDelete = "menu_delete";


        public bool HandleCreate(string Payload, StateObject state)
        {
            try
            {
                var request_packet = Newtonsoft.Json.JsonConvert.DeserializeObject<ServerDb.ServerData.RequestAll>(Payload);
                var menu_from_frontend = Newtonsoft.Json.JsonConvert.DeserializeObject<ServerDb.SignalsData.MenuData>(request_packet.RequestBody);

                var requestStatus = DatabaseMiddlware.Workers.Menus.MenuHandler.AddMenu(new DatabaseMiddlware.ObjectsAfterDb.Menu
                {
                    Name = menu_from_frontend.Name,
                    Date = menu_from_frontend.Date,
                    Dishes = menu_from_frontend.Dishes,
                    Coocker = menu_from_frontend.Coocker
                });

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

                if (action_payload.Count <= 0)
                {
                    return ActionWorker.FinishingTaskSolve(ActionTypeGet, false, "amount must be more then 0", state);
                }
                else
                {
                    var reponseListWithData = DatabaseMiddlware.Workers.Menus.MenuHandler.GetMenus(action_payload.Offset, action_payload.Count);
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
