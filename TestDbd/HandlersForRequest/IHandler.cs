using ServerDb.ServerData;

namespace ServerDb.HandlersForRequest
{
    public interface IHandler
    {
        void hand(StateObject state);
    }
}
