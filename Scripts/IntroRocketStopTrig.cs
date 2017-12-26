using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace AroundWorld80Seconds.Scripts
{
    public class IntroRocketStopTrig : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter()
        {
            //int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(1);

            //touchPadParticles.Play();
            //audioSource.PlayOneShot(touchLandingPad);
        }
    }
}
