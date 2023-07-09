using Microsoft.Xna.Framework;

public class Application : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private long _tick;

    public Application()
    {
        _graphics = new GraphicsDeviceManager(this);
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        _tick += 1;
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(_tick / 5 % 2 == 0 ? Color.Red : Color.Blue);

        base.Draw(gameTime);
    }
}