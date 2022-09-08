using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] AI enemyAI;
    [SerializeField] UIManager uiManager;
    [SerializeField] public Transform playerHandTransform;
    [SerializeField] public Transform enemyHandTransform;
    [SerializeField] public Transform playerFieldTransform;
    [SerializeField] public Transform enemyFieldTransform;
    [SerializeField] CardController cardPrefab;
    public bool isPlayerTurn;
    List<int> playerDeck = new List<int>() { 1, 2, 3, 2 };
    List<int> enemyDeck = new List<int>() { 2, 1, 2, 3 };
    int playerHeroHp;
    int enemyHeroHp;
    [SerializeField] public Transform playerHero;
    public int playerManaCost;
    int playerDefaultManaCost;
    public int enemyManaCost;
    int enemyDefaultManaCost;

    int timeCount;

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
        uiManager.ShowResultPanel(false);
        playerHeroHp = 5;
        enemyHeroHp = 5;
        playerManaCost = playerDefaultManaCost = 10;
        enemyManaCost = enemyDefaultManaCost = 10;
        uiManager.ShowHeroHp(playerHeroHp, enemyHeroHp);
        uiManager.ShowManaCost(playerManaCost, enemyManaCost);
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
        card.Init(cardID, hand.name == "PlayerHand");
    }

    void TurnCalc()
    {
        StopAllCoroutines();
        StartCoroutine(CountDown());

        if (isPlayerTurn)
        {
            PlayerTurn();
        }
        else
        {
            Debug.Log("enemyのターン");
            StartCoroutine(enemyAI.EnemyTurn());
        }
    }

    IEnumerator CountDown()
    {
        timeCount = 20;
        uiManager.UpdateTime(timeCount);

        while (timeCount > 0)
        {
            yield return new WaitForSeconds(1);
            timeCount--;
            uiManager.UpdateTime(timeCount);
        }

        ChangeTurn();
    }

    public void OnClickTurnEndButton()
    {
        if (isPlayerTurn)
        {
            ChangeTurn();
        }
    }

    public void ChangeTurn()
    {
        isPlayerTurn = !isPlayerTurn;

        // ターン変更時のため、一旦全フィールドカードを攻撃不能にする
        CardController[] playerFieldCardList = playerFieldTransform.GetComponentsInChildren<CardController>();
        SettingCanAttackView(playerFieldCardList, false);
        CardController[] enemyFieldCardList = enemyFieldTransform.GetComponentsInChildren<CardController>();
        SettingCanAttackView(enemyFieldCardList, false);

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
        uiManager.ShowManaCost(playerManaCost, enemyManaCost);
        TurnCalc();
    }

    public CardController[] GetEnemyFieldCards()
    {
        return enemyFieldTransform.GetComponentsInChildren<CardController>();
    }

    void PlayerTurn()
    {
        Debug.Log("playerのターン");
        // フィールドのカードを攻撃可能状態にする
        CardController[] playerFieldCardList = playerFieldTransform.GetComponentsInChildren<CardController>();
        SettingCanAttackView(playerFieldCardList, true);
    }

    public void SettingCanAttackView(CardController[] fieldCardList, bool canAttack)
    {
        foreach (CardController card in fieldCardList)
        {
            card.SetCanAttack(canAttack);
        }
    }

    public void CardsBattle(CardController attacker, CardController defender)
    {
        Debug.Log("CardsBattle");
        attacker.Attack(defender);
        defender.Attack(attacker);
        attacker.CheckAlive();
        defender.CheckAlive();
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
        uiManager.ShowManaCost(playerManaCost, enemyManaCost);
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
        uiManager.ShowHeroHp(playerHeroHp, enemyHeroHp);
        CheckHeroHp();
    }

    public void CheckHeroHp()
    {
        if (playerHeroHp <= 0 || enemyHeroHp <= 0)
        {
            ShowResultPanel(playerHeroHp);
        }
    }

    void ShowResultPanel(int heroHp)
    {
        StopAllCoroutines();
        if (heroHp <= 0)
        {
            uiManager.ShowResult("LOSE");
        }
        else
        {
            uiManager.ShowResult("WIN");
        }
    }
}
