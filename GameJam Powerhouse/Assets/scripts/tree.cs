using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tree : MonoBehaviour
{

    [SerializeField] GameObject mole;
    [SerializeField] float pullPower;
    [SerializeField] float spriteScale;

    [SerializeField] Sprite vineSprite;
    [SerializeField] Sprite treeSprite;

    public Camera mainCamera;
    public LineRenderer lineRenderer;
    public DistanceJoint2D distanceJoint;

    SpriteRenderer spriteRenderer;
    bool cantLaunch;
    Vector3 molePos;
    Vector3 treePos;
    Vector3 pullVector;

    bool approaching;

    [SerializeField] float approachSpeed;
    float approachCounter;
    bool pulling;

    // Start is called before the first frame update
    void Start()
    {
        distanceJoint.enabled = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        cantLaunch = mole.GetComponent<mole>().canSlingshot;
        treePos = transform.position;
        approaching = false;
        approachCounter = 0.0f;
        pulling = false;
    }

    // Update is called once per frame
    void Update()
    {
        cantLaunch = mole.GetComponent<mole>().canSlingshot;
        if (!cantLaunch)
        {
            // Wait before pulling
            StartCoroutine(pullWithDelay());

            // lineRenderer.SetPosition(1, treePos);
            // lineRenderer.SetPosition(0, mole.transform.position);
        }

        if (approaching)
        {
            approachCounter += .1f/approachSpeed;
            lineRenderer.SetPosition(1, Mathf.Lerp(0, Vector3.Distance(mole.transform.position, treePos), approachCounter) * Vector3.Normalize(mole.transform.position - treePos) + treePos);
        }

        if (pulling)
        {
            lineRenderer.SetPosition(0, mole.transform.position);
            lineRenderer.SetPosition(1, treePos);
        }
    }



    IEnumerator pullWithDelay()
    {
        molePos = mole.transform.position;

        pullVector = treePos - molePos;

        // Start approaching mole
        approaching = true;

        lineRenderer.SetPosition(0, treePos);

        distanceJoint.connectedAnchor = mole.transform.position;
        distanceJoint.enabled = true;
        lineRenderer.enabled = true;

        // Wait
        yield return new WaitForSeconds(0.5f);

        // Pull
        mole.GetComponent<Rigidbody2D>().AddForce(pullVector * pullPower, ForceMode2D.Impulse);

        approaching = false;
        approachCounter = 0.0f;

        pulling = true;

        lineRenderer.SetPosition(0, mole.transform.position);
        lineRenderer.SetPosition(1, treePos);

        distanceJoint.connectedAnchor = mole.transform.position;
        distanceJoint.enabled = true;
        lineRenderer.enabled = true;

        yield return new WaitForSeconds(0.5f);

        pulling = false;

        distanceJoint.enabled = false;
        lineRenderer.enabled = false;

        mole.GetComponent<mole>().canSlingshot = true;

        // spriteRenderer.sprite = treeSprite; 
        // transform.localScale -= pullVector * spriteScale;
    }
}
