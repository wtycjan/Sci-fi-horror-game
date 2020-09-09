using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Resume();
        }
    }
    public void Resume()
    {
        gameObject.SetActive(false);
        GameData.canPause = true;
        Time.timeScale = 1;
        Cursor.visible = false;
    }
    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
}
