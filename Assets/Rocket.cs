using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.A))
        {
            print("Left and Thrusting");
        }
        else if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.D))
        {
            print("Right and Thrusting");
        }
        else if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            print("Left and Right");
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            print("Thrusting");
        }
        else if (Input.GetKey(KeyCode.A))
        {
            print("Left");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            print("Right");
        }        
    }
}
