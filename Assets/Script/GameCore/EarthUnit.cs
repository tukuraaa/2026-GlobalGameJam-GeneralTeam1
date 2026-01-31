using UnityEngine;
using R3;

public class EarthUnit:Singleton<EarthUnit>
{
    [SerializeField]
    int _initHp = 100;
    public ReactiveProperty<int> LifePoint = new ReactiveProperty<int>(10);

    override protected void Awake()
    {
        base.Awake();
        LifePoint.Value = _initHp;       
    }

    public Vector2 GetImpactPos()
    {
        return (Vector3)(Random.insideUnitCircle * transform.lossyScale.x) // sphere is same scale xyz.
            + transform.position;
    }
}