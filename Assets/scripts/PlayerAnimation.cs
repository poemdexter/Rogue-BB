using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {
	
	public bool facingRight = true;
	
	public ParticleSystem bloods;
	
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
		
		if (Input.GetKeyDown(KeyCode.Z))
		{
			Vector3 position = transform.position + new Vector3(0,0,-0.1f);
			
			float xRot = (GetComponent<tk2dSprite>().FlipX) ? 186.5f : -6.5f;
			bloods.transform.rotation = Quaternion.Euler(xRot, 90, 0);
			
			ParticleSystem localBloodsObj = GameObject.Instantiate(bloods, position, bloods.transform.rotation) as ParticleSystem;
			localBloodsObj.Play();
		}
	}
}
