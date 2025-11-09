using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 單例模式

    public static GameManager instance;

    private int coinCount = 0;  // 收集的金幣數量
    private float hp = 100f;    // 玩家當前的生命值
    private float maxHp = 100f; // 玩家最大生命值

    [SerializeField] private Text coinText;
    [SerializeField] private Image hpBar;
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
        hp = maxHp;
        hpBar.fillAmount = hp / maxHp;
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

    public void TakeDamage(float damage)
    {
       hp = hp - damage;
       hpBar.fillAmount = hp / maxHp;
    }


   
}
