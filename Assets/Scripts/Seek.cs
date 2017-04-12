using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : MonoBehaviour
{

    public Transform target;
    public GraphManager gm;
    private List<Vector3> pathToWalk;
    private Rigidbody rb;

    public float speed = 1;

    private Vector3 velocity;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();

        if (gm != null && target != null)
            pathToWalk = gm.FindPathBetween(transform, target);
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(pathToWalk != null && pathToWalk.Count > 0)
        {
            Vector3 dir = -(pathToWalk[0] - transform.position).normalized;

            Vector3 desiredVelocity = dir * speed;

            Vector3 seekingForce = desiredVelocity = velocity;

            rb.AddForce(seekingForce);
            velocity += seekingForce * Time.deltaTime;
            transform.position += velocity * Time.deltaTime;

            transform.forward = velocity.normalized;
            transform.LookAt(transform.position + velocity.normalized, Vector3.forward);

            if (Vector3.Distance(pathToWalk[0], transform.position) < .25f)
                pathToWalk.RemoveAt(0);
        }
	}

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
