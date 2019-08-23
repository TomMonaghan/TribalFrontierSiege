using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ThreadOne : MonoBehaviour
{
    
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Finish first project");
            DoSomeWork();
            Debug.Log(("Finish Second Project"));
        }
    }

    private void DoSomeWork()
    {
        Thread.Sleep(3000);
        Debug.Log("Trimester over. Only finished one project");
    }
}
