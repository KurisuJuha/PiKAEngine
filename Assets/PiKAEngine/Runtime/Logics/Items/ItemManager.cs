using System;
using System.Collections.ObjectModel;
using UniRx;

namespace JuhaKurisu.PiKAEngine.Logics.Items
{
    public class ItemManager
    {
        public readonly ReadOnlyCollection<ItemComponent> baseComponents;
        public IObservable<Unit> onUpdate => onUpdateSubject;
        private readonly Subject<Unit> onUpdateSubject = new();

        public ItemManager(ItemComponent[] baseComponents)
        {
            this.baseComponents = new(baseComponents);
        }

        public void Update()
        {
            onUpdateSubject.OnNext(Unit.Default);
        }
    }
}