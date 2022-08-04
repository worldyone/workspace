using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public UnitManager player;
    public UnitManager enemy;

    // Start is called before the first frame update
    void Start()
    {
        // player.Attack(enemy);
        // enemy.Attack(player);
    }

    public void OnAttackButton()
    {
        player.Attack(enemy);
        EnemyTurn();
    }

    void EnemyTurn()
    {
        enemy.Attack(player);
    }

}
