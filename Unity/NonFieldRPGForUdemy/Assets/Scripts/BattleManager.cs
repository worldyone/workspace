using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 対戦の管理
public class BattleManager : MonoBehaviour
{
    public PlayerManager player;
    public EnemyManager enemy;

    void Start()
    {
        // PlayerがEnemyに攻撃
        player.Attack(enemy);

        // EnemyがPlayerに攻撃
        enemy.Attack(player);
    }
}
