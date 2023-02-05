using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endCutsceneTimer : MonoBehaviour
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
        yield return new WaitForSeconds(10);

        SceneManager.LoadScene(0);
    }

}
