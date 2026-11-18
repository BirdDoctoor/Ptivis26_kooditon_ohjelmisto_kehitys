
using UnityEngine;

public class CameraLock : MonoBehaviour
{
    private float fixedHeight;

    void Start()
    {
        fixedHeight = transform.position.y;
    }

    void LateUpdate()
    {
        Vector3 position = transform.position;
        position.y = fixedHeight;
        transform.position = position;
    }
}
