using UnityEngine;
using System.Collections;

public class WeaponAppearance : MonoBehaviour
{
    public bool goingLeft = false;

    public void SetAppearance(bool goesLeft)
    {
        // face it correct way
        goingLeft = goesLeft;
        if (goingLeft)
            GetComponent<tk2dSprite>().FlipX = true;
        else
            GetComponent<tk2dSprite>().FlipX = false;

        // color it the correct way
        if (gameObject.tag == "WeaponOne") GetComponent<tk2dSprite>().SetSprite("red_knife");
        if (gameObject.tag == "WeaponTwo") GetComponent<tk2dSprite>().SetSprite("grn_knife");
    }
}