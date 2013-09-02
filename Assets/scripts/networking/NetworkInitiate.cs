using UnityEngine;
using System.Collections;

public class NetworkInitiate : MonoBehaviour {

    void OnNetworkInstantiate(NetworkMessageInfo msg)
    {
        if (networkView.isMine)
            GetComponent<NetworkInterpolatedTransform>().enabled = false;
        else
        {
            name += "Remote";
            GetComponent<NetworkInterpolatedTransform>().enabled = true;
        }

        // set player tag (host is always player one)
        if ((Network.isServer && networkView.isMine) || (Network.isClient && !networkView.isMine))
            gameObject.tag = "PlayerOne";
        else if ((Network.isClient && networkView.isMine) || (Network.isServer && !networkView.isMine))
            gameObject.tag = "PlayerTwo";
    }
}