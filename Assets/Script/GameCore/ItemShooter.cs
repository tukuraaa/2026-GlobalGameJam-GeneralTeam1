using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ItemShooter : Shooter
{
    GameObject _item = null;

    protected override void Start()
    {
        doStart().Forget();
    }

    async UniTaskVoid doStart()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(5));
        StartShooting();
    }

    override protected void changeLevel(int level)
    {
        // // レベルに応じてパラメータを変更
        // _arrivalTime = DataConst.ShooterArriveTime(level);
        // _shootWaveCount = DataConst.ShootWaveCount(level);
        // _shootWaveInterval = DataConst.ShootWaveInterval(level);        
    }

    protected override async UniTask Shooting()
    {
        while (_isShooting)
        {
            await StandBy();
        }
    }

    protected async UniTask StandBy()
    {
        while (_item != null && _item.activeSelf)
        {
            // アイテムが消えるまで待機
            await UniTask.Delay((int)(Time.deltaTime * 1000));
        }

        await UniTask.Delay((int)(_shootInterval * 1000));

        ShotItem();
    }

    void ShotItem()
    {
        if(_item == null)
        {
            _item = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        }

        _item.transform.position = transform.position;

        var damageObj = _item.GetComponent<DamageObj>();
        if (damageObj != null)
        {
            damageObj.Initialize(this, -1f); // -1 弾を不死で設定
        }

        var mover = _item.GetComponent<BaseMover>();
        if (mover != null)
        {
            mover.Initialize(_arrivalTime, transform.position); // transform.position 仮
        }

        var psList = _item.GetComponentsInChildren<ParticleSystem>(true);
        foreach (var ps in psList)
        {
            resetPs(ps).Forget();
        }

        _item.SetActive(true);
    }
}


