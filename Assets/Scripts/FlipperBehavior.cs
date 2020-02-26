using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperBehavior : MonoBehaviour
{

    public Transform caseTransform;
    public int flipperAngleMin;
    public int flipperAngleMax;
    public int flipperModifier;

    public int flipperStrength;

    public bool isReversed;

    private int flipperAngleDifference;

    public enum FlipperStatus {
        still,
        movingDown,
        movingUp,
    }

    public FlipperStatus flipperStatus;

    public string activationKey;

    public Rigidbody ballRigidBody;

    private int reverse;

    // for future jordan
    // need to use add force
        // base the amount of force to add on height of ball at click
        // which essentially emulates 

    // Start is called before the first frame update
    void Start()
    {
        // flipper still to start
        flipperStatus = FlipperStatus.still;
        flipperAngleDifference = 0;

        // the left and right paddle rotate opposite ways
        // this multiplier takes this into account in an easy way
        if (isReversed) {
            reverse = -1;
        } else {
            reverse = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // update flipper status based on button press/release
        CheckButtonPresses();

        // move the flipper based on flipperStatus
        MoveFlipper();

        // disable flipper collision for a short period (to prevent double hits)

    }

    void CheckButtonPresses() {
        // check for left flipper press
        if (Input.GetKeyDown(activationKey)) {
            flipperStatus = FlipperStatus.movingUp;
        }

        // check for left flipper release
        if (Input.GetKeyUp(activationKey)) {
            flipperStatus = FlipperStatus.movingDown;
        }
    }

    void MoveFlipper() {
        
        // move up until max
        if (flipperStatus == FlipperStatus.movingUp && flipperAngleDifference < flipperAngleMax) {
            transform.Rotate(0, -flipperModifier * reverse, 0);
            flipperAngleDifference += flipperModifier; 
        
        // hit max, be still
        } else if (flipperStatus == FlipperStatus.movingUp) {
            flipperStatus = FlipperStatus.still;
        }

        // move down until min
        if (flipperStatus == FlipperStatus.movingDown && flipperAngleDifference > flipperAngleMin) {
            transform.Rotate(0, flipperModifier * reverse, 0); 
            flipperAngleDifference -= flipperModifier;
    
        // hit min be still
        } else if (flipperStatus == FlipperStatus.movingDown) {
            flipperStatus = FlipperStatus.still;
        }
    }

    void OnCollisionEnter(Collision other) {

        if (other.gameObject.name == "Ball") {

            // test
            // add force to ball based on flipper status
            if (flipperStatus == FlipperStatus.movingUp) {
                /*
                // I have no idea why this does not work. Will look into it later
                // Luckily, I don't have to calculate this vector because it 
                // exists in the flipper object

                // make sure adding the force is independent of case angle
                // get angle of repose(?)
                float caseAngle = caseTransform.eulerAngles.x;

                // convert angle to radians
                caseAngle *= Mathf.Deg2Rad;

                // apply force of at correct angle in y/z components
                // note: cosine for z, sine for y. this was determined by
                //       at the case from the side
                float zComponent = Mathf.Cos(caseAngle) * flipperStrength;
                float yComponent = Mathf.Sin(caseAngle) * flipperStrength;


                ballRigidBody.AddForce(0, yComponent, zComponent);
                */

                ballRigidBody.AddForce(transform.right * flipperStrength);
            }

        }
         
    }
}
