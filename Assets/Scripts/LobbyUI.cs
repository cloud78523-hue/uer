using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyUI : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

}
