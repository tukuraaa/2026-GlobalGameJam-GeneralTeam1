
using UnityEngine;
    
public class BaseMover : MonoBehaviour
{
    public float MoveSpeed = 0f;
    public Vector3 MoveDirection = Vector3.forward;

    void Update()
    {
        doMove();
    }

    virtual public void doMove()
    {
        if (MoveSpeed > 0)
            transform.Translate(MoveDirection.normalized * MoveSpeed * Time.deltaTime, Space.World);
    }

}