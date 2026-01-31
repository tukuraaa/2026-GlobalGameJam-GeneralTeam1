using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using R3;

public class Stage : Singleton<Stage>
{
    public EarthUnit EarthUnit;
    public Player Player;
    public List<Shooter> Shooters;

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
                        if (hitMsg.DamageObj.BaseDamagePoint > 0) // damage
                        {
                            EarthUnit.LifePoint.Value -= hitMsg.DamageObj.BaseDamagePoint;
                        }
                        Debug.Log($"EarthUnit Hit! Remaining LifePoint: {EarthUnit.LifePoint.Value}");
                    }
                    else if(hitMsg.HitObj.gameObject.GetInstanceID() == Player.gameObject.GetInstanceID())
                    {
                        if(hitMsg.DamageObj.BaseDamagePoint < 0) // heal
                        {
                            EarthUnit.LifePoint.Value -= hitMsg.DamageObj.BaseDamagePoint;
                        }
                        if (hitMsg.DamageObj.BaseScorePoint > 0)
                        {
                            // ÉXÉRÉAëùÇ¶ÇÈèàóù
                        }
                        Debug.Log("Player Hit");
                    }
                }
            ).AddTo(this);
        }
    }
}