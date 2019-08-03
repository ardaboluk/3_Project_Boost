using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{

    Rigidbody rigidBody;
    AudioSource audioSource;
    [SerializeField] private AudioClip mainEngine;
    [SerializeField] private AudioClip deathExplosion;
    [SerializeField] private AudioClip finishChingle;

    private enum State {Alive = 0, Dying = 1, Transcending = 2 };
    [SerializeField] private State state;

    [SerializeField] private float rotation_const;
    [SerializeField] private float thrust_const;
    [SerializeField] private float sceneChangeDelayTime;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Alive;

        rotation_const = 250;
        thrust_const = 200;
        sceneChangeDelayTime = 1.0f;

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
        if(state == State.Alive)
        {
            switch (collision.gameObject.tag)
            {
                case "Friendly":
                    break;
                case "Landing":
                    state = State.Transcending;
                    audioSource.Stop();
                    audioSource.PlayOneShot(clip: finishChingle, volumeScale: 0.5f);
                    Invoke("LoadNextScene", sceneChangeDelayTime);    // todo: parameterise time
                    break;
                default:
                    state = State.Dying;
                    audioSource.Stop();
                    audioSource.PlayOneShot(clip: deathExplosion, volumeScale: 0.2f);
                    Invoke("ResetToTheFirstScene", sceneChangeDelayTime);
                    break;
            }
        }
    }

    private void ResetToTheFirstScene()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }

    private void ManageThrust()
    {
        if(state == State.Alive)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                rigidBody.AddRelativeForce(new Vector3(0, thrust_const, 0));
                if (audioSource.isPlaying == false)
                {
                    audioSource.PlayOneShot(mainEngine);
                }
            }
            else
            {
                audioSource.Stop();
            }
        }
    }

    private void ManageRotate()
    {
        //rigidBody.freezeRotation = true;

        if(state == State.Alive)
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(rotation_const * Time.deltaTime * Vector3.forward);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate((-1) * rotation_const * Time.deltaTime * Vector3.forward);
            }
        }

        //rigidBody.freezeRotation = false;
    }
}
