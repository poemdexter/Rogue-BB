using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {
	
	public bool facingRight = true;
	
	void Update () 
	{
		float xAxis = Input.GetAxis("Horizontal");
		if (xAxis != 0) 
		{
			if (xAxis > 0) 
				GetComponent<tk2dSprite>().FlipX = false;
			else 
				GetComponent<tk2dSprite>().FlipX = true;
		}
	}
}
