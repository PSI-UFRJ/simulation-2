using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;
using System;

public class IntroductionManager : MonoBehaviour
{
    private bool isPopupOn;

    [SerializeField]
    private GameObject popupExit;

    [SerializeField]
    private UnityEngine.UI.Button muteBtn;

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();
    // Start is called before the first frame update
    void Start()
    {
        InitializeIntroduction();
        isPopupOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeIntroduction()
    {
        ActivateModeBtn();

        this.gameObject.GetComponent<SoundManager>().StartSound();
        this.gameObject.GetComponent<SoundManager>().PlaySound("Environment");

        IniteMuteBtnSprite();
    }

    public void IniteMuteBtnSprite()
    {
        if (AudioListener.pause)
        {
            muteBtn.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load("UI Components/Header_MuteBtn_Off", typeof(Sprite)) as Sprite;
        }
        else
        {
            muteBtn.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load("UI Components/Header_MuteBtn_On", typeof(Sprite)) as Sprite;
        }
    }

    public void ActivateModeBtn()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        //UnityEngine.UI.Button introBtn = GameObject.Find("IntroductionModeBtn").GetComponent<UnityEngine.UI.Button>();
        UnityEngine.UI.Button gameBtn = GameObject.Find("GameModeBtn").GetComponent<UnityEngine.UI.Button>();
        UnityEngine.UI.Button labBtn = GameObject.Find("LabModeBtn").GetComponent<UnityEngine.UI.Button>();

    }

    public void SetPopup(GameObject popup, bool isActive, string message)
    {
        isPopupOn = isActive;
        if(message != "")
        {
            Transform popupText = popup.GetComponent<Transform>().Find("Image").Find("Text");
            popupText.GetComponent<UnityEngine.UI.Text>().text = message;
        }

        popup.SetActive(isActive);
    }

    public void DisablePopup(GameObject popup)
    {
        isPopupOn = false;
        popup.SetActive(false);
    }

    public void ChangeScene(string sceneName)
    {
        if (sceneName == "Game")
        {
            SceneManager.LoadScene("Game");
        }
        else if (sceneName == "Introduction")
        {
            SceneManager.LoadScene("Introduction");
        }
        else if (sceneName == "Menu")
        {
            SceneManager.LoadScene("Menu");
        }
    }

    public void StartClickSound()
    {
        this.gameObject.GetComponent<SoundManager>().PlaySound("Click");
    }

    public void OnMinimizeButtonClick()
    {
        ShowWindow(GetActiveWindow(), 2);
    }

    #region Close App
    public void QuitApplication(bool popup = false)
    {
        if (popup)
        {
            SetPopup(popupExit, true, "");
            return;
        }

        Debug.Log("Closed application");
        Application.Quit();
    }
    #endregion
}
