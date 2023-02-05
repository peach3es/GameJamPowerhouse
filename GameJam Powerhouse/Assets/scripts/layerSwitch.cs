using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class layerSwitch : MonoBehaviour
{
    [SerializeField] GameObject ground_layer;
    [SerializeField] GameObject underGround_layer;
    [SerializeField] GameObject mole;
    [SerializeField] GameObject seed;
    [SerializeField] GameObject seedHole;
    [SerializeField] GameObject[] objectsToSwitch;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("space") && mole.GetComponent<mole>().canSwitchLayers)
        {
            if (ground_layer.GetComponent<Renderer>().enabled) //Check if ground or underground is visible
            {
                ground_layer.GetComponent<Renderer>().enabled = !(ground_layer.GetComponent<Renderer>().enabled); //sets ground layer to visible and then makes it equal to the opposite
                underGround_layer.GetComponent<Renderer>().enabled = underGround_layer.GetComponent<Renderer>(); //sets underground layer to visible 
                mole.transform.position = new Vector3(mole.transform.position.x, mole.transform.position.y, -0.1f);

                // Disable sprites on other layer
                seed.GetComponent<Renderer>().enabled = false;
                seedHole.GetComponent<Renderer>().enabled = false;
                foreach (GameObject obj in objectsToSwitch)
                {
                    obj.GetComponent<Renderer>().enabled = false;
                }
                foreach (Renderer renderer in underGround_layer.GetComponentsInChildren<Renderer>())
                {
                    renderer.enabled = true;
                }
                foreach (Renderer renderer in ground_layer.GetComponentsInChildren<Renderer>())
                {
                    renderer.enabled = false;
                }
                mole.layer = 8;
                Debug.Log("switch to underground! mole z: " + mole.transform.position);
            }

            // Underground visible -> Change to ground
            else if (underGround_layer.GetComponent<Renderer>().enabled)
            {
                ground_layer.GetComponent<Renderer>().enabled = ground_layer.GetComponent<Renderer>(); //sets ground layer to visible 
                underGround_layer.GetComponent<Renderer>().enabled = !(underGround_layer.GetComponent<Renderer>().enabled); //sets underground layer to visible and then makes it equal to the opposite
                mole.transform.position = new Vector3(mole.transform.position.x, mole.transform.position.y, -1.1f);

                // Disable sprites on other layer


                seedHole.GetComponent<Renderer>().enabled = true;
                seed.GetComponent<Renderer>().enabled = true;
                foreach (GameObject obj in objectsToSwitch)
                {
                    obj.GetComponent<Renderer>().enabled = true;
                }
                foreach (Renderer renderer in underGround_layer.GetComponentsInChildren<Renderer>())
                {
                    renderer.enabled = false;
                }
                foreach (Renderer renderer in ground_layer.GetComponentsInChildren<Renderer>())
                {
                    renderer.enabled = true;
                }
                mole.layer = 7;
                Debug.Log("switch to Ground! mole z: " + mole.transform.position);
            }
        }
    }
}