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

    public bool isAlive = true;

    [SerializeField] private float rotation_const;
    [SerializeField] private float thrust_const;
    [SerializeField] private float sceneChangeDelayTime;

    [SerializeField] public bool debug_toggleloadnextsceneonlkey;
    [SerializeField] public bool debug_deathdisabled;

    // Start is called before the first frame update
    void Start()
    {
        rotation_const = 250f;
        thrust_const = 10000f;
        sceneChangeDelayTime = 1.5f;

        rigidBody = GetComponent<Rigidbody>();
        //rigidBody = GetComponent(typeof(Rigidbody)) as Rigidbody;
        //rigidBody = gameObject.GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        debug_toggleloadnextsceneonlkey = true;
        debug_deathdisabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        ManageThrust();
        ManageRotation();
        if (Debug.isDebugBuild)
        {
            ManageDebugKeys();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(isAlive)
        {
            switch (collision.gameObject.tag)
            {
                case "Friendly":
                    break;
                case "Landing":
                    StartFinishSequence();
                    break;
                default:
                    if (debug_deathdisabled == false) {
                        StartDeathSequence();
                    }
                    break;
            }
        }
    }

    private void StartFinishSequence()
    {
        isAlive = false;
        audioSource.Stop();
        audioSource.PlayOneShot(clip: finishChingle, volumeScale: 0.5f);
        finishParticleSystem.Play();
        Invoke("LoadNextScene", sceneChangeDelayTime);    // todo: parameterise time
    }

    private void StartDeathSequence()
    {
        isAlive = false;
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
        int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        if(currentSceneBuildIndex != SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(currentSceneBuildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    private void ManageDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (debug_toggleloadnextsceneonlkey)
            {
                LoadNextScene();
            }
        }else if (Input.GetKeyDown(KeyCode.C))
        {
            debug_deathdisabled = !debug_deathdisabled;
        }        
    }

    private void ApplyThrust()
    {
        rigidBody.AddRelativeForce(new Vector3(0, thrust_const, 0) * Time.deltaTime);
        thrustParticleSystem.Play();
        if (audioSource.isPlaying == false)
        {
            audioSource.PlayOneShot(mainEngine);
        }
    }

    private void StopApplyingThrust()
    {
        thrustParticleSystem.Stop();
        audioSource.Stop();
    }

    private void ManageThrust()
    {
        if(isAlive)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                ApplyThrust();
            }
            else
            {
                StopApplyingThrust();   
            }
        }
    }

    private void ManageRotation()
    {
        //rigidBody.freezeRotation = true;

        if(isAlive)
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
