using AdminPanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.NetworkMiddleware.NetworkSignal
{
    public class Menu_action : ISignal
    {
        public const string ActionTypeGet = "menu_get";
        public const string ActionTypeCreate = "menu_create";
        public const string ActionTypeUpdate = "menu_update";
        public const string ActionTypeDelete = "menu_delete";



        public bool Handle(StateObject state, params object[] param)
        {
            try
            {
                Payload payload;

                if (param.Length == 1)
                    payload = new Payload() { Count = Int32.Parse(param[0].ToString()), Offset = 0 };
                else if (param.Length == 2)
                    payload = new Payload() { Count = Int32.Parse(param[0].ToString()), Offset = Int32.Parse(param[1].ToString()) };
                else
                    payload = new Payload() { Count = -1, Offset = 0 };


                var payload_json = Newtonsoft.Json.JsonConvert.SerializeObject(payload);

                return new Action_worker().HandleWorker(state, payload_json, NetworkResponseCodes.MenuCodes.MENU_GET_CODE);
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

                return new Action_worker().HandleWorker(state, id.ToString(), NetworkResponseCodes.MenuCodes.MENU_DELETE_CODE);
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
                var payload = (NetworkData.Menu)param[0];
                var bytes_for_send = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
                
                return new Action_worker().HandleWorker(state, bytes_for_send, NetworkResponseCodes.MenuCodes.MENU_CREATE_CODE);
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
                var objectMe = new NetworkData.Menu
                {
                    Name = (string)param[0],
                    Coocker = (string)param[1],
                    Date = (DateTime)param[2],
                    Dishes = (List<string>)param[3]
                };
                var messageForSend = Newtonsoft.Json.JsonConvert.SerializeObject(objectMe);

                return new Action_worker().HandleWorker(state, messageForSend, NetworkResponseCodes.MenuCodes.MENU_UPDATE_CODE);
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
