using UnityEngine;
using System.Collections;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform spawnPoint;

	void Start ()
    {
       Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
	}
}
