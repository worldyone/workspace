using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Global;

public class GameMgr : MonoBehaviour
{
    GameObject blockFactory;
    BlockFactory blockFactoryComponent;
    GameObject ballFactory;
    BallFactory ballFactoryComponent;

    // グローバル
    static List<PanelAttribute> PanelAttributes;

    void Start()
    {
        // 初期化
        PanelAttributes = new List<PanelAttribute>();
        blockFactory = GameObject.Find("BlockFactory");
        blockFactoryComponent = blockFactory.GetComponent<BlockFactory>();
        ballFactory = GameObject.Find("BallFactory");
        ballFactoryComponent = ballFactory.GetComponent<BallFactory>();

        // ステージ生成
        blockFactoryComponent.MakeStage1();
    }

    void Update()
    {
        if (blockFactoryComponent.BlockNum <= 0)
        {
            Debug.Log("Game Clear!");
        }

        if (ballFactoryComponent.BallNum <= 0)
        {
            Debug.Log("Game Over");
        }
    }

    public static void getPanel(PanelAttribute attribute)
    {
        PanelAttributes.Add(attribute);
        Debug.Log(PanelAttributes);
        Debug.Log(PanelAttributes.Count);
    }
}
