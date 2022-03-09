using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Global;

public class GameMgr : MonoBehaviour
{
    public GameObject element;
    BlockFactory blockFactory;
    BallFactory ballFactory;
    PanelFactory panelFactory;
    List<GameObject> displayedPanels;

    // グローバル
    static List<PanelAttribute> PanelAttributes;

    void Start()
    {
        // 初期化
        PanelAttributes = new List<PanelAttribute>();
        blockFactory = GameObject.Find("BlockFactory").GetComponent<BlockFactory>();
        ballFactory = GameObject.Find("BallFactory").GetComponent<BallFactory>();
        panelFactory = GameObject.Find("PanelFactory").GetComponent<PanelFactory>();

        // ステージ生成
        blockFactory.MakeStage1();
    }

    void Update()
    {
        // ゲームクリア・ゲームオーバー条件
        if (blockFactory.BlockNum <= 0)
        {
            Debug.Log("Game Clear!");
        }
        if (ballFactory.BallNum <= 0)
        {
            Debug.Log("Game Over");
        }

        // パネル表示
        displayGotPanel();

    }

    public static void getPanel(PanelAttribute attribute)
    {
        PanelAttributes.Add(attribute);
    }

    void displayGotPanel()
    {
        Vector3 position = DISPLAY_ELEMENT_POSITION;
        displayedPanels = new List<GameObject>();

        foreach (PanelAttribute e in PanelAttributes)
        {
            // GameObject panel = panelFactory.add(position);

            // displayedPanels.Add(panelFactory.add(position));
            // position += new Vector3(0f, 0f, 5f);
        }
    }
}
