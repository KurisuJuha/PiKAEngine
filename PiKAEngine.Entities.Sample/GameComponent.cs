namespace PiKAEngine.Entities.Sample;

public abstract class GameComponent : ComponentBase<GameEntity, GameComponent>
{
    protected GameComponent(GameEntity entity) : base(entity)
    {
    }
}