using UnityEngine;
using System.Collections;

public class character_Movement : MonoBehaviour {

    public CharacterController myController;
    public float groundSpeed = 6f;
    public float AerialSpeed = 3f;
    public float GravityStrenght = 9.8f;
    public float jumpSpeed = 10f;
    public Transform CameraTransform;
    private bool canJump = false;
    float verticalVelocity;
    Vector3 velocity;
    Vector3 groundedVelocity;
    Vector3 normal;
    bool onWall = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {

        //gameObject.transform.rotation = camera.transform.rotation;
        GravityStrenght = 9.8f;
        if (onWall && verticalVelocity < 0f)
            GravityStrenght = 4.9f;
        
        Vector3 myVector = Vector3.zero;
        Vector3 input = Vector3.zero;
        input.x = Input.GetAxis("Horizontal");
        input.z = Input.GetAxis("Vertical");
        input = Vector3.ClampMagnitude(input, 1f);
        Quaternion inputRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(CameraTransform.forward, Vector3.up), Vector3.up);
        if (myController.isGrounded)
        {
            myVector = input;
            
            myVector = inputRotation * myVector;
            myVector *= groundSpeed;
        }
        else
        {
            myVector = groundedVelocity;
            myVector += inputRotation * input * AerialSpeed;
        }
        myVector = Vector3.ClampMagnitude(myVector, groundSpeed);
        myVector = myVector * Time.deltaTime;

        verticalVelocity = verticalVelocity - GravityStrenght * Time.deltaTime;
        

        if (Input.GetButtonDown("Jump"))
        {
            if (onWall)
            {
                Vector3 reflection = Vector3.Reflect(velocity, normal);
                Vector3 projected = Vector3.ProjectOnPlane(reflection, Vector3.up);
                groundedVelocity = projected.normalized * groundSpeed + normal * AerialSpeed;
            }    
            if (canJump)
                verticalVelocity += jumpSpeed;
        }
        myVector.y = verticalVelocity * Time.deltaTime;

        CollisionFlags flags = myController.Move(myVector);
        // player speed
        velocity = myVector / Time.deltaTime;

        if ((flags & CollisionFlags.Below) != 0)
        {
            groundedVelocity = Vector3.ProjectOnPlane(velocity, Vector3.up);
            canJump = true;
            onWall = false;
            verticalVelocity = -3f;
        }
        else if ((flags & CollisionFlags.Sides) != 0f)
        {
            canJump = true;
            onWall = true;
            
        }
        else if ((flags & CollisionFlags.Above) != 0f)
        {
            verticalVelocity = -2f;
            canJump = false;
            onWall = false;
        }
        else
        {
            canJump = false;
            onWall = false;
        }
        if (verticalVelocity > 8f)
            verticalVelocity = 8f;
        //print(verticalVelocity);
	}
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
       normal = hit.normal;   
    }
}
