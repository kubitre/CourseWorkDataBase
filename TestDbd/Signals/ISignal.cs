using ServerDb.ServerData;

namespace ServerDb.Signals
{
    interface ISignal
    {
        bool HandleGet(string Payload, StateObject state);
        bool HandleCreate(string Payload, StateObject state);
        bool HandleUpdate(string Payload, StateObject state);
        bool HandleDelete(string Payload, StateObject state);
    }
}
