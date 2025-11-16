using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private GameObject settingsPanel;

    private void Awake()
    {
       // startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);  // 註冊按鈕點擊事件，點擊後執行QuitGame方法
        settingsButton.onClick.AddListener(OpenSettings); // 註冊按鈕點擊事件，點擊後執行OpenSettings方法
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }


    public void QuitGame() 
    {
        Application.Quit();  // 退出遊戲，在Unity Editor中不會退出，在應用程式中會退出
        // Debug.Log("退出遊戲");
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }
    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }
}
