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
    }

    void PlayerAttack()
    {
        player.Attack(enemy);
        enemyUI.UpdateUI(enemy);
    }

    void EnemyAttack()
    {
        enemy.Attack(player);
        playerUI.UpdateUI(player);
    }


}
