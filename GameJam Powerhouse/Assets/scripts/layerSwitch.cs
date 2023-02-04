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
            // Ground visible -> change to underground
            if (ground_layer.GetComponent<Renderer>().enabled) 
            {
                //sets ground layer to visible and then makes it equal to the opposite
                ground_layer.GetComponent<Renderer>().enabled = !(ground_layer.GetComponent<Renderer>().enabled); 
                
                //sets underground layer to visible 
                underGround_layer.GetComponent<Renderer>().enabled = underGround_layer.GetComponent<Renderer>(); 
                Debug.Log("switch to underground");
            }
            
            // Underground visible -> Change to ground
            else if (underGround_layer.GetComponent<Renderer>().enabled)
            {
                //sets ground layer to visible 
                ground_layer.GetComponent<Renderer>().enabled = ground_layer.GetComponent<Renderer>(); 
                
                //sets underground layer to visible and then makes it equal to the opposite
                underGround_layer.GetComponent<Renderer>().enabled = !(underGround_layer.GetComponent<Renderer>().enabled); 
                Debug.Log("switch to ground");
            }
        }
    }
}