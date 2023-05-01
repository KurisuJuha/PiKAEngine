using UnityEngine;
using UniRx;
using JuhaKurisu.PiKAEngine.Logics.Core.Entities;

public class Test : MonoBehaviour
{
    [SerializeField] private bool autoUpdate;
    [SerializeField] private int i;
    EntityManager manager;

    private void Start()
    {
        manager = new EntityManager();
        new Entity(manager, true);

        for (int i = 0; i < 10; i++)
        {
            OnChangedObserverComponent onChangedObserverComponent = new OnChangedObserverComponent();

            onChangedObserverComponent.onChanged.Subscribe(_ => i++);

            Entity entity = new Entity(manager, true, onChangedObserverComponent);
            entity.SubscribeUpdate();
        }

        for (int i = 0; i < 10000; i++)
        {
            new Entity(manager, true);
        }
    }

    private void Update()
    {
        if (!autoUpdate && Input.GetKeyDown(KeyCode.Space)) manager.Update();
    }

    private void FixedUpdate()
    {
        if (autoUpdate)
        {
            manager.Update();
            manager.Update();
        }
    }
}
