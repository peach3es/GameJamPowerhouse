using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mole : MonoBehaviour
{
    [SerializeField] Rigidbody2D moleBody;
    [SerializeField] int movementSpeed;
    

    [SerializeField] float launchSpeed;
    [SerializeField] float maxLaunchSpeed;

    private Vector2 playerPosBeforeSlingshot;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // ** Underground movement **
        if (Input.GetKey("w"))
        {
            Debug.Log("w pressed");
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

        // ** Slingshot **

        // Holding down
        if (Input.GetMouseButtonDown(0)) 
        {
            // Player slingshot root
            playerPosBeforeSlingshot = moleBody.transform.position;

            Debug.Log("Player pos: " + playerPosBeforeSlingshot);
        }

        // Release
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("Mouse pos:" + mousePos);

            Vector2 launchVector = (playerPosBeforeSlingshot - mousePos) * launchSpeed;

            // Caps launch speed to max launch speed
            if (launchVector.magnitude > maxLaunchSpeed)
            {
                launchVector.Normalize();
                launchVector *= maxLaunchSpeed;
                Debug.Log("normalizing to" + launchVector);
            }

            Debug.Log("Launch: " + launchVector);

            moleBody.AddForce(launchVector, (ForceMode2D)ForceMode.Impulse);
        }   
    }
}
