using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public GameObject interactText;
    private NetworkServerUI network;
    private void Awake()
    {
        network = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkServerUI>();
    }
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
        network.ServerSendMessage("Pause");
    }
    public void Resume()
    {
        network.ServerSendMessage("Unpause");
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
        StartCoroutine(Quit2());
    }
    private IEnumerator Quit2()
    {
        network.ServerSendMessage("ExitGame");
        yield return new WaitForSeconds(.1f);
        network.CloseServer();
        SceneManager.LoadScene(0);
    }
}
