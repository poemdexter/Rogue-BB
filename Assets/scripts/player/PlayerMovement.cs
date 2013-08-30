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
	
	public Vector3 floorRayLeftOffset = Vector3.zero;
	public Vector3 floorRayRightOffset = Vector3.zero;
	public Vector3 wallRayTopOffset = Vector3.zero;
	public Vector3 wallRayBottomOffset = Vector3.zero;
	
	private enum RaycastDirection
	{
		Up,
		Down,
		Left,
		Right
	}
	
	void FixedUpdate() 
	{
		DebugRays();
		
		// check collision head or feet
		if (checkIfGrounded()) 
		{
			isGrounded = true;
			isJumping = false;
			inAir = false;
			currentGravity = 0;
			jumpDirection = Vector2.zero;
		} 
		else if (!inAir) // we're not grounded so we must be in air or falling
		{
			inAir = true;
			isGrounded = false;
			currentGravity = gravity;
		}
		
		CalculateMoveDirection();
		
		// move
		this.transform.Translate(moveDirection * Time.deltaTime);
	}
	
	bool checkIfGrounded()
	{
		return RaycastCollideVertical(RaycastDirection.Down);
	}
	
	void CalculateMoveDirection()
	{
		moveDirection = Vector2.zero;
		
		// moving left or right
		float horizInput = Input.GetAxis("Horizontal");
		if (horizInput != 0) 
		{
			if (horizInput > 0 && !RaycastCollideHorizontal(RaycastDirection.Right))
				moveDirection += new Vector2(Input.GetAxisRaw("Horizontal"), moveDirection.y) * speed;
			
			if (horizInput < 0 && !RaycastCollideHorizontal(RaycastDirection.Left))
				moveDirection += new Vector2(Input.GetAxisRaw("Horizontal"), moveDirection.y) * speed;
		}
		
		// jumping while grounded
		if (Input.GetButton("Jump") && isGrounded && !RaycastCollideVertical(RaycastDirection.Up))
		{
			isJumping = true;
			isGrounded = false;
			jumpDirection = new Vector2(0, jumpSpeed);
		}
		
		// we're moving vertically
		if ((isJumping || inAir) && !RaycastCollideVertical(RaycastDirection.Up))
		{
			jumpDirection += new Vector2(0, currentGravity);
			moveDirection += jumpDirection;
		}
	}

	bool RaycastCollideVertical(RaycastDirection direction)
	{
		Ray floorRay_L, floorRay_R;
		if (direction == RaycastDirection.Down)
		{
			floorRay_L = new Ray(transform.position + floorRayLeftOffset, Vector3.down);
			floorRay_R = new Ray(transform.position + floorRayRightOffset, Vector3.down);
		}
		else if (direction == RaycastDirection.Up)
		{
			floorRay_L = new Ray(transform.position + floorRayLeftOffset, Vector3.up);
			floorRay_R = new Ray(transform.position + floorRayRightOffset, Vector3.up);
		}
		else
		{
			Debug.Log("RaycastCollideVertical() BAD DIRECTION");
			return true;
		}
		
		RaycastHit floorHit;
		if (Physics.Raycast(floorRay_L, out floorHit, this.collider.bounds.extents.y + .1f))
		{
			if (floorHit.collider.gameObject.CompareTag("Solid"))
			{
				if (direction == RaycastDirection.Down)
					transform.position = new Vector3(transform.position.x, floorHit.point.y + this.collider.bounds.extents.y, 0);
				else if (direction == RaycastDirection.Up)
					transform.position = new Vector3(transform.position.x, floorHit.point.y - this.collider.bounds.extents.y, 0);
					
				return true;
			}
		}
		else if (Physics.Raycast(floorRay_R, out floorHit, this.collider.bounds.extents.y + .1f))
		{
			if (floorHit.collider.gameObject.CompareTag("Solid"))
			{
				if (direction == RaycastDirection.Down)
					transform.position = new Vector3(transform.position.x, floorHit.point.y + this.collider.bounds.extents.y, 0);
				else if (direction == RaycastDirection.Up)
					transform.position = new Vector3(transform.position.x, floorHit.point.y - this.collider.bounds.extents.y, 0);
					
				return true;
			}
		}
		return false;
	}
	
	bool RaycastCollideHorizontal(RaycastDirection direction)
	{
		Ray wallRay_T, wallRay_B;
		
		if (direction == RaycastDirection.Right)
		{
			wallRay_T = new Ray(transform.position + wallRayTopOffset, Vector3.right);
			wallRay_B = new Ray(transform.position + wallRayBottomOffset, Vector3.right);
		}
		else if (direction == RaycastDirection.Left)
		{
			wallRay_T = new Ray(transform.position + wallRayTopOffset, Vector3.left);
			wallRay_B = new Ray(transform.position + wallRayBottomOffset, Vector3.left);
		}
		else
		{
			Debug.Log("RaycastCollideHorizontal() BAD DIRECTION");
			return true;
		}
		
		RaycastHit wallHit;
		if (Physics.Raycast(wallRay_T, out wallHit, collider.bounds.extents.x + .1f))
		{
			if (wallHit.collider.gameObject.CompareTag("Solid"))
			{
				if (direction == RaycastDirection.Right)
					transform.position = new Vector3(wallHit.point.x - collider.bounds.extents.x, transform.position.y, 0);
				else if (direction == RaycastDirection.Left)
					transform.position = new Vector3(wallHit.point.x + collider.bounds.extents.x, transform.position.y, 0);
				
				return true;
			}
		}
		else if (Physics.Raycast(wallRay_B, out wallHit, collider.bounds.extents.x + .1f))
		{
			if (wallHit.collider.gameObject.CompareTag("Solid"))
			{
				if (direction == RaycastDirection.Right)
					transform.position = new Vector3(wallHit.point.x - collider.bounds.extents.x, transform.position.y, 0);
				else if (direction == RaycastDirection.Left)
					transform.position = new Vector3(wallHit.point.x + collider.bounds.extents.x, transform.position.y, 0);
				
				return true;
			}
		}
		return false;
	}
	
	void DebugRays()
	{
		Ray lWallRay_T = new Ray(transform.position + wallRayTopOffset, Vector3.left);
		Ray lWallRay_B = new Ray(transform.position + wallRayBottomOffset, Vector3.left);
		Debug.DrawLine(lWallRay_T.origin, lWallRay_T.origin - new Vector3(this.collider.bounds.extents.x + .1f,0,0), Color.magenta);
		Debug.DrawLine(lWallRay_B.origin, lWallRay_B.origin - new Vector3(this.collider.bounds.extents.x + .1f,0,0), Color.magenta);
		
		Ray rWallRay_T = new Ray(transform.position + wallRayTopOffset, Vector3.right);
		Ray rWallRay_B = new Ray(transform.position + wallRayBottomOffset, Vector3.right);
		Debug.DrawLine(rWallRay_B.origin, rWallRay_B.origin + new Vector3(this.collider.bounds.extents.x + .1f,0,0), Color.magenta);
		Debug.DrawLine(rWallRay_T.origin, rWallRay_T.origin + new Vector3(this.collider.bounds.extents.x + .1f,0,0), Color.magenta);
		
		Ray dfloorRay_L = new Ray(transform.position + floorRayLeftOffset, Vector3.down);
		Ray dfloorRay_R = new Ray(transform.position + floorRayRightOffset, Vector3.down);
		Debug.DrawLine(dfloorRay_L.origin, dfloorRay_L.origin - new Vector3(0, this.collider.bounds.extents.y + .1f,0), Color.magenta);
		Debug.DrawLine(dfloorRay_R.origin, dfloorRay_R.origin - new Vector3(0, this.collider.bounds.extents.y + .1f,0), Color.magenta);
		
		Ray ufloorRay_L = new Ray(transform.position + floorRayLeftOffset, Vector3.down);
		Ray ufloorRay_R = new Ray(transform.position + floorRayRightOffset, Vector3.down);
		Debug.DrawLine(ufloorRay_L.origin, ufloorRay_L.origin + new Vector3(0, this.collider.bounds.extents.y + .1f,0), Color.magenta);
		Debug.DrawLine(ufloorRay_R.origin, ufloorRay_R.origin + new Vector3(0, this.collider.bounds.extents.y + .1f,0), Color.magenta);
	}
}