using ServerDb.ServerData;

namespace ServerDb.Signals
{
    public static class IHandle
    {
        public delegate bool Handler(string payload, StateObject state);
    }
}
