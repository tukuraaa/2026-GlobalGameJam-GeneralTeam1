using UnityEngine;
using R3;
using System.Collections.Generic;
using System.Linq;

public class EarthUnit : MonoBehaviour
{
    //barrier表現
    [SerializeField]
    GameObject _barrierObj;
    float _barrierLeftTime = 0f;
    HashSet<int> _skillID = new HashSet<int>() {1, 2};
    //

    [SerializeField]
    int _initHp = 100;
    public ReactiveProperty<int> LifePoint = new ReactiveProperty<int>(10);
    [SerializeField]
    AudioClip _deadSound;
    List<BarrierSkillData> _barrierSkill = new ();

    protected void Awake()
    {
        LifePoint.Subscribe((value) =>
        {
            if(LifePoint.Value <= 0)
            {
                AudioManager.Instance.PlayOneShotSe(_deadSound);
            }
        }).AddTo(this);

        LifePoint.Value = _initHp;

        _barrierSkill.Add(new BarrierSkillData() { SkillId = 1 });
        _barrierSkill.Add(new BarrierSkillData() { SkillId = 2 });  
    }

    public void ActiveBarrier(int id, Player player)
    {
        if(!_skillID.Contains(id)) return;

        var barrier = _barrierSkill.FirstOrDefault(b => b.SkillId == id);
        if(barrier == null) return;
        barrier.Init(DataConst.BarrierTime);
        
        //バリア表現
        _barrierObj.SetActive(true);
        _barrierLeftTime = DataConst.BarrierTime;

        _skillID.Remove(id);
    }

    public Vector2 GetImpactPos()
    {
        return (Vector3)(Random.insideUnitCircle * transform.lossyScale.x) // sphere is same scale xyz.
            + transform.position;
    }

    public bool IsBarrierActive()
    {
        foreach (var barrier in _barrierSkill)
        {
            if(barrier.IsActive)
            {
                return true;
            }
        }
        return false;
    }

    public void DoDamage(int dmgValue)
    {
        if(dmgValue > 0 && IsBarrierActive())
        {
            return;
        }
        LifePoint.Value -= dmgValue;
    }

    public void Update()
    {
        foreach(var barrier in _barrierSkill)
        {
            if(barrier.IsActive)
            {
                barrier.DoUpdate(Time.deltaTime);
            }
        }

        if(_barrierLeftTime > 0)
        {
            _barrierLeftTime -= Time.deltaTime;
        }
        if(IsBarrierActive() == false)
        {
            //バリア表現解除
            _barrierObj.SetActive(false);
        }
    }
}