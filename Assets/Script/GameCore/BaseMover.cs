
using UnityEngine;

public class BaseMover : MonoBehaviour
{
    [HideInInspector]
    public bool IsInit = false;
    protected float _currentTime;
    protected float _arrivalTime;
    protected Vector3 _targetPos;

    virtual public void Initialize(float arrivalTime, Vector3 targetPos)
    {
        IsInit = true;
        _currentTime = 0f;
        _arrivalTime = arrivalTime;
        _targetPos = targetPos;
    }

    void Update()
    {
        if (IsInit)
        {
            doMove();
        }
    }

    virtual public void doMove()
    {
        _currentTime += Time.deltaTime;

        float t = _currentTime / _arrivalTime; // 0~1
        transform.position = Vector3.Lerp(transform.position, _targetPos, t);
    }

}