using UnityEngine;

public class SinWaveMover : BaseMover
{
    Vector3 _linearPos;
    Vector3 _unitVec;
    //[SerializeField]
    float _sinSpeed = 1.5f;
    float _sinOffset = 1f;
    float _amp = 2f;
    float _ampOffset = 0.5f;

    public override void Initialize(float arrivalTime, Vector3 targetPos)
    {
        base.Initialize(arrivalTime, targetPos);

        _linearPos = transform.position;
        _unitVec = Vector3.Cross(targetPos - _linearPos, Vector3.forward).normalized;
        Debug.Log(_linearPos);
        // �U���̐ݒ�������ł���H

        _sinSpeed += Random.Range(-_sinOffset, _sinOffset) * DataConst.SinSpeedRate(Stage.Instance.NowLevel.CurrentValue);
        _amp += Random.Range(-_ampOffset, _ampOffset);
    }

    public override void doMove()
    {
        _currentTime += Time.deltaTime;

        float t = _currentTime / _arrivalTime; // 0~1
        _linearPos = Vector3.Lerp(_linearPos, _targetPos, t);

        var amp = Mathf.Sin(_currentTime * _sinSpeed) * _amp; // -amp ~ amp
        transform.position = _linearPos + _unitVec * amp;
    }
}
