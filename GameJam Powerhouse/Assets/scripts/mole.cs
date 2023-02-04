using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mole : MonoBehaviour
{
    [SerializeField] Rigidbody2D moleBody;
    [SerializeField] int movementSpeed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w"))
        {
            moleBody.AddForce(new Vector2(0,movementSpeed), (ForceMode2D)ForceMode.VelocityChange);
        }
        if(Input.GetKey("s"))
        {
            moleBody.AddForce(new Vector2(0, -movementSpeed), (ForceMode2D)ForceMode.VelocityChange);
        }
        if (Input.GetKey("a"))
        {
            moleBody.AddForce(new Vector2(-movementSpeed, 0), (ForceMode2D)ForceMode.VelocityChange);
        }
        if (Input.GetKey("d"))
        {
            moleBody.AddForce(new Vector2(movementSpeed, 0), (ForceMode2D)ForceMode.VelocityChange);
        }
    }
}
