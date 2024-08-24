using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float smooth;
    public Vector2 maxXY;
    public Vector2 minXY;

    public bool verticalOnly;
    public float cameraX;

    void LateUpdate()
    {
        if(transform.position != target.position){ 
            if(!verticalOnly){
                {
                    Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
                    //  Set xy coordinates for camera boundary.
                    targetPosition.x = Mathf.Clamp(targetPosition.x, minXY.x, maxXY.x); 
                    targetPosition.y = Mathf.Clamp(targetPosition.y, minXY.y, maxXY.y); 
                    transform.position = Vector3.Lerp(transform.position, targetPosition, smooth);
                }
            }
            if(verticalOnly){
                Vector3 targetPosition = new Vector3(cameraX, target.position.y, transform.position.z);
                targetPosition.x = Mathf.Clamp(targetPosition.x, minXY.x, maxXY.x);
                targetPosition.y = Mathf.Clamp(targetPosition.y, minXY.y, maxXY.y); 
                transform.position = Vector3.Lerp(transform.position, targetPosition, smooth);
            }
        }
    }
}
