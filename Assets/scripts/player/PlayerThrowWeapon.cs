using UnityEngine;
using System.Collections;

public class PlayerThrowWeapon : MonoBehaviour {

    public GameObject weaponPrefab;

	void Update()
    {
        if (networkView.isMine)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                SendMessage("StartThrowAnimation"); // play throw animation
            }
        }
    }

    void HandleDaggerSpawning(bool goesLeft)
    {
        string daggerTag = "";

        // spawn ours
        if (gameObject.tag == "PlayerOne") daggerTag = "WeaponOne";
        if (gameObject.tag == "PlayerTwo") daggerTag = "WeaponTwo";
        DoDaggerSpawn(transform.position, daggerTag, goesLeft);

        // tell other player to spawn theirs
        networkView.RPC("DoDaggerSpawn", RPCMode.Others, transform.position, daggerTag, goesLeft);
    }

    [RPC]
    void DoDaggerSpawn(Vector3 position, string weaponTag, bool goingLeft)
    {
        weaponPrefab.tag = weaponTag; // tag it by owner
        weaponPrefab.GetComponent<WeaponAppearance>().SetAppearance(goingLeft);
        GameObject.Instantiate(weaponPrefab, position, Quaternion.identity);
    }
}
