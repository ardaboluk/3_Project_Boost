using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{

    Rigidbody rigidBody;
    AudioSource audioSource;
    [SerializeField] private AudioClip mainEngine;
    [SerializeField] private AudioClip deathExplosion;
    [SerializeField] private AudioClip finishChingle;
    [SerializeField] private ParticleSystem thrustParticleSystem;
    [SerializeField] private ParticleSystem finishParticleSystem;
    [SerializeField] private ParticleSystem deathParticleSystem;

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
        sceneChangeDelayTime = 1.5f;

        rigidBody = GetComponent<Rigidbody>();
        //rigidBody = GetComponent(typeof(Rigidbody)) as Rigidbody;
        //rigidBody = gameObject.GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ManageThrust();
        ManageRotation();
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
                    StartFinishSequence();
                    break;
                default:
                    StartDeathSequence();
                    break;
            }
        }
    }

    private void StartFinishSequence()
    {
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(clip: finishChingle, volumeScale: 0.5f);
        finishParticleSystem.Play();
        Invoke("LoadNextScene", sceneChangeDelayTime);    // todo: parameterise time
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(clip: deathExplosion, volumeScale: 0.2f);
        deathParticleSystem.Play();
        Invoke("ResetToTheFirstScene", sceneChangeDelayTime);
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
                thrustParticleSystem.Play();
                if (audioSource.isPlaying == false)
                {
                    audioSource.PlayOneShot(mainEngine);
                }
            }
            else
            {
                thrustParticleSystem.Stop();
                audioSource.Stop();
            }
        }
    }

    private void ManageRotation()
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
