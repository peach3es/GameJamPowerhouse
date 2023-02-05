using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] GameObject sceneTransitionObject;
    [SerializeField] GameObject pauseMenu;
    public Animator transition;
    public float transitionTime = 4f;

    public void TogglePause()
    {
        // Pause menu open, close it
        if (pauseMenu.activeInHierarchy)
        {
            pauseMenu.SetActive(false);
        }
        // Pause menu closed, open it
        else 
        {
            pauseMenu.SetActive(true);
        }
    }

    public void ReloadScene()
    {
        // Not on main menu or intro, reload on "r" click
        int activeScene = SceneManager.GetActiveScene().buildIndex;
        if (activeScene > 1)
        {
            StartCoroutine(LoadLevel(activeScene));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
        if (Input.GetKeyDown("r"))
        {
            ReloadScene();
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
