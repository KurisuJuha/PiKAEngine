namespace PiKAEngine.Entities.Sample;

public abstract class GameEntity : EntityBase<GameEntity, GameComponent, GameEntityManager>
{
    protected GameEntity(GameEntityManager entityManager) : base(entityManager)
    {
    }
}