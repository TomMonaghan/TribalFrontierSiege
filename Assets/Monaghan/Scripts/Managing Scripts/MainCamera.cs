using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public static Camera instance;
    public Renderer renderer; 

    public void Start()
    {
        instance = GetComponent<Camera>();
    }
    
    
}
