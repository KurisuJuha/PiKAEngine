namespace PiKAEngine.Entities;

public abstract class ComponentsAddableEntityBase<TEntity> : EntityBase<TEntity>
    where TEntity : EntityBase<TEntity>
{
    protected ComponentsAddableEntityBase(EntityManagerBase<TEntity> entityManagerBase,
        bool registerEntityManager = true) : base(entityManagerBase, registerEntityManager)
    {
    }
}