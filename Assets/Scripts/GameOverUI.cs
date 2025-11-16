using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Button restartButton; // 重新開始按鈕
    [SerializeField] private Button goToLobbyButton; // 回到主畫面按鈕
    [SerializeField] private GameObject uiRoot;

    public void Awake()
    {
        uiRoot.SetActive(false);
        goToLobbyButton.onClick.AddListener(GoToLoobby);
        restartButton.onClick.AddListener(RestartGame);
    }

    public void GoToLoobby()
    {
        SceneManager.LoadScene("Lobby");
        Time.timeScale = 1; // 遊戲時間恢復
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1; // 遊戲時間恢復
    }

    /// <summary>
    /// 打開GameOverUI
    /// </summary>

    public void OpenGameOverUI()
    {
        Time.timeScale = 0.0000000000001f; // 遊戲時間暫停
        scoreText.text = "得分：" + GameManager.instance.GetScore().ToString();
        uiRoot.SetActive(true);
    }

}
