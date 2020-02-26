using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{

    public Transform ballSpawn;
    public Rigidbody ballRigidBody;
    public float launchSpeed;
    public Transform caseTransform;

    // Start is called before the first frame update
    void Start()
    {
        spawn();
        tempLaunch();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawn() {
        // update the position of the ball to the spawn position
        transform.position = ballSpawn.position;
    }

    void tempLaunch() {
        // add a bunch of force to the ball initially to emulate launch
        ballRigidBody.AddForce(ballRigidBody.gameObject.transform.forward * launchSpeed);
    }
}
