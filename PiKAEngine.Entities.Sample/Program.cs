using PiKAEngine.Entities.Sample;

Console.WriteLine("Hello, World!");

var gameEntityManager = new GameEntityManager();
var testGameEntity = new TestGameEntity(gameEntityManager);

gameEntityManager.RegisterEntity(testGameEntity);

while (true)
{
    Console.ReadKey(true);

    Console.WriteLine("--------------------------");
    gameEntityManager.Update();
}