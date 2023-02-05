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

    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        Debug.Log(levelIndex);

        sceneTransitionObject.SetActive(true);
        transition.SetTrigger("StartTransition");

        yield return new WaitForSeconds(transitionTime);

        sceneTransitionObject.SetActive(false);
        SceneManager.LoadScene(levelIndex);
    }
}
