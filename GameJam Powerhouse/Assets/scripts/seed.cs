using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seed : MonoBehaviour
{

    [SerializeField] Renderer renderer;
    [SerializeField] ParticleSystem particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.CompareTag("seed_hole"))
        {
            particleSystem.Play();
            renderer.enabled = false;
            Debug.Log("Level Complete!");
        }
    }

}
