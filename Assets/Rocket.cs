using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    Rigidbody rigidBody;
    AudioSource audioSource;

    private float rotation_const;
    private bool audioRocketThrustPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        rotation_const = 20;

        rigidBody = GetComponent<Rigidbody>();
        //rigidBody = GetComponent(typeof(Rigidbody)) as Rigidbody;
        //rigidBody = gameObject.GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ManageThrust();
        ManageRotate();
    }

    private void ManageThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(new Vector3(0, 1, 0));
            if(audioSource.isPlaying == false)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void ManageRotate()
    {
        rigidBody.freezeRotation = true;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(rotation_const * Time.deltaTime * Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate((-1) * rotation_const * Time.deltaTime * Vector3.forward);
        }

        rigidBody.freezeRotation = false;
    }
}
