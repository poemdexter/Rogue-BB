using UnityEngine;
using System.Collections;

public class NetworkInitiate : MonoBehaviour {

	void OnNetworkInstantiate(NetworkMessageInfo msg)
	{
		Debug.Log("yh");
		if (networkView.isMine)
			GetComponent<NetworkInterpolatedTransform>().enabled = false;

		else // This is just some remote controlled player
		{
			name += "Remote";
			GetComponent<NetworkInterpolatedTransform>().enabled = true;
		}
	}
}
