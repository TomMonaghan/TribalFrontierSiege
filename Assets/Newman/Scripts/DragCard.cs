using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCard : MonoBehaviour
{
    /*private Vector3 mOffset;
    private float mZCoord;
    

    void OnMouseDown()
    {
        mZCoord = MainCamera.instance.WorldToScreenPoint(gameObject.transform.position).z;
        // Store offset = gameobject world pos - mouse world pos
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;

        return MainCamera.instance.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
       // if GameManager.Instance; 
        transform.position = GetMouseWorldPos() + mOffset;
    }*/
}
