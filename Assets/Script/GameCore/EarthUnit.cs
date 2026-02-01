using UnityEngine;
using R3;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using R3.Triggers;

public class EarthUnit : MonoBehaviour
{
    //barrier表現
    [SerializeField]
    GameObject _barrierObj;
    [SerializeField]
    GameObject _barrierActBulletObj;
    [SerializeField]
    Material _barrierMat;
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


        var barrierActObj = Instantiate(
            _barrierActBulletObj,
            player.transform.position,
            Quaternion.identity
        );

        _skillID.Remove(id);

        doBarrierEffect(barrierActObj, barrier).Forget();
    }

    async UniTaskVoid doBarrierEffect(GameObject barrierActObj, BarrierSkillData barrier)
    {
        const float moveTime = 2f;
        barrierActObj.GetComponent<BaseMover>().Initialize(
            moveTime,
            this.transform.position            
        );

        bool isHit = false;
        barrierActObj.OnTriggerEnterAsObservable()
            .Subscribe((other) =>
            {
                if(other.gameObject.GetInstanceID() == this.gameObject.GetInstanceID())
                {
                    isHit = true;
                }
            }).AddTo(barrierActObj);

        await UniTask.WaitUntil(() => isHit);
        Destroy(barrierActObj);

        barrier.Init(DataConst.BarrierTime);
        
        //バリア表現
        _barrierObj.SetActive(true);
        _barrierLeftTime = DataConst.BarrierTime;        
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

            _barrierMat.SetFloat("_barrier_level", (DataConst.BarrierTime - _barrierLeftTime) / (DataConst.BarrierTime * 0.25f));
        }
        if(IsBarrierActive() == false)
        {
            //バリア表現解除
            _barrierObj.SetActive(false);
        }
    }
}