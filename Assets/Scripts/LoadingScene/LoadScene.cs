using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class LoadScene : MonoBehaviour
{
    public GameObject loadingBar;
    private void Start()
    {
        StartCoroutine(LoadAsyncOperation());
    }

    IEnumerator LoadAsyncOperation()
    {
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(GameData.loadLevel);

        while (gameLevel.progress < 1)
        {
            loadingBar.transform.Rotate(new Vector3(0, 0, 5));
            yield return new WaitForEndOfFrame();

        }
        
    }
}
