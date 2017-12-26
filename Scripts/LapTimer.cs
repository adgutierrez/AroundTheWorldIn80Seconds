using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapTimer : MonoBehaviour
{
    public float lapTimer = 0;
    public Text textLapTimer;

	// Use this for initialization
	void Start ()
    {
        textLapTimer = GetComponent<Text>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
        lapTimer += Time.deltaTime;
        textLapTimer.text = lapTimer.ToString("f2");
	}
}
