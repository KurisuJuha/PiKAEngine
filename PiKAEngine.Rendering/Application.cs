using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PiKATools.Engine.Rendering;

public class Application : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _whiteRectangle;

    public Application()
    {
        _graphics = new GraphicsDeviceManager(this);
        IsMouseVisible = true;
        TargetElapsedTime = TimeSpan.FromSeconds(1d / 60);
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        base.LoadContent();

        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _whiteRectangle = new Texture2D(GraphicsDevice, 1, 1);
        _whiteRectangle.SetData(new[] { Color.White });
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        GraphicsDevice.Clear(Color.MediumAquamarine);

        _spriteBatch.Begin();

        _spriteBatch.Draw(_whiteRectangle, new Rectangle(10, 20, 30, 40), Color.White);

        _spriteBatch.End();
    }
}