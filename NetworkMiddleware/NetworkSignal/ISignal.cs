namespace AdminPanel.NetworkMiddleware.NetworkSignal
{
    interface ISignal
    {
        bool Handle(StateObject state, params object []param);
        bool HandleSet(StateObject state, params object[] param);
        bool HandleUpdate(StateObject state, params object[] param);
        bool HandleDelete(StateObject state, params object[] param);
    }
}
