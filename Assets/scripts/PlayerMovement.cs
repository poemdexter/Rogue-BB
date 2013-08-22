using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	public float speed = 5.0f;
	public float jumpSpeed = 5.0f;
	public float gravity = -.1f;
	
	public float currentGravity = 0;
	public Vector2 moveDirection = Vector2.zero;
	public Vector2 jumpDirection = Vector2.zero;
	public bool isGrounded = false;
	public bool isJumping = false;
	public bool inAir = false;
	public ContactPoint contact;
	
	void FixedUpdate() 
	{
		// check if grounded by raycasting down half the size of the collider height
		Ray floorRay = new Ray(transform.position, Vector3.down);
		RaycastHit floorHit;
		moveDirection = Vector2.zero;
		if (Physics.Raycast(floorRay, out floorHit, this.collider.bounds.extents.y + .1f))
		{
			if (floorHit.collider.gameObject.CompareTag("Solid"))
			{
				Debug.DrawLine(floorRay.origin, floorHit.point, Color.magenta);
				transform.position = new Vector3(transform.position.x, floorHit.point.y + this.collider.bounds.extents.y, 0);
				isGrounded = true;
				isJumping = false;
				inAir = false;
				currentGravity = 0;
			}
		}
		// we're not grounded so we must be in air or falling
		else if (!inAir)
		{
			inAir = true;
			isGrounded = false;
			currentGravity = gravity;
		}
		
		Move(); 
	}
	
	void Move()
	{
		moveDirection = Vector3.zero;
		
		// moving left or right
		if (Input.GetAxis("Horizontal") != 0) 
		{
			moveDirection += new Vector2(Input.GetAxisRaw("Horizontal"), moveDirection.y) * speed;	
		}
		
		// jumping while grounded
		if (Input.GetButton("Jump") && isGrounded)
		{
			isJumping = true;
			isGrounded = false;
			jumpDirection = new Vector3(0, jumpSpeed);
		}
		
		if (isJumping || inAir)
		{
			jumpDirection += new Vector2(0, currentGravity);
			moveDirection += jumpDirection;
		}
	}
	
	void Update()
	{
		// move
		this.transform.Translate(moveDirection * Time.deltaTime);
	}
}
