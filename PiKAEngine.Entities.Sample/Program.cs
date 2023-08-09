using PiKAEngine.DebugSystem;
using PiKAEngine.Entities.Sample;

var kettle = new Kettle();
kettle.OnLogged.Subscribe(Console.WriteLine);
var gameEntityManager = new GameEntityManager(kettle);

var testGameEntity = new TestGameEntity(gameEntityManager);

for (var i = 0;; i++)
{
    Console.ReadKey(true);
    Console.WriteLine($"---------------{i}---------------");
    gameEntityManager.Update();
}