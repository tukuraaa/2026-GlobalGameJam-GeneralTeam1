using UnityEngine;

public class ItemMover : BaseMover
{
    Vector3 _bottomLeft;
    Vector3 _topRight;

    public override void Initialize(float arrivalTime, Vector3 _)
    {
        base.Initialize(arrivalTime, _);
        var cam = Camera.main;
        var distanceZ = Stage.Instance.EarthUnit.transform.position.z - cam.transform.position.z; // ÉJÉÅÉâÇ∆ínãÖÇÃZé≤ãóó£
        _bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, distanceZ));
        _topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, distanceZ));

        SetNewTargetPosition();
    }

    public override void doMove()
    {
        _currentTime += Time.deltaTime;

        float t = _currentTime / _arrivalTime; // 0~1
        transform.position = Vector3.Lerp(transform.position, _targetPos, t);

        if (transform.position == _targetPos)
        {
            _currentTime = 0;
            SetNewTargetPosition();
        }
    }

    void SetNewTargetPosition()
    {
        if (_targetPos.x == _bottomLeft.x)
        {
            _targetPos = new Vector3(_topRight.x, Random.Range(_bottomLeft.y, _topRight.y), 0);
        }
        else if (_targetPos.x == _topRight.x)
        {
            _targetPos = new Vector3(_bottomLeft.x, Random.Range(_bottomLeft.y, _topRight.y), 0);
        }
        else if(_targetPos.y == _bottomLeft.y)
        {
            _targetPos = new Vector3(Random.Range(_bottomLeft.x, _topRight.x), _topRight.y, 0);
        }
        else
        {
            _targetPos = new Vector3(Random.Range(_bottomLeft.x, _topRight.x), _bottomLeft.y, 0);
        }
    }
}
