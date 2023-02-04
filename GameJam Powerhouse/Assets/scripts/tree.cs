using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tree : MonoBehaviour
{

    [SerializeField] GameObject mole;
    [SerializeField] float pullPower;
    bool cantLaunch;
    Vector3 molePos;
    Vector3 treePos;
    Vector3 pullVector;
    // Start is called before the first frame update
    void Start()
    {
        cantLaunch = mole.GetComponent<mole>().canSlingshot;
        treePos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        cantLaunch = mole.GetComponent<mole>().canSlingshot;
        if (!cantLaunch)
        {
            molePos = mole.transform.position;
            pullVector = treePos - molePos;

            // Wait before pulling
            StartCoroutine(pullWithDelay());


        }
    }

    IEnumerator pullWithDelay()
    {
        // Wait
        yield return new WaitForSeconds(0.5f);

        // Pull
        mole.GetComponent<Rigidbody2D>().AddForce(pullVector * pullPower, ForceMode2D.Impulse);

        mole.GetComponent<mole>().canSlingshot = true;
    }
}
