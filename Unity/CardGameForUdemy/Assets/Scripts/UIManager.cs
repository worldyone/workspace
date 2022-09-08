using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] GameObject resultPanel;
    [SerializeField] Text resultText;
    [SerializeField] Text playerHeroHpText;
    [SerializeField] Text enemyHeroHpText;
    [SerializeField] Text playerManaCostText;
    [SerializeField] Text enemyManaCostText;

    // 時間管理
    [SerializeField] Text timeCountText;



    public void ShowResultPanel(bool seeable)
    {
        resultPanel.SetActive(seeable);
    }

    public void ShowHeroHp(int playerHeroHp, int enemyHeroHp)
    {
        playerHeroHpText.text = playerHeroHp.ToString();
        enemyHeroHpText.text = enemyHeroHp.ToString();
    }

    public void ShowManaCost(int playerManaCost, int enemyManaCost)
    {
        playerManaCostText.text = playerManaCost.ToString();
        enemyManaCostText.text = enemyManaCost.ToString();
    }

    public void UpdateTime(int timeCount)
    {
        timeCountText.text = timeCount.ToString();
    }

    public void ShowResult(string result)
    {
        ShowResultPanel(true);
        resultText.text = "WIN";
    }

}
