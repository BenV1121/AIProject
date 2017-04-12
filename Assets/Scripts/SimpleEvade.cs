using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SimpleEvade : MonoBehaviour
{

    Rigidbody rb;
    public GameObject target, target2;
    public float speed;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //var t = Vector3.Distance(target.transform.position, transform.position) /
        //        target.GetComponent<Rigidbody>().velocity.magnitude;

        var tpos = target.transform.position + target.GetComponent<Rigidbody>().velocity;

        var dir = -(target.transform.position - transform.position).normalized;

        var desiredVelocity = dir * speed;

        var force = desiredVelocity - rb.velocity;


        var tpos2 = target2.transform.position + target2.GetComponent<Rigidbody>().velocity;

        var dir2 = -(target2.transform.position - transform.position).normalized;

        var desiredVelocity2 = dir2 * speed;

        var force2 = desiredVelocity2 - rb.velocity;

        rb.AddForce((force + force2) / 2);

        Vector3 head = rb.velocity;
        head.y = 0;

        transform.LookAt(transform.position + head, Vector3.up);
    }
}
