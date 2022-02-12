using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InitializeMenu();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitializeMenu()
    {
        this.gameObject.GetComponent<SoundManager>().StartSound();
    }

    public void LoadScene(string sceneName)
    {
        if (sceneName == "Game")
        {
            SceneManager.LoadScene("Game");
        }
        else if (sceneName == "Introduction")
        {
            SceneManager.LoadScene("Introduction");
        }
    }

    public void QuitApplication()
    {
        Debug.Log("Closed application");
        Application.Quit();
    }

    public void CloseModal(GameObject modal)
    {
        modal.SetActive(false);
    }

    public void OpenModal(GameObject modal)
    {
        modal.SetActive(true);
    }

    public void StartClickSound()
    {
        this.gameObject.GetComponent<SoundManager>().PlaySound("Click");
    }
}