using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] GameObject sceneTransitionObject;
    public Animator transition;
    public float transitionTime = 4f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            // Not on main menu or intro, reload on "r" click
            int activeScene = SceneManager.GetActiveScene().buildIndex;
            if (activeScene > 1)
            {
                StartCoroutine(LoadLevel(activeScene));
            }
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadFirstLevel()
    {
        StartCoroutine(LoadLevel(2));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        Debug.Log(levelIndex);

        sceneTransitionObject.SetActive(true);
        transition.SetTrigger("StartTransition");

        SceneManager.LoadScene(levelIndex);
        yield return new WaitForSeconds(transitionTime);

        sceneTransitionObject.SetActive(false);
    }
}
