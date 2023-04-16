using System;
using System.Collections.ObjectModel;
using UniRx;

namespace JuhaKurisu.PiKAEngine.Logics.Core.Entities
{
    public class EntityManager
    {
        public readonly ReadOnlyCollection<EntityComponent> baseComponents;
        public IObservable<Unit> onUpdate => onUpdateSubject;
        private readonly Subject<Unit> onUpdateSubject = new();

        public EntityManager(params EntityComponent[] baseComponents)
        {
            this.baseComponents = new(baseComponents);
        }

        public void Update()
        {
            onUpdateSubject.OnNext(Unit.Default);
        }
    }
}