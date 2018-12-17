using ServerDb.ServerData;
using ServerDb.Signals;
using System;

namespace ServerDb.HandlersForRequest
{
    public class HandlerRequestBuilder
    {
        public static IHandle.Handler RequestBuild(StateObject state)
        {
            try
            {
                var requestState = Newtonsoft.Json.JsonConvert.DeserializeObject<RequestAll>(state.MessageForReciveData.ToString());
                switch (requestState.Code)
                {
                    case TestConnectionSignal.ActionType:
                        return new TestConnectionSignal().HandleGet;
                    case AuthentificateSignal.ActionType:
                        return new AuthentificateSignal().HandleGet;
                    case DishSiganl.ActionTypeGet:
                        return new DishSiganl().HandleGet;
                    case DishSiganl.ActionTypeCreate:
                        return new DishSiganl().HandleCreate;
                    case CooperatorSignal.ActionTypeGet:
                        return new CooperatorSignal().HandleGet;
                    case CooperatorSignal.ActionTypeCreate:
                        return new CooperatorSignal().HandleCreate;
                    case MenuSignal.ActionTypeGet:
                        return new MenuSignal().HandleGet;
                    case MenuSignal.ActionTypeCreate:
                        return new MenuSignal().HandleCreate;
                    case StreetSignal.ActionTypeGet:
                        return new StreetSignal().HandleGet;
                    case StreetSignal.ActionTypeCreate:
                        return new StreetSignal().HandleCreate;
                    case ProductSignal.ActionTypeGet:
                        return new ProductSignal().HandleGet;
                    case ProductSignal.ActionTypeCreate:
                        return new ProductSignal().HandleCreate;
                    case UserSignal.ActionTypeGet:
                        return new UserSignal().HandleGet;
                    case UserSignal.ActionTypeCreate:
                        return new UserSignal().HandleCreate;
                    case PositionSignal.ActionTypeGet:
                        return new PositionSignal().HandleGet;
                    case PositionSignal.ActionTypeCreate:
                        return new PositionSignal().HandleCreate;
                    case CategorySignal.ActionTypeGet:
                        return new CategorySignal().HandleGet;
                    case CategorySignal.ActionTypeCreate:
                        return new CategorySignal().HandleCreate;
                    default:
                        return new NullSignal().HandleGet;
                }
            }
            catch(Exception ex)
            {
                return new NullSignal().HandleGet;
            }
        }
    }
}
