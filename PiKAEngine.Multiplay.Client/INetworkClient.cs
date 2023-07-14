namespace PiKAEngine.MultiPlay.Client;

public interface INetworkClient
{
    IObservable<IEnumerable<byte>> OnMessage { get; }
    void SendMessage(IEnumerable<byte> message);
}