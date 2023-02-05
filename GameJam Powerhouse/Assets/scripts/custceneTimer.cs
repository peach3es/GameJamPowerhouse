using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class custceneTimer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitForEndOfCutscene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator waitForEndOfCutscene()
    {
        yield return new WaitForSeconds(30);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
