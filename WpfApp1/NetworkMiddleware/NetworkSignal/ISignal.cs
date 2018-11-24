namespace AdminPanel.NetworkMiddleware.NetworkSignal
{
    interface ISignal
    {
        bool Handle(params object []param);
    }
}
