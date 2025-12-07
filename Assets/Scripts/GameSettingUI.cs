using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSettingUI : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button goToLobbyButton;
    [SerializeField] private Button openButton;

    [SerializeField] private GameObject uiRoot;
    [SerializeField] private GameObject settingsPanel; // 設定音量面板

    private void Awake()
    {
        uiRoot.SetActive(false);

        continueButton.onClick.AddListener(ContinueGame);
        settingsButton.onClick.AddListener(OpenAudioSetting);
        goToLobbyButton.onClick.AddListener(GoToLobby);
        openButton.onClick.AddListener(OpenSetting);
    }

    public void OpenSetting()
    {
        uiRoot.SetActive(true);
        Time.timeScale = 0.00000000001f; // 暫停遊戲
    }
    private void ContinueGame()
    {
        uiRoot.SetActive(false);
        Time.timeScale = 1f; // 繼續遊戲
    }
    private void OpenAudioSetting()
    {
        settingsPanel.SetActive(true);
    }
    private void GoToLobby()
    {
        Time.timeScale = 1f; // 繼續遊戲
        SceneManager.LoadScene("Lobby");
    }
}
