using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;

    public float smoothSpeed= 0.124f;

    public Vector3 offset;

    void Update(){

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position,desiredPosition,smoothSpeed);
        transform.position = smoothedPosition;  
    }
}
