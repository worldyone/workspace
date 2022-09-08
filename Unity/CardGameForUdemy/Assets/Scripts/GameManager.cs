using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] AI enemyAI;
    [SerializeField] UIManager uiManager;
    [SerializeField] public GamePlayerManager player;
    [SerializeField] public GamePlayerManager enemy;
    [SerializeField] public Transform playerHandTransform;
    [SerializeField] public Transform enemyHandTransform;
    [SerializeField] public Transform playerFieldTransform;
    [SerializeField] public Transform enemyFieldTransform;
    [SerializeField] CardController cardPrefab;
    public bool isPlayerTurn;
    [SerializeField] public Transform playerHero;

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
        player.Init(new List<int>() { 1, 2, 3, 1, 2, 3 });
        enemy.Init(new List<int>() { 3, 3, 2, 2, 1, 1 });
        uiManager.ShowHeroHp(player.heroHp, enemy.heroHp);
        uiManager.ShowManaCost(player.manaCost, enemy.manaCost);
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
        player.deck = new List<int>() { 1, 1, 3, 2 };
        enemy.deck = new List<int>() { 2, 1, 2, 3 };

        StartGame();
    }

    void SettingInitHand()
    {
        for (int i = 0; i < 3; i++)
        {
            GiveCardToHand(player.deck, playerHandTransform);
            GiveCardToHand(enemy.deck, enemyHandTransform);
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
            player.IncreaseManaCost();
            GiveCardToHand(player.deck, playerHandTransform);
        }
        else
        {
            enemy.IncreaseManaCost();
            GiveCardToHand(enemy.deck, enemyHandTransform);
        }
        uiManager.ShowManaCost(player.manaCost, enemy.manaCost);
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
            player.manaCost -= cost;
        }
        else
        {
            enemy.manaCost -= cost;
        }
        uiManager.ShowManaCost(player.manaCost, enemy.manaCost);
    }

    public void AttackToHero(CardController attacker, bool isPlayerCard)
    {
        if (isPlayerCard)
        {
            enemy.heroHp -= attacker.model.at;
        }
        else
        {
            player.heroHp -= attacker.model.at;
        }
        attacker.SetCanAttack(false);
        uiManager.ShowHeroHp(player.heroHp, enemy.heroHp);
        CheckHeroHp();
    }

    public void CheckHeroHp()
    {
        if (player.heroHp <= 0 || enemy.heroHp <= 0)
        {
            ShowResultPanel(player.heroHp);
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
