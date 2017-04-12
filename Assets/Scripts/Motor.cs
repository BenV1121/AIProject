using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour
{
    private Rigidbody rb;

    public float walkSpeed;
    public float WalkMaxSpeed;
    public float walkResRate;

    private float walkInput;

    public void DoWalk(float input01) { walkInput += input01; }

	// Use this for initialization
	void Start () { rb = GetComponent<Rigidbody>(); }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        var desiredVelocity = Vector3.forward * WalkMaxSpeed;

        var steer = desiredVelocity - rb.velocity;

        //Vector3.Project(rb.velocity, Vector3.forward);

        rb.AddForce(steer.normalized * walkSpeed * walkInput);

        walkInput = 0;
	}
}
