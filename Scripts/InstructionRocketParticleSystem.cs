using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AroundWorld80Seconds.Scripts
{

public class InstructionRocketParticleSystem : MonoBehaviour
    {

    [SerializeField] ParticleSystem instructionRocketParticlesRt;
    [SerializeField] ParticleSystem instructionRocketParticlesCen;
    [SerializeField] ParticleSystem instructionRocketParticlesLt;

    // Use this for initialization
    void Start ()
        {
            instructionRocketParticlesRt.Play();
            instructionRocketParticlesCen.Play();
            instructionRocketParticlesLt.Play();
        }
	
	// Update is called once per frame
	void Update ()
        {
		
	    }
    }
}
