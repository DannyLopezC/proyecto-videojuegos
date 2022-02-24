using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform lookAt;
    public float xbound = 0.15f;
    public float ybound = 0.05f;

    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        //x bounds 
        float deltaX = lookAt.position.x - transform.position.x;
        if (deltaX > xbound || deltaX < -xbound)
            delta.x = deltaX + (transform.position.x < lookAt.position.x ? -xbound : xbound);

        //y bounds
        float deltaY = lookAt.position.y - transform.position.y;
        if (deltaY > ybound || deltaY < -ybound)
            delta.y = deltaY + (transform.position.y < lookAt.position.y ? -ybound : ybound);

        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}