using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour 
{
	public float speed = 5.0f;
	public float jumpSpeed = 500.0f;
	public Vector3 moveDirection = Vector3.zero;
	public bool isGrounded = false;
	public bool isJumping = false;
	public bool inAir = false;
	public ContactPoint contact;
	
	void FixedUpdate() 
	{
		if (!isGrounded)
		{
			// check if grounded by raycasting down half the size of the collider height
			if (Physics.Raycast(transform.position, -transform.up, this.collider.bounds.size.y/2))
			{
				isGrounded = true;
				isJumping = false;
				inAir = false;
			}
			else if (!inAir)
				inAir = true;
		}
		
		Move(); 
	}
	
	void Move()
	{
		// moving left or right
		if (Input.GetAxis("Horizontal") != 0)
			moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), moveDirection.y, 0);
		
		// jumping while grounded
		if (Input.GetButton("Jump") && isGrounded)
		{
			isJumping = true;
			rigidbody.AddForce((transform.up) * jumpSpeed);
		}
		
		this.transform.Translate(moveDirection.normalized * speed * Time.deltaTime);
	}
	
	void OnCollisionStay(Collision collisionInfo)
	{
		contact = collisionInfo.contacts[0];
		if (inAir || isJumping)
			rigidbody.AddForceAtPosition(-rigidbody.velocity, contact.point); 
	}
	
	void OnCollisionExit(Collision collisionInfo)
	{
		isGrounded = false; // left the collision of the ground
	}
}
