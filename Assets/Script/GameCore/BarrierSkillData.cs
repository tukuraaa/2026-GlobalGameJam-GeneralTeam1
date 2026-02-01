using UnityEngine;

public class BarrierSkillData
{
    public int SkillId;    
    public bool IsActive => _isActive;
    bool _isActive = false;
    public float NowTime => _nowTime;
    float _nowTime = 0;
    float _totalTime = 0;    
    public void Init(float time)
    {
        _nowTime = 0;
        _totalTime = time;
        _isActive = true;
    }

    public void DoUpdate(float deltaTime)
    {
        _nowTime += deltaTime;
        if(_nowTime >= _totalTime)
        {
            _isActive = false;
        }
        else
        {
            _isActive = true;
        }
    }
}