using ServerDb.ServerData;
using DatabaseMiddlware.Workers.Users;
using ServerDb.ModelForTransmition;
using System;
using Newtonsoft.Json;

namespace ServerDb.HandlersForRequest
{
    class Authentification : IHandler
    {
        public static string ActionType = "authentification";
        public string Action = ActionType;
        public string Payload = "";

        private static string PatternForCondition = "";

        public Authentification() { }

        public Authentification(string payload)
        {
            this.Payload = payload;
        }

        public void hand(StateObject state)
        {
            try
            {
                Console.WriteLine($"Message auth: {state.MessageForReciveData.ToString()}");
                var authTok = JsonConvert.DeserializeObject<Auth>(state.MessageForReciveData.ToString());

                //if (UserHandler.Authentificate(authTok.Payload))
                //{
                //    ServerHandlers.SendHandler.Send(state.WorkSocket, $"authentificate succes! token : {Guid.NewGuid()}");
                //}
                //else
                //{
                //    ServerHandlers.SendHandler.Send(state.WorkSocket, "authentificate error! error code: User does not exist in database!");
                //}
            }
            catch(Exception ex)
            {
                ServerDb.Server.NewExceptionRequest(ex.Message);
                ServerHandlers.SendHandler.Send(state.WorkSocket, "authentificate error! Does not prepare recieved data !");
            }
        }
    }
}
