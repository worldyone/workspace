using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public StageUIManager stageUI;
    public GameObject enemyPrefab;
    public BattleManager battleManager;

    // 敵に遭遇するテーブル：-1なら遭遇しない, 0なら遭遇
    int[] encounterTable = { -1, -1, 0, -1, 0, -1 };
    int currentStage = 0;

    void Start()
    {
        stageUI.UpdateUI(currentStage);
    }

    public void OnNextButton()
    {
        currentStage++;
        stageUI.UpdateUI(currentStage);

        if (currentStage >= encounterTable.Length)
        {
            Debug.Log("クエストクリア！");
        }
        else if (encounterTable[currentStage] == 0)
        {
            Debug.Log("敵に遭遇した！");
            EncounterEnemy();
        }
    }

    void EncounterEnemy()
    {
        stageUI.ShowButtons(false);
        GameObject enemyObj = Instantiate(enemyPrefab);
        EnemyManager enemy = enemyObj.GetComponent<EnemyManager>();
        battleManager.Setup(enemy);
    }
}
