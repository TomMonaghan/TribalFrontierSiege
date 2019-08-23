using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ThreadTwo : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Finish first project");
            Thread myThread = new Thread(new ThreadStart(DoSomeWork));
            myThread.Start();
            Debug.Log(("Finish second project"));

        }
    }

    private void DoSomeWork()
    {
        Thread.Sleep(3000);
        Debug.Log("Trimester over. Passed both projects");

    }
}