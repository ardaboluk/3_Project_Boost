using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [Range(0,10)]
    [SerializeField]
    float movementPeriod;

    Vector3 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = gameObject.transform.position;
        movementVector = new Vector3(5,0,0);
        movementPeriod = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(movementPeriod > Mathf.Epsilon)
        {
            // float cycles = Time.time / movementPeriod;
            // float radians = cycles * 2 * Mathf.PI;
            // float movementFactor = Mathf.Sin(radians);
            // Vector3 offset = movementVector * movementFactor;
            Vector3 offset = movementVector * Mathf.Sin((Time.time / movementPeriod) * 2 * Mathf.PI);
            gameObject.transform.position = startingPosition + offset;
        }        
    }
}
