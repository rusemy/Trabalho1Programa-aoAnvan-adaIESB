using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private float inputVertical;
    private float inputHorizontal;

    private float maxVelocity = 10f;

    public float movementForce = 100f;
    public float rotationSpeed = 15f;

   private void Awake() {
       playerRigidbody = this.GetComponent<Rigidbody>();
       
   }

   private void Update() {
       inputVertical = Input.GetAxis("Vertical");
       inputHorizontal = Input.GetAxisRaw("Horizontal");
       
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

       transform.Rotate(transform.up * inputHorizontal * rotationSpeed);
   }


}
