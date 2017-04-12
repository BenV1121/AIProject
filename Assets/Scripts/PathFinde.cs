using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinde : MonoBehaviour
{
    public Transform target;
    public GraphManager gm;
    private List<Vector3> pathToWalk;
    private Rigidbody rb;

    public float speed = 1;
    public float radius = 1;

    Vector3 avoidance(Vector3 position, Vector3 direction, 
        float distance, float radius, float speed)
    {
        RaycastHit info;
        bool res = Physics.SphereCast(position, radius, direction, out info, distance);
        var desiredVelocity = info.normal * speed / Mathf.Max(info.distance, -1);
        return res ? desiredVelocity : Vector3.zero;
    }

    //private Vector3 velocity;

    Vector3 seek(Vector3 position, Vector3 target, float speed)
    {
        var dir = (target - position).normalized;
        //var dist = Vector3.Distance(target, position);
        var desiredVelocity = dir * speed;

        return desiredVelocity;
    }

    Vector3 arrival(Vector3 position, Vector3 target, float speed, float radius)
    {
        var desiredVelocity = seek(position, target, speed);
        var dist = Vector3.Distance(target, position);

        if (dist < radius)
            desiredVelocity *= dist / radius;

        //var force = desiredVelocity - velocity;
        return desiredVelocity;
    }

    void Update()
    {
        var desired = arrival(transform.position, pathToWalk[0], speed, radius)
                      + .25f * avoidance(transform.position, transform.forward, .2f, 1, speed);

        var force = desired - rb.velocity;
        rb.AddForce(force);

        Vector3 head = rb.velocity;
        head.y = 0;

        transform.LookAt(transform.position + head, Vector3.up);

        if (Vector3.Distance(transform.position, pathToWalk[0]) > .3f)
            pathToWalk.RemoveAt(0);

    }

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (gm != null && target != null)
            pathToWalk = gm.FindPathBetween(transform, target);
    }

    // Update is called once per frame

    private void OnValidate()
    {
        if (target != null && gm != null)
        {
            pathToWalk = gm.FindPathBetween(transform, target);
        }
        else pathToWalk = null;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere();

        if (pathToWalk != null && pathToWalk.Count > 0)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(transform.position, pathToWalk[0]);

            for (int i = 0; i < pathToWalk.Count; ++i)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawWireSphere(pathToWalk[i], .3f);

                if (i < pathToWalk.Count - 1)
                    Gizmos.DrawLine(pathToWalk[i], pathToWalk[i + 1]);
            }
        }
    }
}
