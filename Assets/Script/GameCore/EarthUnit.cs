using UnityEngine;
using R3;

public class EarthUnit:Singleton<EarthUnit>
{
    public ReactiveProperty<int> LifePoint = new ReactiveProperty<int>(3);

    public Vector2 GetImpactPos()
    {
        return (Vector3)(Random.insideUnitCircle * transform.lossyScale.x) // sphere is same scale xyz.
            + transform.position;
    }
}