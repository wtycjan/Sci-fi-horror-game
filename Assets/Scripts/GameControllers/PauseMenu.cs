using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public GameObject interactText;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Resume();
        }
    }
    private void OnEnable()
    {
        interactText.SetActive(false);
    }
    public void Resume()
    {
        gameObject.SetActive(false);
        GameData.canPause = true;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        interactText.SetActive(true);
    }
    public void Quit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
