using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{

    public Transform ballSpawn;
    public Rigidbody ballRigidBody;
    public float launchSpeed;
    public Transform caseTransform;

    public GateBehavior gateBehavior;
    private bool needSpawn;
    public GameObject launcher;

    // Start is called before the first frame update
    void Start()
    {
        needSpawn = true;
        // gateBehavior = gameObject.GetComponent<GateBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (needSpawn) {
            Respawn();
        }
    }

    void Spawn() {
        // update the position of the ball to the spawn position
        transform.position = ballSpawn.position;
    }

    void TempLaunch() {
        // add a bunch of force to the ball initially to emulate launch
        ballRigidBody.AddForce(launcher.transform.up * launchSpeed);
    }

    void Respawn() {
        StopBall();
        Spawn();
        TempLaunch();
        needSpawn = false;
    }

    void OnCollisionEnter(Collision other) {

        // check for bottom wall collision
        if (other.gameObject.name == "Wall Bottom") {
            // respawn ball
            needSpawn = true;

            // set gate inactive
            gateBehavior.SetInactive();
        }
    }

    void StopBall() {
        ballRigidBody.velocity = Vector3.zero;
        ballRigidBody.angularVelocity = Vector3.zero;
    }
}
