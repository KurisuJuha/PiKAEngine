namespace PiKAEngine.MultiPlay.Server;

public interface INetworkServer
{
    IObservable<(Guid, IEnumerable<byte>)> OnMessage { get; }
    IObservable<Guid> OnConnected { get; }
    IObservable<Guid> OnDisconnected { get; }
    void SendMessage(Guid id, IEnumerable<byte> message);
}