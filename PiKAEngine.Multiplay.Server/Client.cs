namespace PiKAEngine.MultiPlay.Server;

public class Client
{
    public Client()
    {
        LatestInput = Array.Empty<byte>();
    }

    public byte[] LatestInput { get; private set; }

    public void UpdateInput(byte[] input)
    {
        LatestInput = input;
    }
}