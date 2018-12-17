using ServerDb.ServerData;
using System;

namespace ServerDb.HandlersForRequest
{
    class GetDish : IHandler
    {
        private string Payload = "";

        public GetDish(string payload)
        {
            this.Payload = payload;
        }
        public void hand(StateObject state)
        {
            try
            {
                var objectSend = Newtonsoft.Json.JsonConvert.DeserializeObject<ModelForTransmition.Dish>(state.MessageForReciveData.ToString());
                var dishes = DatabaseMiddlware.Workers.Dishes.DishHandler.GetDishes(objectSend.Payload.offsetAboutFirst, objectSend.Payload.countDishes);

                //Console.WriteLine($"Prepare data from db: {Newtonsoft.Json.JsonConvert.SerializeObject(dishes)}");
                ServerHandlers.SendHandler.Send(state.WorkSocket, Newtonsoft.Json.JsonConvert.SerializeObject(dishes));
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error for handle! Code error: {ex.Message}");

            }
        }
    }
}
