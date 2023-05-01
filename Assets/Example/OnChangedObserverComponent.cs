using UnityEngine;
using JuhaKurisu.PiKAEngine.Logics.Core.Entities;

public class OnChangedObserverComponent : EntityComponent
{
    protected override void ComponentStart()
    {
        isActive = true;
        Debug.Log("componentStart");
    }

    protected override void ComponentUpdate()
    {
        NotifyChanged();
    }

    public override EntityComponent Copy()
        => new OnChangedObserverComponent();

    protected override void DisposeComponent() { }
}