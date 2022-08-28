using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 対戦の管理
public class BattleManager : MonoBehaviour
{
    public Transform playerDamagePanel;
    public QuestManager questManager;
    public PlayerUIManager playerUI;
    public EnemyUIManager enemyUI;
    public PlayerManager player;
    EnemyManager enemy;

    void Start()
    {
        enemyUI.gameObject.SetActive(false);
    }

    // 初期設定
    public void Setup(EnemyManager enemyManager)
    {
        enemyUI.gameObject.SetActive(true);
        enemy = enemyManager;
        playerUI.SetupUI(player);
        enemyUI.SetupUI(enemy);

        enemy.AddEventListenerOnTap(PlayerAttack);
    }

    void PlayerAttack()
    {
        StopAllCoroutines();


        SoundManager.instance.PlaySE(1);
        int damage = player.Attack(enemy);

        DialogTextManager.instance.SetScenarios(new string[] {
            "プレイヤーの攻撃！\nモンスターに" + damage + "ダメージを与えた。"
        });

        enemyUI.UpdateUI(enemy);
        if (enemy.hp <= 0)
        {
            // 撃破
            StartCoroutine(EndBattle());
        }
        else
        {
            // まだ倒していないので、敵のターン
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(2f);

        SoundManager.instance.PlaySE(1);
        playerDamagePanel.DOShakePosition(0.3f, 0.5f, 20, 0, false, true);
        int damage = enemy.Attack(player);
        playerUI.UpdateUI(player);

        DialogTextManager.instance.SetScenarios(new string[] {
            "モンスターの攻撃！\nプレイヤーは" + damage + "ダメージを受けた。"
        });

        if (player.hp <= 0)
        {
            yield return new WaitForSeconds(2f);

            // 倒されてしまった
            DialogTextManager.instance.SetScenarios(new string[] { "プレイヤーは倒れてしまった…。" });
            questManager.QuestFailure();
        }
    }

    IEnumerator EndBattle()
    {
        yield return new WaitForSeconds(2f);

        DialogTextManager.instance.SetScenarios(new string[] {
            "モンスターは逃げていった。"
        });

        enemyUI.gameObject.SetActive(false);
        Destroy(enemy.gameObject);
        questManager.EndBattle();
    }
}
