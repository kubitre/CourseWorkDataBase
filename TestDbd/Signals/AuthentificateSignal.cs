using DatabaseMiddlware.ModelForTransmition;
using ServerDb.ServerData;
using ServerDb.SignalsData;
using System;
using System.Text;

namespace ServerDb.Signals
{
    public class AuthentificateSignal : ISignal
    {
        public const string ActionType = "authentification";
        public string Payload;

        public AuthentificateSignal()
        {
        }

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
                var requestTest = Newtonsoft.Json.JsonConvert.DeserializeObject<RequestAll>(Payload);
                var AuthPayload = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthData>(requestTest.RequestBody);

                var resultRequest = DatabaseMiddlware.Workers.Users.UserHandler.Authentificate(new AuthBLock() { Username = AuthPayload.Username, Password = AuthPayload.Password, Enter = AuthPayload.Enter });

                return ActionWorker.FinishingTaskSolve(ActionType, resultRequest == -1 ? false : true, resultRequest.ToString(), state);
            }
            catch(Exception ex)
            {
                return ActionWorker.FinishingTaskSolve(ActionType, false, ex.Message, state);
            }
        }

        public bool HandleUpdate(string Payload, StateObject state)
        {
            throw new NotImplementedException();
        }
    }
}
