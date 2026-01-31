using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 50, 0f);

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(rotationSpeed * Time.fixedDeltaTime);
    }
}
