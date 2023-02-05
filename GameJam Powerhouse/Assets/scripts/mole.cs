using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mole : MonoBehaviour
{
    [SerializeField] Rigidbody2D moleBody;
    [SerializeField] int movementSpeed;

    [SerializeField] AudioClip[] diggingSounds;
    

    [SerializeField] float launchSpeed;
    [SerializeField] float maxLaunchSpeed;
    public Animator animator;

    public bool canSwitchLayers;
    public bool canSlingshot;

    private Vector2 playerPosBeforeSlingshot;

    private bool isAimingSlingshot;

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
        // Reset if walking normally
        if (!isAimingSlingshot)
        {
            animator.SetBool("right", false);
            animator.SetBool("left", false);
            animator.SetBool("front", false);
            animator.SetBool("back", false);
        }

        // Underground, move with wasd
        if (transform.position.z == -0.1f) 
        { 
            // ** Underground movement **
            if (Input.GetKey("w"))
            {
                animator.SetBool("back", true);
                moleBody.AddForce(new Vector2(0,movementSpeed), (ForceMode2D)ForceMode.VelocityChange);
            }
            if(Input.GetKey("s"))
            {
                animator.SetBool("front", true);
                moleBody.AddForce(new Vector2(0, -movementSpeed), (ForceMode2D)ForceMode.VelocityChange);
            }
            if (Input.GetKey("a"))
            {
                animator.SetBool("left", true);
                moleBody.AddForce(new Vector2(-movementSpeed, 0), (ForceMode2D)ForceMode.VelocityChange);
            }
            if (Input.GetKey("d"))
            {
                animator.SetBool("right",true);
                moleBody.AddForce(new Vector2(movementSpeed, 0), (ForceMode2D)ForceMode.VelocityChange);
            }

            // ** Digging sound

            // Moving, play sound if not playing
            if (moleBody.velocity.magnitude > 0.1)
            {
                if (!digging.isPlaying) {
                    int clipIndex = Random.Range(0, 5);
                    digging.PlayOneShot(diggingSounds[clipIndex]);  
                }
            } else
            {
                if (digging.isPlaying) {
                    digging.Stop();
                }
            }
        }

        // ** Slingshot **
        // Above ground, can slingshot
        else if (transform.position.z == -1.1f && canSlingshot)
        {
            // First press down
            if (Input.GetMouseButtonDown(0)) 
            {
                // Player slingshot root
                playerPosBeforeSlingshot = moleBody.transform.position;

                isAimingSlingshot = true;
            }
            // Holding down
            if (Input.GetMouseButton(0))
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Vector2 launchVector = (playerPosBeforeSlingshot - mousePos) * launchSpeed;

                lookInThrownDirection(launchVector);
            }
            // Release
            if (Input.GetMouseButtonUp(0))
            {
                launching.Play();
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Vector2 launchVector = (playerPosBeforeSlingshot - mousePos) * launchSpeed;

                lookInThrownDirection(launchVector);

                // Caps launch speed to max launch speed
                if (launchVector.magnitude > maxLaunchSpeed)
                {
                    launchVector.Normalize();
                    launchVector *= maxLaunchSpeed;
                }

                moleBody.AddForce(launchVector, (ForceMode2D)ForceMode.Impulse);

                transform.rotation = Quaternion.identity;
            }   
        }
    }

    private void lookInThrownDirection(Vector2 launchVector)
    {
        float angle = 360 - (Mathf.Atan2(launchVector.x, launchVector.y) * Mathf.Rad2Deg * Mathf.Sign(launchVector.x));
        if (launchVector.x > 0)
        {
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else {
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
        }
        Debug.Log("aiming with angle: " + angle);
        if (launchVector.y > 0)
        {
            animator.SetBool("front", false);
            animator.SetBool("back", true);
        }
        else
        {
            animator.SetBool("front", true);
            animator.SetBool("back", false);
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

    private void OnTriggerStay2D(Collider2D collider)
    {
        
        if (collider.gameObject.CompareTag("concrete"))
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

