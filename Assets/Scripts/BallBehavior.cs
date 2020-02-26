using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallBehavior : MonoBehaviour
{

    public Transform ballSpawn;
    public Rigidbody ballRigidBody;
    public float launchSpeed;
    public Transform caseTransform;

    public GateBehavior gateBehavior;
    private bool needSpawn;
    public GameObject launcher;

    public Text scoreDisplay;

    public int score;

    // Start is called before the first frame update
    void Start()
    {
        needSpawn = true;
        score = 0;
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
        Score(-10);
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

    void OnTriggerEnter(Collider other) {
        // check for target/bottom collisoin
        if (other.gameObject.CompareTag("Target")) {
            Score(10);
        }
    }

    void StopBall() {
        ballRigidBody.velocity = Vector3.zero;
        ballRigidBody.angularVelocity = Vector3.zero;
    }

    void Score(int delta) {
        // change score    
        score += delta;
    
        // don't let score be negative
        if (score < 0) {
            score = 0;
        }

        // update display
        RefreshDisplay();
    }

    void RefreshDisplay() {
        scoreDisplay.text = "Score: " + score.ToString();
    }
}
