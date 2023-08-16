using PiKAEngine.DebugSystem;

namespace PiKAEngine.Entities.Sample;

public class GameEntityManager : EntityManagerBase<GameEntity, GameComponent, GameEntityManager>
{
    public GameEntityManager(Kettle kettle = null) : base(kettle)
    {
    }
}