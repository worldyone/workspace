using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject resultPanel;
    [SerializeField] Text resultText;
    [SerializeField] Transform playerHandTransform;
    [SerializeField] Transform enemyHandTransform;
    [SerializeField] Transform playerFieldTransform;
    [SerializeField] Transform enemyFieldTransform;
    [SerializeField] CardController cardPrefab;
    bool isPlayerTurn;
    List<int> playerDeck = new List<int>() { 1, 2, 3, 2 };
    List<int> enemyDeck = new List<int>() { 2, 1, 2, 3 };
    int playerHeroHp;
    int enemyHeroHp;
    [SerializeField] Text playerHeroHpText;
    [SerializeField] Text enemyHeroHpText;
    public int playerManaCost;
    int playerDefaultManaCost;
    int enemyManaCost;
    int enemyDefaultManaCost;
    [SerializeField] Text playerManaCostText;
    [SerializeField] Text enemyManaCostText;

    // シングルトン
    public static GameManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        resultPanel.SetActive(false);
        playerHeroHp = 5;
        enemyHeroHp = 5;
        playerManaCost = 1;
        playerDefaultManaCost = 1;
        enemyManaCost = 1;
        enemyDefaultManaCost = 1;
        ShowHeroHp();
        ShowManaCost();
        SettingInitHand();
        isPlayerTurn = true;
        TurnCalc();
    }

    public void Restart()
    {
        // handとFieldのカードを削除
        foreach (Transform card in playerHandTransform)
        {
            Destroy(card.gameObject);
        }
        foreach (Transform card in playerFieldTransform)
        {
            Destroy(card.gameObject);
        }
        foreach (Transform card in enemyHandTransform)
        {
            Destroy(card.gameObject);
        }
        foreach (Transform card in enemyFieldTransform)
        {
            Destroy(card.gameObject);
        }

        // デッキを初期化
        playerDeck = new List<int>() { 1, 1, 3, 2 };
        enemyDeck = new List<int>() { 2, 1, 2, 3 };

        StartGame();
    }

    void SettingInitHand()
    {
        for (int i = 0; i < 3; i++)
        {
            GiveCardToHand(playerDeck, playerHandTransform);
            GiveCardToHand(enemyDeck, enemyHandTransform);
        }
    }

    void GiveCardToHand(List<int> deck, Transform hand)
    {
        if (deck.Count <= 0)
        {
            return;
        }

        int cardID = deck[0];
        deck.RemoveAt(0);
        CreateCard(cardID, hand);
    }

    void CreateCard(int cardID, Transform hand)
    {
        CardController card = Instantiate(cardPrefab, hand, false);
        card.Init(cardID);
    }

    void TurnCalc()
    {
        if (isPlayerTurn)
        {
            PlayerTurn();
        }
        else
        {
            EnemyTurn();
        }
    }

    public void ChangeTurn()
    {
        isPlayerTurn = !isPlayerTurn;
        if (isPlayerTurn)
        {
            playerDefaultManaCost++;
            playerManaCost = playerDefaultManaCost;
            GiveCardToHand(playerDeck, playerHandTransform);
        }
        else
        {
            enemyDefaultManaCost++;
            enemyManaCost = playerDefaultManaCost;
            GiveCardToHand(enemyDeck, enemyHandTransform);
        }
        ShowManaCost();
        TurnCalc();
    }

    void PlayerTurn()
    {
        Debug.Log("playerのターン");
        // フィールドのカードを攻撃可能状態にする
        CardController[] playerFieldCardList = playerFieldTransform.GetComponentsInChildren<CardController>();
        foreach (CardController card in playerFieldCardList)
        {
            // cardを攻撃可能状態にする
            card.SetCanAttack(true);
        }

    }

    void EnemyTurn()
    {
        Debug.Log("enemyのターン");

        // フィールドのカードリストを取得
        CardController[] enemyFieldCardList = enemyFieldTransform.GetComponentsInChildren<CardController>();
        // フィールドのカードを攻撃可能状態にする
        foreach (CardController card in enemyFieldCardList)
        {
            // cardを攻撃可能状態にする
            card.SetCanAttack(true);
        }

        /* 場にカードを出す */
        // 手札のカードリストを取得
        CardController[] handCardList = enemyHandTransform.GetComponentsInChildren<CardController>();
        // コスト以下のカードリストを取得
        CardController[] selectableHandCardList = Array.FindAll(handCardList, card => card.model.cost <= enemyManaCost);
        // 場に出すカードを選択
        if (selectableHandCardList.Length > 0)
        {
            CardController enemyCard = selectableHandCardList[0];
            enemyCard.movement.SetCardTransform(enemyFieldTransform);
            ReduceManaCost(enemyCard.model.cost, false);
            enemyCard.model.isFieldCard = true;
        }

        /* 攻撃 */
        // 攻撃可能カードを取得
        CardController[] enemyCanAttackCardList = Array.FindAll(enemyFieldCardList, card => card.model.canAttack);
        CardController[] playerFieldCardList = playerFieldTransform.GetComponentsInChildren<CardController>();
        if (enemyCanAttackCardList.Length > 0)
        {
            // 攻撃するカード(attacker)を選択
            CardController attacker = enemyCanAttackCardList[0];

            if (playerFieldCardList.Length > 0)
            {
                // 被攻撃カード(defender)を選択(Playerフィールドから選択)
                CardController defender = playerFieldCardList[0];
                // attackerとdefenderを戦わせる
                CardsBattle(attacker, defender);
            }
            else
            {
                AttackToHero(attacker, false);
            }
        }

        ChangeTurn();
    }

    public void CardsBattle(CardController attacker, CardController defender)
    {
        Debug.Log("CardsBattle");
        attacker.Attack(defender);
        defender.Attack(attacker);
        attacker.CheckAlive();
        defender.CheckAlive();
    }

    void ShowHeroHp()
    {
        playerHeroHpText.text = playerHeroHp.ToString();
        enemyHeroHpText.text = enemyHeroHp.ToString();
    }

    void ShowManaCost()
    {
        playerManaCostText.text = playerManaCost.ToString();
        enemyManaCostText.text = enemyManaCost.ToString();
    }

    public void ReduceManaCost(int cost, bool isPlayerCard)
    {
        if (isPlayerCard)
        {
            playerManaCost -= cost;
        }
        else
        {
            enemyManaCost -= cost;
        }
        ShowManaCost();
    }

    public void AttackToHero(CardController attacker, bool isPlayerCard)
    {
        if (isPlayerCard)
        {
            enemyHeroHp -= attacker.model.at;
        }
        else
        {
            playerHeroHp -= attacker.model.at;
        }
        attacker.SetCanAttack(false);
        ShowHeroHp();
        CheckHeroHp();
    }

    void CheckHeroHp()
    {
        if (playerHeroHp <= 0)
        {
            resultPanel.SetActive(true);
            resultText.text = "LOSE";
        }
        if (enemyHeroHp <= 0)
        {
            resultPanel.SetActive(true);
            resultText.text = "WIN";
        }
    }
}
