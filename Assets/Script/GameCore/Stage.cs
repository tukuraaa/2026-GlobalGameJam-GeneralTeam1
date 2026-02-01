using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using R3;
using System;

public class Stage : Singleton<Stage>
{
    public EarthUnit EarthUnit;
    public Player Player;
    public List<Shooter> Shooters;
    int _maxLevel = DataConst.MaxLevel;
    public ReadOnlyReactiveProperty<int> NowLevel => _nowLevel.ToReadOnlyReactiveProperty();
    ReactiveProperty<int> _nowLevel = new(1);
    float _upLevelInterval = DataConst.UpLevelInterval;
    float _totalTime = 0f;

    void Start()
    {
        // Initialize or find EarthUnit and Shooters in the scene
        foreach (var shooter in Shooters)
        {
            shooter.OnHit().Subscribe(
                (hitMsg) =>
                {
                    if(hitMsg.HitObj.gameObject.GetInstanceID() == EarthUnit.gameObject.GetInstanceID())
                    {
                        EarthUnit.DoDamage(hitMsg.DamageObj.BaseDamagePoint);
                        //Debug.Log($"EarthUnit Hit! Remaining LifePoint: {EarthUnit.LifePoint.Value}");
                    }
                    else if(hitMsg.HitObj.gameObject.GetInstanceID() == Player.gameObject.GetInstanceID())
                    {
                        //Debug.Log("Player Hit");                        
                    }
                }
            ).AddTo(this);
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

    //汚いパラメータ調整
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