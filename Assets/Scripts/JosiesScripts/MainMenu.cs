using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Animator transition;
    public GameObject ControllerScreen;

    public float transitionTime = 1f;

    private void Update()
    {

        if (ControllerScreen.activeInHierarchy && Input.GetKeyDown(KeyCode.Space))
        {
            LoadNextLevel();
        }
    }

    public void PlayGame()
    {
        LoadNextLevel();
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
