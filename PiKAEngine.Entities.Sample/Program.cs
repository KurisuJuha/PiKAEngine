using PiKAEngine.DebugSystem;
using PiKAEngine.Entities.Sample;

Console.WriteLine("Hello, World!");

var kettle = new Kettle();
kettle.OnLogged.Subscribe(Console.WriteLine);
var gameEntityManager = new GameEntityManager(kettle);
var testGameEntity = new TestGameEntity(gameEntityManager);

gameEntityManager.RegisterEntity(testGameEntity);

while (true)
{
    Console.ReadKey(true);

    Console.WriteLine("--------------------------");
    gameEntityManager.Update();
}