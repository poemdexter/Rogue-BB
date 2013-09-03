using UnityEngine;
using System.Collections;

public class WeaponCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Solid")
            Destroy(gameObject);
    }
}