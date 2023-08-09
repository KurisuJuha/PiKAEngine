using PiKAEngine.DebugSystem;

namespace PiKAEngine.Entities.Sample;

public class GameEntityManager : EntityManagerBase<GameEntity, GameComponent>
{
    public GameEntityManager(Kettle kettle) : base(kettle)
    {
    }
}