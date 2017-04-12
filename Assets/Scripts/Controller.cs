using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private Motor motor;
	// Use this for initialization
	void Start ()
    {
        motor = GetComponent<Motor>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        motor.DoWalk(Input.GetAxis("Vertical"));
	}
}
