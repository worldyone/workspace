using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class QuestManager : MonoBehaviour
{
    public StageUIManager stageUI;
    public GameObject enemyPrefab;
    public BattleManager battleManager;
    public SceneTransitionManager sceneTransitionManager;
    public GameObject questBG;

    // 敵に遭遇するテーブル：-1なら遭遇しない, 0なら遭遇
    int[] encounterTable = { -1, -1, 0, -1, 0, -1, -1, -1, -1 };
    int currentStage = 0;

    void Start()
    {
        stageUI.UpdateUI(currentStage);
    }

    IEnumerator Explore()
    {
        // 背景を大きくする
        questBG.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 2f)
            .OnComplete(() => questBG.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f));
        // フェードアウト
        SpriteRenderer questBGSpriteRenderer = questBG.GetComponent<SpriteRenderer>();
        questBGSpriteRenderer.DOFade(0, 2f)
            .OnComplete(() => questBGSpriteRenderer.DOFade(1, 0f));

        // 演出が終わるまで2秒間待機
        yield return new WaitForSeconds(2f);

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
        else
        {
            stageUI.ShowButtons(true);
        }
    }

    public void OnNextButton()
    {
        SoundManager.instance.PlaySE(0);
        stageUI.ShowButtons(false);
        StartCoroutine(Explore());
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
