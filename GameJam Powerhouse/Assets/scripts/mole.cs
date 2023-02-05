using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mole : MonoBehaviour
{
    [SerializeField] Rigidbody2D moleBody;
    [SerializeField] float movementSpeed;

    [SerializeField] AudioClip[] diggingSounds;
    

    [SerializeField] float launchSpeed;
    [SerializeField] float maxLaunchSpeed;

    [SerializeField] float launchMultiplier;

    [SerializeField] GameObject tailSlingshot;
    [SerializeField] GameObject tail;

    [SerializeField] Sprite idleSprite;
    [SerializeField] Sprite flyingSprite;

    // private SpriteRenderer tailSlingshotRenderer;

    public Animator animator;

    private SpriteRenderer spriteRenderer;

    private Animator tailAnimator;

    private SpriteRenderer tailSpriteRenderer;

    public bool canSwitchLayers;
    public bool canSlingshot;

    private Vector2 playerPosBeforeSlingshot;

    private bool isAimingSlingshot;

    private AudioSource launching;
    private AudioSource digging;

    private LineRenderer vineSlingshot;

    // Start is called before the first frame update
    void Start()
    {
        tailAnimator = tail.GetComponent<Animator>();
        tailSpriteRenderer = tail.GetComponent<SpriteRenderer>();
        canSlingshot = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        launching = gameObject.GetComponents<AudioSource>()[0];
        digging = gameObject.GetComponents<AudioSource>()[1];
        vineSlingshot = tailSlingshot.GetComponent<LineRenderer>();
        // tailSlingshotRenderer = tailSlingshot.GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        // Reset if walking normally
        if (!isAimingSlingshot)
        {
            animator.SetBool("right", false);
            animator.SetBool("left", false);
            animator.SetBool("front", false);
            animator.SetBool("back", false);

            tailAnimator.SetBool("right", false);
            tailAnimator.SetBool("left", false);
            tailAnimator.SetBool("front", false);
            tailAnimator.SetBool("back", false);
            // tail.transform.position = new Vector3(tail.transform.position.x, tail.transform.position.y, 0);
            tail.transform.localPosition = new Vector3(tail.transform.localPosition.x, tail.transform.localPosition.y, 0.015f);

            tailSpriteRenderer.enabled = true;
        }
        // Aiming slingshot, make tail invisible
        else
        {
            tailSpriteRenderer.enabled = false;
        }

        // Underground, move with wasd
        if (transform.position.z == -0.1f) 
        { 
            // ** Underground movement **
            if (Input.GetKey("w"))
            {
                animator.SetBool("back", true);
                tailAnimator.SetBool("back", true);
                tail.transform.localPosition = new Vector3(tail.transform.localPosition.x, tail.transform.localPosition.y, -0.01f);
                moleBody.AddForce(new Vector2(0,movementSpeed), (ForceMode2D)ForceMode.VelocityChange);
            }
            if(Input.GetKey("s"))
            {
                animator.SetBool("front", true);
                tailAnimator.SetBool("front", true);
                moleBody.AddForce(new Vector2(0, -movementSpeed) , (ForceMode2D)ForceMode.VelocityChange);
            }
            if (Input.GetKey("a"))
            {
                animator.SetBool("left", true);
                tailAnimator.SetBool("left", true);
                moleBody.AddForce(new Vector2(-movementSpeed, 0) , (ForceMode2D)ForceMode.VelocityChange);
            }
            if (Input.GetKey("d"))
            {
                animator.SetBool("right",true);
                tailAnimator.SetBool("right", true);
                moleBody.AddForce(new Vector2(movementSpeed, 0) , (ForceMode2D)ForceMode.VelocityChange);
            }
            if (Input.GetKeyUp("w"))
            {
                tail.transform.localPosition = new Vector3(tail.transform.localPosition.x, tail.transform.localPosition.y, 0.015f);
                // tail.transform.position = new Vector3(tail.transform.position.x, tail.transform.position.y, 0);
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
    }

    // Update is called once per frame
    void Update()
    {
        // ** Slingshot **
        // Above ground, can slingshot
        if (transform.position.z == -1.1f && canSlingshot)
        // else if (transform.position.z == -1.1f && canSlingshot)
        {
            // First press down
            if (Input.GetMouseButtonDown(0)) 
            {
                // Player slingshot root
                playerPosBeforeSlingshot = moleBody.transform.position;

                isAimingSlingshot = true;

                // tailSlingshotRenderer.enabled = true;

                vineSlingshot.enabled = true;
            }
            // Holding down
            if (Input.GetMouseButton(0))
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Vector2 launchVector = (playerPosBeforeSlingshot - mousePos) * launchSpeed;

                lookInThrownDirection(launchVector);

                vineSlingshot.SetPosition(0, tailSlingshot.transform.position);
                vineSlingshot.SetPosition(1, mousePos);

                // tailSlingshot.transform.LookAt(mousePos);
                // tailSlingshot.transform.Rotate(new Vector3(0,90,90));
                // tailSlingshotRenderer.size = new Vector2(tailSlingshotRenderer.size.x, Vector3.Distance(tailSlingshot.transform.position, mousePos));
            }
            // Release
            if (Input.GetMouseButtonUp(0))
            {
                launching.Play();
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Vector2 launchVector = (playerPosBeforeSlingshot - mousePos) * launchSpeed;

                lookInThrownDirection(launchVector);

                // tailSlingshotRenderer.enabled = false;

                Debug.Log("Changing to flying sprite");
                animator.enabled = false;
                spriteRenderer.sprite = flyingSprite;

                isAimingSlingshot = false;

                vineSlingshot.enabled = false;

                // Caps launch speed to max launch speed
                if (launchVector.magnitude > maxLaunchSpeed)
                {
                    launchVector.Normalize();
                    launchVector *= maxLaunchSpeed;
                }

                moleBody.AddForce(launchVector, (ForceMode2D)ForceMode.Impulse);

                StartCoroutine(waitInDirection());
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

    IEnumerator waitInDirection()
    {
        yield return new WaitForSeconds(0.5f);

        transform.rotation = Quaternion.identity;
        spriteRenderer.sprite = idleSprite;
        animator.enabled = true;
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

