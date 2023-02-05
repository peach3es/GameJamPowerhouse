using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class seed : MonoBehaviour
{

    [SerializeField] Renderer renderer;
    [SerializeField] ParticleSystem particleSystem;

    [SerializeField] LevelLoader levelLoader;

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
            StartCoroutine(postLevelWait());
        }
    }

    IEnumerator postLevelWait()
    {
        particleSystem.Play();
        renderer.enabled = false;

        yield return new WaitForSeconds(0.6f);

        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        levelLoader.LoadNextLevel();
        Debug.Log("Level Complete!");
    }

}

