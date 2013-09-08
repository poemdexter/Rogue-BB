using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {

    private tk2dSpriteAnimator anim;
    public string remoteClip;
    public string prevRemoteClip;

	public bool isLookingLeft = false;
    public bool isThrowing = false;
    public bool throwWait = false;

	public ParticleSystem bloods;
	public ParticleSystem piss;
	public ParticleSystem barfs;
	public ParticleSystem shits;

    // called when we're done with throw animation
    void ThrowCompleteDelegate(tk2dSpriteAnimator sprite, tk2dSpriteAnimationClip clip)
    {
        isThrowing = false;
        throwWait = false;
        anim.Play(prevRemoteClip);
    }

    // hit middle of the throw animation, so release the dagger!
    void SpawnDaggerDelegate(tk2dSpriteAnimator sprite, tk2dSpriteAnimationClip clip, int frameNum)
    {
        SendMessage("HandleDaggerSpawning", isLookingLeft);
    }

    // we're throwing now
    void StartThrowAnimation()
    {
        isThrowing = true;
    }

    // Throwing animation takes priority over all others
    public void HandleMovementAnimations(PlayerMovement.Movement m)
    {
        if (anim == null) anim = GetComponent<tk2dSpriteAnimator>();
        string clip;

        // if we're ready to throw again and we pushed throw button
        if (!throwWait && isThrowing)
        {
            clip = "throw";
            anim.AnimationCompleted = ThrowCompleteDelegate;
            anim.AnimationEventTriggered = SpawnDaggerDelegate;
            throwWait = true;
            anim.Play(clip);
        }
        else if (!throwWait) // we're not mid throw animation so run other animations
        {
            if (m.jumping || m.inAir) clip = "jump";
            else if (m.moveHorizontal) clip = "walk";
            else clip = "still";
            if (prevRemoteClip != clip)
            {
                prevRemoteClip = clip;
                anim.Play(clip);
            }
        }
    }

	void Update () 
	{
		float xAxis = Input.GetAxis("Horizontal");
		if (xAxis != 0) // we're moving
		{
            // face us the correct way
			if (xAxis > 0 && isLookingLeft == true)
			{
				GetComponent<tk2dSprite>().FlipX = false;
                isLookingLeft = false;
			}
			else if (xAxis < 0 && isLookingLeft == false)
			{
				GetComponent<tk2dSprite>().FlipX = true;
                isLookingLeft = true;
			}
		}
		
		if (Input.GetKeyDown(KeyCode.B))
		{
			FireBloodParticles();
		}
	}
	
	void MakeSpriteFaceLeft(bool left)
	{
		GetComponent<tk2dSprite>().FlipX = left;
	}

	void FireBloodParticles()
	{
		Vector3 position = transform.position + new Vector3(0,0,-0.1f);

		float xRot = (GetComponent<tk2dSprite>().FlipX) ? 186.5f : -6.5f;
		bloods.transform.rotation = Quaternion.Euler(xRot, 90, 0);

		ParticleSystem localBloodsObj = GameObject.Instantiate(bloods, position, bloods.transform.rotation) as ParticleSystem;
		localBloodsObj.Play();
	}
}