namespace PiKAEngine.Entities.Sample;

public abstract class GameEntity : EntityBase<GameEntity, GameComponent>
{
    protected GameEntity(EntityManagerBase<GameEntity, GameComponent> entityManagerBase,
        bool registerEntityManager = true) : base(entityManagerBase, registerEntityManager)
    {
    }
}