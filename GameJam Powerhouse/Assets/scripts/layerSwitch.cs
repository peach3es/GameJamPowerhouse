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
            if (ground_layer.activeSelf)
            {
                underGround_layer.SetActive(true);
                ground_layer.SetActive(false);
                Debug.Log("switch to underground");
            }
            else if (underGround_layer.activeSelf)
            {
                ground_layer.SetActive(true);
                underGround_layer.SetActive(false);
                Debug.Log("switch to ground");
            }
        }
    }
}