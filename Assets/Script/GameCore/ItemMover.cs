using UnityEngine;

public class ItemMover : BaseMover
{
    Vector3 _bottomLeft;
    Vector3 _topRight;

    public override void Initialize(float arrivalTime, Vector3 _)
    {
        base.Initialize(arrivalTime, _);
        var cam = Camera.main;
        _bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Debug.LogWarning(_bottomLeft);
        _topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
        Debug.LogWarning(_topRight);

        SetNewTargetPosition();
    }

    public override void doMove()
    {
        _currentTime += Time.deltaTime;

        float t = _currentTime / _arrivalTime; // 0~1
        transform.position = Vector3.Lerp(transform.position, _targetPos, t);

        if (transform.position == _targetPos)
        {
            SetNewTargetPosition();
        }
    }

    void SetNewTargetPosition()
    {
        _targetPos = new Vector3(Random.Range(_bottomLeft.x, _topRight.x),
                                 Random.Range(_bottomLeft.y, _topRight.y),
                                 0);
        Debug.Log(_targetPos);
    }
}
