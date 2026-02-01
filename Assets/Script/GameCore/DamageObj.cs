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
    public int BaseScorePoint = 0;
    public int HitTimes = 1;

    private bool _isFirstInitialize = true;
    public Shooter OwnerShooter;
    public AudioClip HitPlayerSe = null;
    public AudioClip HitEarthSe = null;
    public AudioClip HitBarrierSe = null;

    public void Initialize(Shooter owner, float lifetime)
    {
        if (_isFirstInitialize)
        {
            InitializeOnce(owner);
        }

        Lifetime.Value = lifetime;
    }

    private void InitializeOnce(Shooter owner)
    {
        OwnerShooter = owner;
        Lifetime.Subscribe((value) =>
        {
            if (Lifetime.Value > 0 || Lifetime.Value == -1) return;

            if(OwnerShooter.GetType() == typeof(ItemShooter))
            {
                // ItemShooterはItemを一つだけ出す想定なので、オブジェクトプールを使用しない
                GetComponent<BaseMover>().IsInit = false;
                gameObject.SetActive(false);
            }
            else
            {
                OwnerShooter.BulletPool.Release(this.gameObject);
            }
        }).AddTo(this);

        _isFirstInitialize = false;
    }

    void Update()
    {
        if (Lifetime.Value > 0)
        {
            Lifetime.Value -= Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if ((targetLayer.value & (1 << other.gameObject.layer)) == 0) return;

        this.gameObject.SetActive(false);
        OwnerShooter?.OnDamageObjHit(this, other);        
    }
}