namespace PiKAEngine.MultiPlay.Client;

public class MultiPlayClient
{
    private readonly INetworkClient _networkClient;
    
    public MultiPlayClient(INetworkClient networkClient)
    {
        _networkClient = networkClient;
    }
}