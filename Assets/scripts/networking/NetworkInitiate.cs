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
    }
}