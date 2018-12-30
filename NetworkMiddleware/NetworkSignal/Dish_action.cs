using AdminPanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace AdminPanel.NetworkMiddleware.NetworkSignal
{
    [Serializable]
    public class Dish_action : ISignal
    {
        public const string ActionTypeGet = "dish_get";
        public const string ActionTypeCreate = "dish_create";
        public const string ActionTypeUpdate = "dish_update";
        public const string ActionTypeDelete = "dish_delete";

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

                return new Action_worker().HandleWorker(state, Newtonsoft.Json.JsonConvert.SerializeObject(payload), NetworkResponseCodes.DishCodes.DISH_GET_CODE);
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

                return new Action_worker().HandleWorker(state, id.ToString(), NetworkResponseCodes.DishCodes.DISH_DELETE_CODE);
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
                var objectT = new NetworkData.Dish
                {
                    Name = (string)param[0]
                };

                return new Action_worker().HandleWorker(state, Newtonsoft.Json.JsonConvert.SerializeObject(objectT), NetworkResponseCodes.DishCodes.DISH_CREATE_CODE);
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
                var objectT = new NetworkData.Dish
                {
                    Name = (string)param[0]
                };

                return new Action_worker().HandleWorker(state, Newtonsoft.Json.JsonConvert.SerializeObject(objectT), NetworkResponseCodes.DishCodes.DISH_CREATE_CODE);
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
