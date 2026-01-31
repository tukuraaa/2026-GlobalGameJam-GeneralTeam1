using R3;
using UnityEngine;

public class DamageObj : MonoBehaviour
{
    //config data
    public LayerMask targetLayer;
    public ReactiveProperty<float> Lifetime = new ReactiveProperty<float>(-1f); // -1 means infinite
    public int MaxTargets = -1; // -1 means unlimited
    public float ContinuousInterval = 0f; // Interval for continuous damage, 0 means no continuous damage
    public int BaseDamagePoint = 1;
    public int HitTimes = 1;

    public Shooter OwnerShooter;

    void Awake()
    {
        Lifetime.Subscribe((value)=>
        {
            if (Lifetime.Value > 0)
                OwnerShooter.BulletPool.Release(this.gameObject);
        }).AddTo(this);
    }
    void OnTriggerEnter(Collider other)
    {
        this.gameObject.SetActive(false);
        OwnerShooter?.OnDamageObjHit(this, other);        
    }
}