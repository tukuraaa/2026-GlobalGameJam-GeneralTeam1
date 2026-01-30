using UnityEngine;
using UnityEngine.Pool;
using System.Collections;
using Cysharp.Threading.Tasks;
using R3;

public class ShooterMsg
{
    public DamageObj DamageObj;
    public Collider HitObj;
}
public class Shooter : MonoBehaviour
{
    [SerializeField]
    float _xRange = 3f;
    [SerializeField]
    float _yRange = 0;
    [SerializeField]
    GameObject _bulletPrefab;
    //todo: ObjectPool
    //ObjectPool<GameObject> _bulletPool;
    [SerializeField]
    float _shootWaveInterval = 1f;
    [SerializeField]
    float _shootInterval = 0.1f;
    [SerializeField]
    int _shootWaveCount = 3;
    [SerializeField]
    float _bulletSpeed = 5f;
    Vector3 _targetPosition;

    Subject<ShooterMsg> _hitSubject = new Subject<ShooterMsg>();
    public Observable<ShooterMsg> OnHit() => _hitSubject.AsObservable();
    
    private bool _isShooting = false;

    void Start()
    {
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

    protected async UniTask Shooting()
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
            //発射位置
            Vector3 randomOffset = new Vector3(
                Random.Range(-_xRange, _xRange),
                Random.Range(-_yRange, _yRange),
                0
            );
            Vector3 shootPosition = transform.position + randomOffset;
            
            // ターゲット位置と方向
            _targetPosition = Stage.Instance.EarthUnit.transform.position;
            Vector3 direction = (_targetPosition - shootPosition).normalized;
            
            // 弾の発射
            ShootBullet(shootPosition, direction);
            
            // 単発の間隔時間を待つ
            if (i < _shootWaveCount - 1)
            {
                await UniTask.Delay((int)(_shootInterval * 1000));
            }
        }
    }

    protected void ShootBullet(Vector3 position, Vector3 direction)
    {
        if (_bulletPrefab != null)
        {
            GameObject bullet = Instantiate(_bulletPrefab, position, transform.rotation);
            var mover = bullet.GetComponent<BaseMover>();
            if (mover != null)
            {
                mover.MoveDirection = direction;
                mover.MoveSpeed = _bulletSpeed;
            }

            var damageObj = bullet.GetComponent<DamageObj>();
            if (damageObj != null)
            {
                damageObj.OwnerShooter = this;
                damageObj.Lifetime.Value = 5f; // 弾の寿命を設定
            }
        }
    }

    public void OnDamageObjHit(DamageObj damageObj, Collider other)
    {
        _hitSubject.OnNext(new ShooterMsg(){DamageObj = damageObj, HitObj = other});
    }
}