using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public float currentSpeed;
    private Vector3 lastPosition;
    private Rigidbody playerRigidbody;
    private float inputVertical;
    private float inputHorizontal;

    private float maxVelocity = 10f;

    public float movementForce;
    private float forceBack;

   private void Awake() {
       playerRigidbody = this.GetComponent<Rigidbody>();
       lastPosition = this.transform.position;
       forceBack = movementForce / 2f;
   }

   private void Update() {
       inputVertical = Input.GetAxis("Vertical");
       inputHorizontal = Input.GetAxis("Horizontal");
       if (Input.GetKeyDown(KeyCode.LeftShift))
       {
           maxVelocity /= 2f;
       }
       if (Input.GetKeyUp(KeyCode.LeftShift))
       {
           maxVelocity *= 2f;
       }
   }

   private void FixedUpdate() {
       if (inputVertical > 0f)
       {       
           if (playerRigidbody.velocity.magnitude < maxVelocity)
           {
               playerRigidbody.AddForce(movementForce * transform.forward);
           }
       }
       if (inputVertical < 0f)
       {
           if (playerRigidbody.velocity.magnitude < (maxVelocity/2f) )
           {
               playerRigidbody.AddForce(movementForce * -transform.forward);
           }
       }
   }


}
