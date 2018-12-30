using AdminPanel.Models;
using Newtonsoft.Json;
using System;

namespace AdminPanel.NetworkMiddleware.NetworkSignal
{
    public class Authentification_action : ISignal
    {
        public const string ActionTypeGet = "authentification";

        public string Action = ActionTypeGet;

        public bool Handle(StateObject state, params object[] param)
        {
            try
            {
                if (param.Length != 2)
                    throw new ArgumentException("autentification payload must be contain username and pasword!");

                var Payload = new AuthBLock { Username = (string)param[0], Password = (string)param[1], Enter = DateTime.Now };
                
                return new Action_worker().HandleWorker(state, Newtonsoft.Json.JsonConvert.SerializeObject(Payload), NetworkResponseCodes.AuthentificationCodes.AUTH_CODE);
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
                var id = (Guid)param[0];

                return new Action_worker().HandleWorker(state, Newtonsoft.Json.JsonConvert.SerializeObject(id), NetworkResponseCodes.AuthentificationCodes.AUTH_CODE);
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool HandleSet(StateObject state, params object[] param)
        {
            return false;
        }

        public bool HandleUpdate(StateObject state, params object[] param)
        {
            return false;
        }
    }
}
