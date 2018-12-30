using AdminPanel.Models.Payloads;
using System;

namespace AdminPanel.NetworkMiddleware.HandlersAfterRequesting
{
    public class HandleAuthentification : IActiontResponse
    {
        public void Handle(StateObject state)
        {
            var messageFromServer = state.MessageForRecieving.ToString();

            try
            {
                var payload = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthPayload>(messageFromServer);
            }
            catch(Exception ex)
            {
                //TODO
            }

        }
    }
}
