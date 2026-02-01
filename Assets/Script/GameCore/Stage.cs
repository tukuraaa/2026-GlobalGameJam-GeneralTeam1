using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using R3;
using System;

public class Stage : Singleton<Stage>
{
    public EarthUnit EarthUnit;
    public List<Shooter> Shooters;
    int _maxLevel = DataConst.MaxLevel;
    public ReadOnlyReactiveProperty<int> NowLevel => _nowLevel.ToReadOnlyReactiveProperty();
    ReactiveProperty<int> _nowLevel = new(1);
    float _upLevelInterval = DataConst.UpLevelInterval;
    float _totalTime = 0f;
    int _playerLayer;
    int _defaultLayer;

    void Start()
    {
        _playerLayer = LayerMask.GetMask("Player");
        _defaultLayer = LayerMask.GetMask("Default");

        // Initialize or find EarthUnit and Shooters in the scene
        foreach (var shooter in Shooters)
        {
            shooter.OnHit().Subscribe(
                (hitMsg) =>
                {
                    DoHitReaction(hitMsg);
                }
            ).AddTo(this);
        }
    }

    void DoHitReaction(ShooterMsg hitMsg)
    {
        int hitObjLayer = 1 << hitMsg.HitObj.gameObject.layer;

        // hit earth
        if (hitObjLayer == _defaultLayer)
        {
            if (hitMsg.DamageObj.BaseDamagePoint >= 0) // damage
            {
                EarthUnit.LifePoint.Value -= hitMsg.DamageObj.BaseDamagePoint;
                AudioManager.Instance.PlayOneShotSe(hitMsg.DamageObj.HitEarthSe);
            }
            Debug.Log($"EarthUnit Hit! Remaining LifePoint: {EarthUnit.LifePoint.Value}");
        }
        else if (hitObjLayer == _playerLayer)
        {
            AudioManager.Instance.PlayOneShotSe(hitMsg.DamageObj.HitPlayerSe);

            if (hitMsg.DamageObj.BaseDamagePoint < 0)
            {
                // heal
                EarthUnit.LifePoint.Value -= hitMsg.DamageObj.BaseDamagePoint;
            }

            if (hitMsg.DamageObj.BaseScorePoint > 0)
            {
                // スコア増える処理
            }
            Debug.Log("Player Hit");
        }
    }

    void FixedUpdate()
    {
        _totalTime += Time.fixedDeltaTime;
        if(_totalTime >= _upLevelInterval && _nowLevel.Value < _maxLevel)
        {
            _nowLevel.Value++;
            _totalTime = _totalTime - _upLevelInterval;
            Debug.Log($"Level Up! Now Level: {_nowLevel.Value}");
            checkLevelOpenShooter();
        }
    }

    //�����p�����[�^����
    void checkLevelOpenShooter()
    {
        if(NowLevel.CurrentValue > 6)
        {
            if(Shooters.Count > 2 && !Shooters[3].gameObject.activeSelf)
            {
                Shooters[3].gameObject.SetActive(true);
            }
        }
        else if(NowLevel.CurrentValue > 3)
        {
            if(Shooters.Count > 1 && !Shooters[2].gameObject.activeSelf)
            {
                Shooters[2].gameObject.SetActive(true);
            }
        }                       
        else if(NowLevel.CurrentValue > 1)
        {
            if(Shooters.Count > 0 && !Shooters[1].gameObject.activeSelf)
            {
                Shooters[1].gameObject.SetActive(true);
            }
        }   
    }

}