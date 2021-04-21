using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
    }

    public void LoadGameOver()
    {
        StartCoroutine(DelayLoading(2));
    }

    public void LoadGameScene()
    {
        StartCoroutine(DelayLoading(1));
    }

    public void LoadStartMenu()
    {
        StartCoroutine(DelayLoading(0));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator DelayLoading(int indexOfScene)
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(indexOfScene);
    }
}
