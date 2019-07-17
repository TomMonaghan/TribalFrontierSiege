using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = new Color(0.5f,1,1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
