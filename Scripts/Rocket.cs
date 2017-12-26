using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AroundWorld80Seconds.Scripts
{
    public class Rocket : MonoBehaviour
    {
        [SerializeField] float rcsThrust = 100f;
        [SerializeField] float ForwardThrust = 100f;
        [SerializeField] float reverseThrust = 50f;
        [SerializeField] float levelLoadDelay = 2f;
        [SerializeField] AudioClip mainEngine;
        [SerializeField] AudioClip reverseEngine;
        [SerializeField] AudioClip success;
        [SerializeField] AudioClip death;
        [SerializeField] AudioClip sideThruster;
        [SerializeField] ParticleSystem forwardEngineParticles;
        [SerializeField] ParticleSystem reverseEngineParticles1;
        [SerializeField] ParticleSystem reverseEngineParticles2;
        [SerializeField] ParticleSystem reverseEngineParticles3;
        [SerializeField] ParticleSystem leftEngineParticles;
        [SerializeField] ParticleSystem rightEngineParticles;
        [SerializeField] ParticleSystem successParticles;
        [SerializeField] ParticleSystem deathParticles;
        [SerializeField] ParticleSystem touchLandingPad;

        Rigidbody rigidBody;
        AudioSource audioSource;

        bool isTransitioning = false;
        bool collisionsDisabled = false;

        // Use this for initialization
        void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!isTransitioning)
            {
                RespondToForwardThrustInput();
                RespondToReverseThrustInput();
                RespondToRotateInput();
            }
            if (Debug.isDebugBuild)
            {
                RespondToDebugKeys();
            }
        }

        private void RespondToDebugKeys()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                LoadNextLevel();
            }
            else if(Input.GetKeyDown(KeyCode.C))
            {
                collisionsDisabled = !collisionsDisabled;
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            if (isTransitioning || collisionsDisabled) { return; }

            switch (collision.gameObject.tag)
            {
                case "Friendly":
                    print("Friendly");
                    // do nothing
                    break;
                case "Finish":
                    print("Finish");
                    StartSuccessSequence();
                    break;
                case "SpaceStationNorth":
                    print("Space Station North");
                    //StartSuccessSequence();   do nothing
                    break;
                default:
                    StartDeathSequence();
                    break;
            }
        }

        private void StartSuccessSequence()
        {
            isTransitioning = true;
            audioSource.Stop();
            audioSource.PlayOneShot(success);
            successParticles.Play();
            Invoke("LoadNextLevel", levelLoadDelay);
        }

        private void StartDeathSequence()
        {
            isTransitioning = true;
            audioSource.Stop();
            audioSource.PlayOneShot(death);
            deathParticles.Play();
            PlayerPrefs.SetInt("MinSave", 0);
            PlayerPrefs.SetInt("SecSave", 0);
            Invoke("LoadFristLevel", levelLoadDelay);

        }

        private void LoadNextLevel()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;
            if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 0;
            }
            SceneManager.LoadScene(nextSceneIndex); 
        }

        private void LoadFristLevel()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }

        private void RespondToForwardThrustInput()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                ApplyForwardThrust();
            }
            else
            {
                StopApplyingForwardThrust();
            }
        }

        private void RespondToReverseThrustInput()
        {
            if (Input.GetKey(KeyCode.M))
            {
                ApplyReverseThrust();
            }
            else
            {
                StopApplyingReverseThrust();
            }
        }

        private void StopApplyingForwardThrust()
        {
            audioSource.Stop();
            forwardEngineParticles.Stop();
        }

        private void StopApplyingReverseThrust()
        {
            reverseEngineParticles1.Stop();
            reverseEngineParticles2.Stop();
            reverseEngineParticles3.Stop();
        }

        private void ApplyForwardThrust()
        {
            rigidBody.AddRelativeForce(Vector3.up * ForwardThrust * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            forwardEngineParticles.Play();
        }

        private void ApplyReverseThrust()
        {
            rigidBody.AddRelativeForce(Vector3.down * reverseThrust * Time.deltaTime);
            reverseEngineParticles1.Play();
            reverseEngineParticles2.Play();
            reverseEngineParticles3.Play();
        }
        
        private void RespondToRotateInput()
        {
            rigidBody.angularVelocity = Vector3.zero;
            leftEngineParticles.Stop();
            rightEngineParticles.Stop();

            float rotationThisFrame = rcsThrust * Time.deltaTime;
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(Vector3.forward * rotationThisFrame);
                leftEngineParticles.Play();
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(-Vector3.forward * rotationThisFrame);
                rightEngineParticles.Play();
            }

        }
    }
}