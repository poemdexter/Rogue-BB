using UnityEngine;
using System.Collections;

public class PlayerAppearance : MonoBehaviour {

	void Start()
	{
        // PLAYER ONE
		if ((Network.isClient && !networkView.isMine) || (Network.isServer && networkView.isMine))
        {
            GetComponent<tk2dSprite>().SetSprite("red_stand");
        }
        // PLAYER TWO
		else if ((Network.isClient && networkView.isMine) || (Network.isServer && !networkView.isMine))
        {
            GetComponent<tk2dSprite>().SetSprite("grn_stand");
        }
	}
}
