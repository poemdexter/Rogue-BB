using UnityEngine;
using System.Collections;

public class PlayerAppearance : MonoBehaviour {

	void Start()
	{
		if ((Network.isClient && !networkView.isMine) || (Network.isServer && networkView.isMine))
			GetComponent<tk2dSprite>().SetSprite("red_stand");
		else if ((Network.isClient && networkView.isMine) || (Network.isServer && !networkView.isMine))
			GetComponent<tk2dSprite>().SetSprite("blu_stand");
	}
}
