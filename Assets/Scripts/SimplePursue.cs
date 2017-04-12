using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SimplePursue : MonoBehaviour
{

    Rigidbody rb;
    public GameObject target;
    public float speed ;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var t = Vector3.Distance(target.transform.position, transform.position)/
                target.GetComponent<Rigidbody>().velocity.magnitude;

        var tpos = target.transform.position + t * target.GetComponent<Rigidbody>().velocity;

        var dir = (target.transform.position - transform.position).normalized;

        var desiredVelocity = dir * speed;

        var force = desiredVelocity - rb.velocity;

        rb.AddForce(force);

        Vector3 head = rb.velocity;
        head.y = 0;

        transform.LookAt(transform.position + head, Vector3.up);
    }
}
