using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.instance;
    }

    public IEnumerator EnemyTurn()
    {


        // フィールドのカードを攻撃可能状態にする
        CardController[] enemyFieldCardList = gameManager.enemyFieldTransform.GetComponentsInChildren<CardController>();
        gameManager.SettingCanAttackView(enemyFieldCardList, true);

        yield return new WaitForSeconds(1);

        /* 場にカードを出す */
        // 手札のカードリストを取得
        CardController[] handCardList = gameManager.enemyHandTransform.GetComponentsInChildren<CardController>();

        // コスト以下のカードがあれば、カードをフィールドに出し続ける
        while (Array.Exists(handCardList, card => card.model.cost <= gameManager.enemyManaCost))
        {
            // コスト以下のカードリストを取得
            CardController[] selectableHandCardList = Array.FindAll(handCardList, card => card.model.cost <= gameManager.enemyManaCost);
            // 場に出すカードを選択
            CardController enemyCard = selectableHandCardList[0];
            StartCoroutine(enemyCard.movement.MoveToField(gameManager.enemyFieldTransform));
            enemyCard.OnField(false);

            // 手札を更新
            handCardList = gameManager.enemyHandTransform.GetComponentsInChildren<CardController>();

            yield return new WaitForSeconds(1);
        }

        yield return new WaitForSeconds(1);

        /* 攻撃 */
        // フィールドのカードリストを取得
        CardController[] fieldCardList = gameManager.enemyFieldTransform.GetComponentsInChildren<CardController>();

        // 攻撃可能カードがあれば攻撃を繰り返す
        while (Array.Exists(fieldCardList, card => card.model.canAttack))
        {
            // 攻撃可能カードを取得
            CardController[] enemyCanAttackCardList = Array.FindAll(fieldCardList, card => card.model.canAttack);
            CardController[] playerFieldCardList = gameManager.playerFieldTransform.GetComponentsInChildren<CardController>();
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
                gameManager.CardsBattle(attacker, defender);
            }
            else
            {
                StartCoroutine(attacker.movement.MoveToTarget(gameManager.playerHero.transform));
                yield return new WaitForSeconds(0.25f);
                gameManager.AttackToHero(attacker, false);
                yield return new WaitForSeconds(0.25f);
            }

            // フィールドカードの再取得
            fieldCardList = gameManager.enemyFieldTransform.GetComponentsInChildren<CardController>();

            yield return new WaitForSeconds(1);
        }

        yield return new WaitForSeconds(1);
        gameManager.ChangeTurn();
    }
}
