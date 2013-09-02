using UnityEngine;
using System.Collections;

public class PlayerThrowWeapon : MonoBehaviour {

	void Update()
    {
        // need to sync this and set owner
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SendMessage("StartThrowAnimation"); // play throw animation
        }
    }
}
