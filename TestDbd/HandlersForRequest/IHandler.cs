using ServerDB.ServerData;

namespace ServerDB.HandlersForRequest
{
    public interface IHandler
    {
        void hand(StateObject state);
    }
}
