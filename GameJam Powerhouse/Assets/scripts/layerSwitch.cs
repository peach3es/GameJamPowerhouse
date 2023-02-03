using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class layerSwitch : MonoBehaviour
{
    [SerializeField] GameObject ground_layer;
    [SerializeField] GameObject underGround_layer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (ground_layer.GetComponent<Renderer>()) //Check if ground or underground is visible
            {
                ground_layer.GetComponent<Renderer>().enabled = !(ground_layer.GetComponent<Renderer>().enabled); //sets ground layer to visible and then makes it equal to the opposite
                underGround_layer.GetComponent<Renderer>().enabled = underGround_layer.GetComponent<Renderer>(); //sets underground layer to visible 
                Debug.Log("switch to underground");
            }
            else if (underGround_layer.GetComponent<Renderer>().enabled)
            {
                ground_layer.GetComponent<Renderer>().enabled = ground_layer.GetComponent<Renderer>(); //sets ground layer to visible 
                underGround_layer.GetComponent<Renderer>().enabled = !(underGround_layer.GetComponent<Renderer>().enabled); //sets underground layer to visible and then makes it equal to the opposite
                Debug.Log("switch to ground");
            }
        }
    }
}