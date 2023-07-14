namespace PiKAEngine.MultiPlay.Server;

public class MultiPlayServer : IDisposable
{
    private readonly Dictionary<Guid, Client> _clients = new();
    private readonly INetworkServer _networkServer;
    private readonly IDisposable _onConnectedDisposable;
    private readonly IDisposable _onDisconnectedDisposable;

    public MultiPlayServer(INetworkServer networkServer)
    {
        _networkServer = networkServer;
        _onConnectedDisposable = _networkServer.OnConnected.Subscribe(id => { _clients[id] = new Client(); });
        _onDisconnectedDisposable = _networkServer.OnDisconnected.Subscribe(id => { _clients.Remove(id); });
    }


    public void Dispose()
    {
        _onConnectedDisposable.Dispose();
        _onDisconnectedDisposable.Dispose();
    }

    public void Update()
    {
        
    }

    private void BroadCast(byte[] message)
    {
        foreach (var id in _clients.Keys) _networkServer.SendMessage(id, message);
    }
}