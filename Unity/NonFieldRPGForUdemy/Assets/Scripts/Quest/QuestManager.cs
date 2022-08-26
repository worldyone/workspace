using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public StageUIManager stageUI;
    public GameObject enemyPrefab;
    public BattleManager battleManager;
    public SceneTransitionManager sceneTransitionManager;

    // 敵に遭遇するテーブル：-1なら遭遇しない, 0なら遭遇
    int[] encounterTable = { -1, -1, 0, -1, 0, -1 };
    int currentStage = 0;

    void Start()
    {
        stageUI.UpdateUI(currentStage);
    }

    public void OnNextButton()
    {
        SoundManager.instance.PlaySE(0);

        currentStage++;
        stageUI.UpdateUI(currentStage);

        if (currentStage >= encounterTable.Length)
        {
            Debug.Log("クエストクリア！");
            QuestClear();
        }
        else if (encounterTable[currentStage] == 0)
        {
            Debug.Log("敵に遭遇した！");
            EncounterEnemy();
        }
    }

    public void OnToTownButton()
    {
        SoundManager.instance.PlaySE(0);
    }

    void EncounterEnemy()
    {
        SoundManager.instance.PlayBGM("Battle");
        stageUI.ShowButtons(false);
        GameObject enemyObj = Instantiate(enemyPrefab);
        EnemyManager enemy = enemyObj.GetComponent<EnemyManager>();
        battleManager.Setup(enemy);
    }

    public void EndBattle()
    {
        SoundManager.instance.PlayBGM("Quest");
        stageUI.ShowButtons(true);
    }

    void QuestClear()
    {
        // クエストクリア
        SoundManager.instance.StopBGM();
        SoundManager.instance.PlaySE(2);
        stageUI.ShowClearText();
    }
}
