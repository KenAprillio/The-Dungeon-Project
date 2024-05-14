using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class M_Menu : MonoBehaviour
{
    public void PlayGame ()
    {
        StartCoroutine(LoadNextScene(SceneManager.GetActiveScene().buildIndex + 1));
    }
    public void QuitGame ()
    {
        Application.Quit();
    }

    public IEnumerator LoadNextScene(int sceneIndex)
    {
        yield return new WaitForSeconds(2f);

        AsyncOperation loadScene = SceneManager.LoadSceneAsync(sceneIndex);

        while (!loadScene.isDone)
        {
            yield return null;
        }
    }
}
