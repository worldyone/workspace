using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (enemy.hp > 0)
        {
            EnemyTurn();
        }
        else
        {
            BattleEnd();
        }
    }

    void EnemyTurn()
    {
        enemy.Attack(player);
        if (player.hp <= 0) BattleEnd();
    }

    void BattleEnd()
    {
        Debug.Log("対戦終了");
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

}
