using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform playerHandTransform;
    [SerializeField] Transform enemyHandTransform;
    [SerializeField] Transform playerFieldTransform;
    [SerializeField] Transform enemyFieldTransform;
    [SerializeField] CardController cardPrefab;
    bool isPlayerTurn;

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
        SettingInitHand();
        isPlayerTurn = true;
        TurnCalc();
    }

    void SettingInitHand()
    {
        for (int i = 0; i < 3; i++)
        {
            CreateCard(playerHandTransform);
            CreateCard(enemyHandTransform);
        }
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
            CreateCard(playerHandTransform);
        }
        else
        {
            CreateCard(enemyHandTransform);
        }
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

        /* 場にカードを出す */
        // 手札のカードリストを取得
        CardController[] handCardList = enemyHandTransform.GetComponentsInChildren<CardController>();
        // 場に出すカードを選択
        CardController enemyCard = handCardList[0];
        // カードを移動
        enemyCard.movement.SetCardTransform(enemyFieldTransform);

        /* 攻撃 */
        // フィールドのカードリストを取得
        CardController[] fieldCardList = enemyFieldTransform.GetComponentsInChildren<CardController>();
        // 攻撃可能カードを取得
        CardController[] enemyCanAttackCardList = Array.FindAll(fieldCardList, card => card.model.canAttack);
        CardController[] playerFieldCardList = playerFieldTransform.GetComponentsInChildren<CardController>();
        if (enemyCanAttackCardList.Length > 0 && playerFieldCardList.Length > 0)
        {
            // 攻撃するカード(attacker)を選択
            CardController attacker = enemyCanAttackCardList[0];

            // 被攻撃カード(defender)を選択(Playerフィールドから選択)
            CardController defender = playerFieldCardList[0];

            // attackerとdefenderを戦わせる
            CardsBattle(attacker, defender);
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

    void CreateCard(Transform hand)
    {
        CardController card = Instantiate(cardPrefab, hand, false);
        card.Init(2);
    }

}
