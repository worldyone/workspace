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
    public bool isPlayerTurn;
    List<int> playerDeck = new List<int>() { 1, 2, 3, 2 };
    List<int> enemyDeck = new List<int>() { 2, 1, 2, 3 };
    int playerHeroHp;
    int enemyHeroHp;
    [SerializeField] Transform playerHero;
    [SerializeField] Text playerHeroHpText;
    [SerializeField] Text enemyHeroHpText;
    public int playerManaCost;
    int playerDefaultManaCost;
    int enemyManaCost;
    int enemyDefaultManaCost;
    [SerializeField] Text playerManaCostText;
    [SerializeField] Text enemyManaCostText;

    // 時間管理
    [SerializeField] Text timeCountText;
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
        resultPanel.SetActive(false);
        playerHeroHp = 5;
        enemyHeroHp = 5;
        playerManaCost = playerDefaultManaCost = 10;
        enemyManaCost = enemyDefaultManaCost = 10;
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
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator CountDown()
    {
        timeCount = 20;
        timeCountText.text = timeCount.ToString();

        while (timeCount > 0)
        {
            yield return new WaitForSeconds(1);
            timeCount--;
            timeCountText.text = timeCount.ToString();
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
        ShowManaCost();
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

    IEnumerator EnemyTurn()
    {
        Debug.Log("enemyのターン");

        // フィールドのカードを攻撃可能状態にする
        CardController[] enemyFieldCardList = enemyFieldTransform.GetComponentsInChildren<CardController>();
        SettingCanAttackView(enemyFieldCardList, true);

        yield return new WaitForSeconds(1);

        /* 場にカードを出す */
        // 手札のカードリストを取得
        CardController[] handCardList = enemyHandTransform.GetComponentsInChildren<CardController>();

        // コスト以下のカードがあれば、カードをフィールドに出し続ける
        while (Array.Exists(handCardList, card => card.model.cost <= enemyManaCost))
        {
            // コスト以下のカードリストを取得
            CardController[] selectableHandCardList = Array.FindAll(handCardList, card => card.model.cost <= enemyManaCost);
            // 場に出すカードを選択
            CardController enemyCard = selectableHandCardList[0];
            StartCoroutine(enemyCard.movement.MoveToField(enemyFieldTransform));
            enemyCard.OnField(false);

            // 手札を更新
            handCardList = enemyHandTransform.GetComponentsInChildren<CardController>();

            yield return new WaitForSeconds(1);
        }

        yield return new WaitForSeconds(1);

        /* 攻撃 */
        // フィールドのカードリストを取得
        CardController[] fieldCardList = enemyFieldTransform.GetComponentsInChildren<CardController>();

        // 攻撃可能カードがあれば攻撃を繰り返す
        while (Array.Exists(fieldCardList, card => card.model.canAttack))
        {
            // 攻撃可能カードを取得
            CardController[] enemyCanAttackCardList = Array.FindAll(fieldCardList, card => card.model.canAttack);
            CardController[] playerFieldCardList = playerFieldTransform.GetComponentsInChildren<CardController>();
            // 攻撃するカード(attacker)を選択
            CardController attacker = enemyCanAttackCardList[0];

            if (playerFieldCardList.Length > 0)
            {
                // 被攻撃カード(defender)を選択(Playerフィールドから選択)
                // シールドカードがあるならシールドカードのみ攻撃対象にする
                if (Array.Exists(playerFieldCardList, card => card.model.ability == ABILITY.SHIELD))
                {
                    playerFieldCardList = Array.FindAll(playerFieldCardList, card => card.model.ability == ABILITY.SHIELD);
                }
                CardController defender = playerFieldCardList[0];
                // attackerとdefenderを戦わせる
                StartCoroutine(attacker.movement.MoveToTarget(defender.transform));
                yield return new WaitForSeconds(0.51f);
                CardsBattle(attacker, defender);
            }
            else
            {
                StartCoroutine(attacker.movement.MoveToTarget(playerHero.transform));
                yield return new WaitForSeconds(0.25f);
                AttackToHero(attacker, false);
                yield return new WaitForSeconds(0.25f);
            }

            // フィールドカードの再取得
            fieldCardList = enemyFieldTransform.GetComponentsInChildren<CardController>();

            yield return new WaitForSeconds(1);
        }

        yield return new WaitForSeconds(1);
        ChangeTurn();
    }

    void SettingCanAttackView(CardController[] fieldCardList, bool canAttack)
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
            resultPanel.SetActive(true);
            resultText.text = "LOSE";
        }
        else
        {
            resultPanel.SetActive(true);
            resultText.text = "WIN";
        }

    }
}
