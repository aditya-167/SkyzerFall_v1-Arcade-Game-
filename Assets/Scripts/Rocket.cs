using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    
    [SerializeField] float rcsThrust = 150f;
    [SerializeField] float Throttle = 20f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip win;
    [SerializeField] AudioClip die;

    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem winParticle;
    [SerializeField] ParticleSystem dieParticle;


    Rigidbody rigidBody;
    AudioSource audioSource;
    
    enum State { Alive, Dying, Transcending};

    State state = State.Alive;
    // Start is called before the first frame update
              
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(state == State.Alive)
        {
            RespondToThrust();
            RespondToRotate();
        }
        
    }

    private void RespondToRotate()
    {
        rigidBody.freezeRotation = true;

        float rotationSpeed = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.forward * rotationSpeed);

        }

        else if (Input.GetKey(KeyCode.D))
            transform.Rotate(-Vector3.forward * rotationSpeed);
        rigidBody.freezeRotation = false;
    }

    private void RespondToThrust()
    {

        float thrustSpeed = Throttle * Time.deltaTime;
        
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            audioSource.Stop();
            mainEngineParticle.Stop();
        }

    }

    private void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * Throttle);
        if (audioSource.isPlaying == false)
        {
            audioSource.PlayOneShot(mainEngine);
            
        }
        if(!mainEngineParticle.isPlaying)
            mainEngineParticle.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive) { return; }
        switch (collision.gameObject.tag)
        {
            case "Friendy":
                {
                    break;
                }
            case "Finish":
                {
                    WinSequence();
                    break;
                }
            default:
                {
                    DieSequence();
                    break;
                }
        }
    }

    private void DieSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(die);
        dieParticle.Play();
        Invoke("LoadFirstLevel", 1f);
    }

    private void WinSequence()
    {
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(win);
        winParticle.Play();
        Invoke("LoadNextScene", 1f);
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }
}
