using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWanderer : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 1;
    public float radius = 1;
    public float jitter = 1;
    public float distance = 1;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 target = Vector3.zero;
        // Wander
        target = Random.insideUnitSphere.normalized * radius;

        target.z = target.y;
        target += transform.position;

        // Jitter
        target = (Vector2)target + Random.insideUnitCircle * jitter;
        target = target.normalized * radius;

        // Distance
        target += transform.forward * distance;

        // Seek
        var dir = (target - transform.position).normalized;
        var desiredVelocity = dir * speed;
        var force = desiredVelocity - rb.velocity;
        rb.AddForce(force);

        transform.LookAt(transform.position + rb.velocity, Vector3.forward);
    }
}
