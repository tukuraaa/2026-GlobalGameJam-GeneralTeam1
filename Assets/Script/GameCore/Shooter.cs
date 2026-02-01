using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.Pool;

public class ShooterMsg
{
    public DamageObj DamageObj;
    public Collider HitObj;
}
public class Shooter : MonoBehaviour
{
    [SerializeField]
    protected GameObject _bulletPrefab;
    public ObjectPool<GameObject> BulletPool;

    [SerializeField]
    float _shootWaveInterval = 1f;
    [SerializeField]
    protected float _shootInterval = 0.1f;
    [SerializeField]
    int _shootWaveCount = 3;
    [SerializeField]
    protected float _arrivalTime = 5f;
    [SerializeField]
    float _height = 2f;

    Subject<ShooterMsg> _hitSubject = new Subject<ShooterMsg>();
    public Observable<ShooterMsg> OnHit() => _hitSubject.AsObservable();
    
    protected bool _isShooting = false;

    protected virtual void Start()
    {
        BulletPool = new ObjectPool<GameObject>(
            createFunc: () =>
            {
                var bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
                return bullet;
            },
            actionOnGet: (obj) =>
            {
                obj.SetActive(true);
            },
            actionOnRelease: (obj) =>
            {
                obj.GetComponent<BaseMover>().IsInit = false;
                obj.SetActive(false);
            },
            actionOnDestroy: (obj) =>
            {
                // destroy
            },
            defaultCapacity: 10, //仮
            maxSize: 100 // 仮
        );


        Stage.Instance.NowLevel.Subscribe((level) =>
        {
            // レベルに応じてパラメータを変更
            _arrivalTime = DataConst.ShooterArriveTime(level);
            _shootWaveCount = DataConst.ShootWaveCount(level);
            _shootWaveInterval = DataConst.ShootWaveInterval(level);
        }).AddTo(this);
        
        StartShooting();
    }

    public void StartShooting()
    {
        if (!_isShooting)
        {
            _isShooting = true;
            Shooting().ToCancellationToken(this.GetCancellationTokenOnDestroy());
        }
    }

    public void StopShooting()
    {
        _isShooting = false;
    }

    protected virtual async UniTask Shooting()
    {
        while (_isShooting)
        {
            await ShootWave();
            await UniTask.Delay((int)(_shootWaveInterval * 1000));
        }
    }

    protected async UniTask ShootWave()
    {
        for (int i = 0; i < _shootWaveCount; i++)
        {
            // 落下地点
            // xy基準かxz基準かをshooterごとに設定する処理が必要になる
            Vector3 targetPosition = Stage.Instance.EarthUnit.GetImpactPos();

            // 発射地点
            Vector3 vec = Stage.Instance.EarthUnit.transform.position - targetPosition;
            Vector3 shootPosition = vec * _height;

            // 弾の発射
            ShootBullet(shootPosition, targetPosition);
            
            // 単発の間隔時間を待つ
            if (i < _shootWaveCount - 1)
            {
                await UniTask.Delay((int)(_shootInterval * 1000));
            }
        }
    }

    protected void ShootBullet(Vector3 shootPosition, Vector3 targetPosition)
    {
        var bullet = BulletPool.Get();
        bullet.transform.position = shootPosition;

        var damageObj = bullet.GetComponent<DamageObj>();
        if (damageObj != null)
        {
            damageObj.Initialize(this, _arrivalTime + 1f); // 弾の寿命を設定 （+1はバッファ）
        }

        var mover = bullet.GetComponent<BaseMover>();
        if (mover != null)
        {
            mover.Initialize(_arrivalTime, targetPosition);
        }

         var psList = bullet.GetComponentsInChildren<ParticleSystem>(true);
        foreach (var ps in psList)
        {
            resetPs(ps).Forget();
        }
    }

    protected async UniTaskVoid resetPs(ParticleSystem ps)
    {
        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        ps.Clear(true);
        await UniTask.DelayFrame(0);
        ps.Play();
    }

    public void OnDamageObjHit(DamageObj damageObj, Collider other)
    {
        _hitSubject.OnNext(new ShooterMsg(){DamageObj = damageObj, HitObj = other});
    }
}