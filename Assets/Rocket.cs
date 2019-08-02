using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{

    Rigidbody rigidBody;
    AudioSource audioSource;

    [SerializeField] private float rotation_const;
    [SerializeField] private float thrust_const;
    private bool audioRocketThrustPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        rotation_const = 250;
        thrust_const = 200;

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

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Landing":
                SceneManager.LoadScene(1);
                break;
            default:
                SceneManager.LoadScene(0);
                break;
        }
    }

    private void ManageThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(new Vector3(0, thrust_const, 0));
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
        //rigidBody.freezeRotation = true;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(rotation_const * Time.deltaTime * Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate((-1) * rotation_const * Time.deltaTime * Vector3.forward);
        }

        //rigidBody.freezeRotation = false;
    }
}
