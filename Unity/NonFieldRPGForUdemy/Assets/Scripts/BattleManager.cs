using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 対戦の管理
public class BattleManager : MonoBehaviour
{
    public PlayerUIManager playerUI;
    public EnemyUIManager enemyUI;
    public PlayerManager player;
    EnemyManager enemy;

    // 初期設定
    public void Setup(EnemyManager enemyManager)
    {
        enemy = enemyManager;
        playerUI.SetupUI(player);
        enemyUI.SetupUI(enemy);

        enemy.AddEventListenerOnTap(PlayerAttack);
    }

    void PlayerAttack()
    {
        player.Attack(enemy);
        enemyUI.UpdateUI(enemy);
        if (enemy.hp <= 0)
        {
            // 撃破
            Destroy(enemy.gameObject);
            EndBattle();
        }
        else
        {
            // まだ倒していないので、敵のターン
            EnemyTurn();
        }
    }

    void EnemyTurn()
    {
        enemy.Attack(player);
        playerUI.UpdateUI(player);
    }

    void EndBattle()
    {
        Debug.Log("EndBattle");
    }

}
