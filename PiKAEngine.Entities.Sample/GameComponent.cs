namespace PiKAEngine.Entities.Sample;

public abstract class GameComponent : ComponentBase<GameEntity, GameComponent, GameEntityManager>
{
    protected GameComponent(GameEntity entity) : base(entity)
    {
    }
}