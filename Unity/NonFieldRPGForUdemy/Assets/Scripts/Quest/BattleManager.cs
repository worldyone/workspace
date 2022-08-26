using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 対戦の管理
public class BattleManager : MonoBehaviour
{
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
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1f);
        SoundManager.instance.PlaySE(1);
        enemy.Attack(player);
        playerUI.UpdateUI(player);
    }

    void EndBattle()
    {
        Debug.Log("EndBattle");
        questManager.EndBattle();
        enemyUI.gameObject.SetActive(false);
    }

}
