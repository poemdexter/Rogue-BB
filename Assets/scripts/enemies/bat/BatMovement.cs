using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BatMovement : MonoBehaviour
{
    public Dictionary<string, Vector3> diagonalMovement;
    public string currentDirection;
    public float speed = 15;

	void Start()
    {
        diagonalMovement = new Dictionary<string, Vector3>();
        diagonalMovement.Add("NE", new Vector3( 1, 1,0));
        diagonalMovement.Add("SE", new Vector3( 1,-1,0));
        diagonalMovement.Add("SW", new Vector3(-1,-1,0));
        diagonalMovement.Add("NW", new Vector3(-1, 1,0));
        List<string> keyList = new List<string>(diagonalMovement.Keys);
        currentDirection = keyList[Random.Range(0,3)];
	}

    void FixedUpdate()
    {
        transform.Translate(diagonalMovement[currentDirection] * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Solid")
            SwitchDirection(collision.contacts);
    }

    /* we need to determine what direction we collided with environment so we can change direction.
     * we check first and last point in the collection and determine what pieces of the vector positions match
     * and then flip horizontal/vertical while maintaining the opposite movement (vertical/horizontal respectively)
     */
    void SwitchDirection(ContactPoint[] hits)
    {
        // InverseTransformPoint will convert to local space so we can easily tell where it is in relation to us
        Vector3 relativePosition1 = transform.InverseTransformPoint(hits[0].point);
        Vector3 relativePosition2 = transform.InverseTransformPoint(hits[hits.Length - 1].point);

        if (relativePosition1.x > 0 && relativePosition2.x > 0) // right
        {
            if (currentDirection == "NE") currentDirection = "NW";
            if (currentDirection == "SE") currentDirection = "SW";

            GetComponent<tk2dSprite>().FlipX = true;
        }
        if (relativePosition1.x < 0 && relativePosition2.x < 0) // left
        {
            if (currentDirection == "NW") currentDirection = "NE";
            if (currentDirection == "SW") currentDirection = "SE";

            GetComponent<tk2dSprite>().FlipX = false;
        }
        if (relativePosition1.y > 0 && relativePosition2.y > 0) // up
        {
            if (currentDirection == "NE") currentDirection = "SE";
            if (currentDirection == "NW") currentDirection = "SW";
        }
        if (relativePosition1.y < 0 && relativePosition2.y < 0) // down
        {
            if (currentDirection == "SE") currentDirection = "NE";
            if (currentDirection == "SW") currentDirection = "NW";
        }
    }
}
