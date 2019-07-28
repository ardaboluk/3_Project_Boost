using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    Rigidbody rigidBody;

    private float rotation_const;

    // Start is called before the first frame update
    void Start()
    {
        rotation_const = 5;

        rigidBody = GetComponent<Rigidbody>();
        //rigidBody = GetComponent(typeof(Rigidbody)) as Rigidbody;
        //rigidBody = gameObject.GetComponent<Rigidbody>();
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
            rigidBody.AddRelativeForce(new Vector3(0, 1, 0));
            transform.Rotate(rotation_const * Time.deltaTime * Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.D))
        {
            rigidBody.AddRelativeForce(new Vector3(0, 1, 0));
            transform.Rotate((-1) * rotation_const * Time.deltaTime * Vector3.forward);
        }
        else if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            ;
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(new Vector3(0, 1, 0));
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(new Vector3(0, 1, 0));
            //rigidBody.AddRelativeForce(Vector3.up);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(5 * Time.deltaTime * Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate((-1) * 5 * Time.deltaTime * Vector3.forward);
        }        
    }
}
