// original by Eric Haines (Eric5h5)
// adapted by @torahhorse
// http://wiki.unity3d.com/index.php/FPSWalkerEnhanced

using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]
public class FirstPersonDrifter: MonoBehaviour
{
    public float walkSpeed = 6.0f;
    public float runSpeed = 10.0f;
    public float climbSpeed;

    public float climbDetectRadius;

    public MouseLook mouseLook;
 
    // If true, diagonal speed (when strafing + moving forward or back) can't exceed normal move speed; otherwise it's about 1.4 times faster
    private bool limitDiagonalSpeed = true;
 
    public bool enableRunning = false;
 
    public float jumpSpeed = 4.0f;
    public float gravity = 10.0f;
 
    // Units that player can fall before a falling damage function is run. To disable, type "infinity" in the inspector
    private float fallingDamageThreshold = 10.0f;
 
    // If the player ends up on a slope which is at least the Slope Limit as set on the character controller, then he will slide down
    public bool slideWhenOverSlopeLimit = false;
 
    // If checked and the player is on an object tagged "Slide", he will slide down it regardless of the slope limit
    public bool slideOnTaggedObjects = false;
 
    public float slideSpeed = 5.0f;
 
    // If checked, then the player can change direction while in the air
    public bool airControl = true;
 
    // Small amounts of this results in bumping when walking down slopes, but large amounts results in falling too fast
    public float antiBumpFactor = .75f;
 
    // Player must be grounded for at least this many physics frames before being able to jump again; set to 0 to allow bunny hopping
    public int antiBunnyHopFactor = 1;
 
    private Vector3 moveDirection = Vector3.zero;
    private bool grounded = false;
    public bool climbing = false;
    public bool isInTree = false;
    private CharacterController controller;
    private Transform myTransform;
    public float speed;
    private RaycastHit hit;
    private float fallStartLevel;
    private bool falling;
    private float slideLimit;
    private float rayDistance;
    private Vector3 contactPoint;
    private bool playerControl = false;
    public bool isLookUp = false;
    private int jumpTimer;
    private bool speedUp = false;

    public GameObject speedlineparticle;

    public Transform fakePlayer;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        myTransform = transform;
        speed = walkSpeed;
        rayDistance = controller.height * .5f + controller.radius;
        slideLimit = controller.slopeLimit - .1f;
        jumpTimer = antiBunnyHopFactor;
        mouseLook = GetComponent<MouseLook>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            if (isInTree)
            {
                isInTree = false;
                gravity = 10;
            }
        }
    }

    void FixedUpdate() {

        if (isLookUp && !ScenesManager.Instance.isPause)
        {
            float inputX = Input.GetAxis("Horizontal");
            float inputY = Input.GetAxis("Vertical");
            // If both horizontal and vertical are used simultaneously, limit speed (if allowed), so the total doesn't exceed normal move speed
            float inputModifyFactor = (inputX != 0.0f && inputY != 0.0f && limitDiagonalSpeed) ? .7071f : 1.0f;


            Vector3 tempDir = new Vector3(inputX * inputModifyFactor, -antiBumpFactor, inputY * inputModifyFactor);
            //  Debug.Log("inputx"+ inputX);
            //  Debug.Log("inputy"+ inputY);
            //   Debug.Log("movedirection" + moveDirection);

            tempDir.y = 0;  
            tempDir = fakePlayer.TransformDirection(moveDirection) * speed;

            if (speedUp && (inputX != 0.0f || inputY != 0.0f))
            {
                speedlineparticle.SetActive(true);
            }
            else
            {
                speedlineparticle.SetActive(false);
            }

            Debug.DrawLine(myTransform.position,
             myTransform.position + transform.forward * 5, Color.cyan);

            //Climb Tree detection
            if (Physics.Raycast(myTransform.position, transform.forward, out hit, rayDistance * climbDetectRadius))
            {
                if (hit.collider.tag == "Tree")
                {
                    Debug.Log("test tree");
                    if (Input.GetKey(KeyCode.Q))
                    {
                        climbing = true;

                        if (!isInTree)
                        {
                            gravity = 0;
                            controller.Move(new Vector3(0, climbSpeed, 0) * Time.deltaTime);
                        }
                        else
                        {

                        }
                        //  mouseLook.SetSensitivity(0.5f);
                        controller.Move(new Vector3(0, climbSpeed, 0) * Time.deltaTime);
                        //  moveDirection.y = climbSpeed;
                        // moveDirection.x = 0;
                    }
                    else
                    {
                        climbing = false;
                    }
                }

            }
            else
            {
                climbing = false;
            }

            if (grounded && !climbing)
            {
                bool sliding = false;
                //  mouseLook.SetSensitivity(5f);

                // See if surface immediately below should be slid down. We use this normally rather than a ControllerColliderHit point,
                // because that interferes with step climbing amongst other annoyances
                if (Physics.Raycast(myTransform.position, -Vector3.up, out hit, rayDistance))
                {
                    if (Vector3.Angle(hit.normal, Vector3.up) > slideLimit)
                        sliding = true;
                }
                // However, just raycasting straight down from the center can fail when on steep slopes
                // So if the above raycast didn't catch anything, raycast down from the stored ControllerColliderHit point instead
                else
                {
                    Physics.Raycast(contactPoint + Vector3.up, -Vector3.up, out hit);
                    if (Vector3.Angle(hit.normal, Vector3.up) > slideLimit)
                        sliding = true;
                }

                // If we were falling, and we fell a vertical distance greater than the threshold, run a falling damage routine
                if (falling)
                {
                    falling = false;
                    if (myTransform.position.y < fallStartLevel - fallingDamageThreshold)
                        FallingDamageAlert(fallStartLevel - myTransform.position.y);
                }

                if (enableRunning)
                {
                    speed = Input.GetButton("Run") ? runSpeed : walkSpeed;
                }

                // If sliding (and it's allowed), or if we're on an object tagged "Slide", get a vector pointing down the slope we're on
                if ((sliding && slideWhenOverSlopeLimit) || (slideOnTaggedObjects && hit.collider.tag == "Slide"))
                {
                    Vector3 hitNormal = hit.normal;
                    moveDirection = new Vector3(hitNormal.x, -hitNormal.y, hitNormal.z);
                    Vector3.OrthoNormalize(ref hitNormal, ref moveDirection);
                    moveDirection *= slideSpeed;
                    playerControl = false;
                }
                // Otherwise recalculate moveDirection directly from axes, adding a bit of -y to avoid bumping down inclines
                else
                {
                    moveDirection = new Vector3(inputX * inputModifyFactor, -antiBumpFactor, inputY * inputModifyFactor);
                    //  Debug.Log("inputx"+ inputX);
                    //  Debug.Log("inputy"+ inputY);
                    //   Debug.Log("movedirection" + moveDirection);

                    moveDirection.y = 0;
                    moveDirection = fakePlayer.TransformDirection(moveDirection) * speed;

                    //   moveDirection = moveDirection.normalized * speed;

                    //    Debug.Log("newmovedirection" + moveDirection);

                    //   moveDirection.x = 0;
                    //   moveDirection.z = 0;
                    playerControl = true;
                }

                // Jump! But only if the jump button has been released and player has been grounded for a given number of frames
                if (!Input.GetButton("Jump"))
                    jumpTimer++;
                else if ( jumpTimer >= antiBunnyHopFactor)
                {
                    //  controller.Move(new Vector3(0, jumpSpeed, 0));
                    moveDirection.y = jumpSpeed;
                    jumpTimer = 0;
                }
            }
            else
            {
                // If we stepped over a cliff or something, set the height at which we started falling
                if (!falling)
                {
                    falling = true;
                    fallStartLevel = myTransform.position.y;
                }

                // If air control is allowed, check movement but don't touch the y component
                if (airControl && playerControl)
                {
                    moveDirection.x = inputX * speed * inputModifyFactor;
                    moveDirection.z = inputY * speed * inputModifyFactor;
                    moveDirection = fakePlayer.TransformDirection(moveDirection);

                }
            }

            // Apply gravity
            moveDirection.y -= gravity * Time.deltaTime;

            // Move the controller, and set grounded true or false depending on whether we're standing on something
            grounded = (controller.Move(moveDirection * Time.deltaTime) & CollisionFlags.Below) != 0;
        }
        
    }
 
    // Store point that we're in contact with for use in FixedUpdate if needed
    void OnControllerColliderHit (ControllerColliderHit hit) {
        contactPoint = hit.point;
    }
 
    // If falling damage occured, this is the place to do something about it. You can make the player
    // have hitpoints and remove some of them based on the distance fallen, add sound effects, etc.
    void FallingDamageAlert (float fallDistance)
    {
        //print ("Ouch! Fell " + fallDistance + " units!");   
    }

    public IEnumerator SpeedUpRoutine()
    {
        speed *= 2f;
        speedUp = true;
        yield return new WaitForSeconds(15f);
        speed /= 2f;
        speedUp = false;
    }

/*    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "AppleTree")
        {
            isInTree = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "AppleTree")
        {
            isInTree = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "AppleTree")
        {
            isInTree = true;
        }
    }*/
}