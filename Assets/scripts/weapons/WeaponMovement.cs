using UnityEngine;
using System.Collections;

public class WeaponMovement : MonoBehaviour
{
    public float speed;

	void FixedUpdate ()
    {
        int x = (GetComponent<WeaponAppearance>().goingLeft) ? -1 : 1;
        transform.position += new Vector3(x,0,0) * speed * Time.deltaTime;
	}
}
