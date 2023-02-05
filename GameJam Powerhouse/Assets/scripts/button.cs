using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{

    [SerializeField] GameObject wall;
    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collider)
    {

        if (collider.gameObject.CompareTag("mole"))
        {
            animator.SetBool("pressed", true);
            wall.active = false;
            
        }
    }
}
