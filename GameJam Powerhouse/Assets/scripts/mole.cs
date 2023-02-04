using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mole : MonoBehaviour
{
    [SerializeField] Rigidbody2D moleBody;
    [SerializeField] int movementSpeed;
    

    [SerializeField] float launchSpeed;
    [SerializeField] float maxLaunchSpeed;

    public bool canSwitchLayers;
    public bool canSlingshot;

    private Vector2 playerPosBeforeSlingshot;

    private AudioSource launching;
    private AudioSource digging;

    // Start is called before the first frame update
    void Start()
    {
        canSlingshot = true;
        launching = gameObject.GetComponents<AudioSource>()[0];
        digging = gameObject.GetComponents<AudioSource>()[1];
    }

    // Update is called once per frame
    void Update()
    {
        // Underground, move with wasd
        if (transform.position.z == -0.1f) 
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

            // ** Digging sound

            Debug.Log(moleBody.velocity.magnitude);
            // Moving, play sound if not playing
            if (moleBody.velocity.magnitude > 0.1)
            {
                if (!digging.isPlaying) digging.Play();
            } else
            {
                if (digging.isPlaying) digging.Stop();
            }
        }

        // ** Slingshot **
        // Above ground, can slingshot
        else if (transform.position.z == -1.1f && canSlingshot)
        {
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
                launching.Play();
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collid with hole");
        if (collision.gameObject.CompareTag("hole"))
        {
            canSwitchLayers = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("hole"))
        {
            Debug.Log("collid with hole");
            canSwitchLayers = true;
        }
        else if (collider.gameObject.CompareTag("concrete"))
        {
            Debug.Log("collid with concrete");
            canSlingshot = false;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("hole"))
        {
            canSwitchLayers = false;
        }
        else if (collider.gameObject.CompareTag("concrete"))
        {
            canSlingshot = true;
        }
    }
}

