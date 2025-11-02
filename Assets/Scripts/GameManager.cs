using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 單例模式

    public static GameManager instance;

    private int coinCount = 0;  // 收集的金幣數量

    [SerializeField] private Text coinText;  
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateCoinText();
    }


    /// <summary>
    /// 更新金幣數量
    /// </summary>

    public void AddCoin()
    {
        coinCount ++;
        UpdateCoinText();
    }

    /// <summary>
    /// 更新金幣數量文字
    /// </summary>

    public void UpdateCoinText()
    {
        coinText.text = coinCount.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
