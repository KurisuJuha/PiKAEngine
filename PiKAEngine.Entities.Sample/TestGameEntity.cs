﻿namespace PiKAEngine.Entities.Sample;

public class TestGameEntity : GameEntity
{
    public TestGameEntity(GameEntityManager entityManager) : base(entityManager)
    {
    }

    protected override void InitializeEntity()
    {
        Console.WriteLine("Initialize");
    }

    protected override void StartEntity()
    {
        Console.WriteLine("Start");

        Activate();
        Console.WriteLine("Activate!!!!!!!!!!!!!");
    }

    protected override void UpdateEntity()
    {
        Console.WriteLine("Update");
    }
}