using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {
	
	public bool facingRight = true;
	
	void Update () 
	{
		if (Input.GetAxis("Horizontal") != 0) 
		{
			if (Input.GetAxisRaw("Horizontal") > 0) 
				GetComponent<tk2dSprite>().FlipX = false;
			else 
				GetComponent<tk2dSprite>().FlipX = true;
		}
	}
}
