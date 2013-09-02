using UnityEngine;
using System.Collections;

public class WeaponAppearance : MonoBehaviour
{
    public bool goingLeft = false;

    public void SetAppearance(bool goesLeft)
    {
        if (gameObject.tag == "WeaponOne") GetComponent<tk2dSprite>().SetSprite("red_knife");
        if (gameObject.tag == "WeaponTwo") GetComponent<tk2dSprite>().SetSprite("grn_knife");
    }
}