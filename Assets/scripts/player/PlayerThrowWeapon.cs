using UnityEngine;
using System.Collections;

public class PlayerThrowWeapon : MonoBehaviour {

    public GameObject weaponPrefab;

	void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SendMessage("StartThrowAnimation"); // play throw animation
        }
    }

    void HandleDaggerSpawning(bool goesLeft)
    {
        DoDaggerSpawn(transform.position, goesLeft);
    }

    void DoDaggerSpawn(Vector3 position, bool goingLeft)
    {
        weaponPrefab.GetComponent<WeaponAppearance>().SetAppearance(goingLeft);
        GameObject.Instantiate(weaponPrefab, position, Quaternion.identity);
    }
}
