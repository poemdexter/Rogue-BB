using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {

    private tk2dSpriteAnimator anim;

	public bool isLookingLeft = false;
    public bool isThrowing = false;

	public ParticleSystem bloods;
	public ParticleSystem piss;
	public ParticleSystem barfs;
	public ParticleSystem shits;

    void Start()
    {
        anim = GetComponent<tk2dSpriteAnimator>();
    }

    void ThrowCompleteDelegate(tk2dSpriteAnimator sprite, tk2dSpriteAnimationClip clip)
    {
        isThrowing = false;
    }

    // need to set ownership
    void HandleMovementAnimations(PlayerMovement.Movement m)
    {
//        if (isJumping || inAir) animator.Play("jump");
//        else if (isHorizontalMoving) animator.Play("walk");
//        else animator.Play("still");

        if (!isThrowing)
        {
            anim.Play("throw");
            isThrowing = true;
            anim.AnimationCompleted = ThrowCompleteDelegate;
        }
    }

	void Update () 
	{
		if (networkView.isMine)
		{
			float xAxis = Input.GetAxis("Horizontal");
			if (xAxis != 0) // we're moving
			{
                // face us the correct way
				if (xAxis > 0 && isLookingLeft == true)
				{
					GetComponent<tk2dSprite>().FlipX = false;
					networkView.RPC("MakeSpriteFaceLeft", RPCMode.Others, false);
                    isLookingLeft = false;
				}
				else if (xAxis < 0 && isLookingLeft == false)
				{
					GetComponent<tk2dSprite>().FlipX = true;
					networkView.RPC("MakeSpriteFaceLeft", RPCMode.Others, true);
                    isLookingLeft = true;
				}
			}
			
//			if (Input.GetKeyDown(KeyCode.Z))
//			{
//				FireBloodParticles();
//				networkView.RPC("FireBloodParticles", RPCMode.Others);
//			}
//			if (Input.GetKeyDown(KeyCode.X))
//			{
//				FireBarfParticles();
//				networkView.RPC("FireBarfParticles", RPCMode.Others);
//			}
//			if (Input.GetKeyDown(KeyCode.C))
//			{
//				FirePissParticles();
//				networkView.RPC("FirePissParticles", RPCMode.Others);
//			}
//			if (Input.GetKeyDown(KeyCode.V))
//			{
//				FireShitParticles();
//				networkView.RPC("FireShitParticles", RPCMode.Others);
//			}
		}
	}
	
	[RPC]
	void MakeSpriteFaceLeft(bool left)
	{
		GetComponent<tk2dSprite>().FlipX = left;
	}
	
	[RPC]
	void FireBloodParticles()
	{
		Vector3 position = transform.position + new Vector3(0,0,-0.1f);

		float xRot = (GetComponent<tk2dSprite>().FlipX) ? 186.5f : -6.5f;
		bloods.transform.rotation = Quaternion.Euler(xRot, 90, 0);

		ParticleSystem localBloodsObj = GameObject.Instantiate(bloods, position, bloods.transform.rotation) as ParticleSystem;
		localBloodsObj.Play();
	}
//	
//	[RPC]
//	void FireBarfParticles()
//	{
//		Vector3 position = transform.position + new Vector3(0,0,-0.1f);
//				
//		float xRot = (GetComponent<tk2dSprite>().FlipX) ? 186.5f : -6.5f;
//		barfs.transform.rotation = Quaternion.Euler(xRot, 90, 0);
//		
//		ParticleSystem localBarfsObj = GameObject.Instantiate(barfs, position, barfs.transform.rotation) as ParticleSystem;
//		localBarfsObj.Play();
//	}
//	
//	[RPC]
//	void FireShitParticles()
//	{	
//		float xRot = (GetComponent<tk2dSprite>().FlipX) ? -6.5f : 186.5f;
//		shits.transform.rotation = Quaternion.Euler(xRot, 90, 0);
//		
//		float xPos = (GetComponent<tk2dSprite>().FlipX) ? .5f : -.5f;
//		Vector3 position = transform.position + new Vector3(xPos, -1.1f, -0.1f);
//		
//		
//		ParticleSystem localShitsObj = GameObject.Instantiate(shits, position, shits.transform.rotation) as ParticleSystem;
//		localShitsObj.Play();
//	}
//	
//	[RPC]
//	void FirePissParticles()
//	{	
//		float xRot = (GetComponent<tk2dSprite>().FlipX) ? 186.5f : -6.5f;
//		piss.transform.rotation = Quaternion.Euler(xRot, 90, 0);
//		
//		float xPos = (GetComponent<tk2dSprite>().FlipX) ? -.5f : .5f;
//		Vector3 position = transform.position + new Vector3(xPos, -1.1f, -0.1f);
//		
//		ParticleSystem localPissObj = GameObject.Instantiate(piss, position, piss.transform.rotation) as ParticleSystem;
//		localPissObj.Play();
//	}
}