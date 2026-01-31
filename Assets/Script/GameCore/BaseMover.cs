
using UnityEngine;

public class BaseMover : MonoBehaviour
{
    public bool IsInit = false;
    float _currentTime;
    float _arrivalTime;
    Vector3 _targetPos;

    public void Initialize(float arrivalTime, Vector3 targetPos)
    {
        IsInit = true;
        _currentTime = 0f;
        _arrivalTime = arrivalTime;
        _targetPos = targetPos;
    }

    void Update()
    {
        doMove();
    }

    virtual public void doMove()
    {
        if(IsInit)
        {
            _currentTime += Time.deltaTime;

            float t = _currentTime / _arrivalTime; // 0~1
            transform.position = Vector3.Lerp(transform.position, _targetPos, t);
        }
    }

}