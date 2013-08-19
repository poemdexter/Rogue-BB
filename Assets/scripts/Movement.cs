using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour 
{
	public float speed = 5.0f;
	public float jumpSpeed = 500.0f;
	public float airControl = 0.5f;
	public Vector3 jumpDirection = Vector3.zero;
	public Vector3 moveDirection = Vector3.zero;
	public bool isGrounded = false;
	public bool isJumping = false;
	public bool inAir = false;
	
	void Move()
	{
		if (Input.GetAxis("Horizontal") != 0)
		{
			moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), moveDirection.y, 0);
			this.transform.Translate(moveDirection.normalized * speed * Time.deltaTime);
		}
		
		if (Input.GetButtonDown("Jump") && isGrounded)
		{
			
		}
	}
	
	void FixedUpdate() { Move(); }
}
