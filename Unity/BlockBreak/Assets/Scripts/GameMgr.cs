using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Global;

public class GameMgr : MonoBehaviour
{
    public GameObject element;
    public GameObject gotPanel;
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
        displayedPanels = new List<GameObject>();
        displayedPanels.Add(Instantiate(gotPanel, DISPLAY_ELEMENT_POSITION, transform.rotation));
        displayedPanels.Add(Instantiate(gotPanel, DISPLAY_ELEMENT_POSITION + new Vector3(0f, 0f, 5f), transform.rotation));
        displayedPanels.Add(Instantiate(gotPanel, DISPLAY_ELEMENT_POSITION + new Vector3(0f, 0f, 10f), transform.rotation));

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

        // 上限3つとするため、配列の2, 3, 4の要素を1, 2, 3に移す
        if (PanelAttributes.Count > 3)
        {
            PanelAttributes = PanelAttributes.GetRange(1, 3);
        }
    }

    void displayGotPanel()
    {
        for (int i = 0; i < PanelAttributes.Count; i++)
        {
            coloredGotPanel(displayedPanels[i], PanelAttributes[i]);
        }
    }

    private void coloredGotPanel(GameObject gameObject, PanelAttribute panelAttribute)
    {
        switch (panelAttribute)
        {
            case PanelAttribute.Fire:
                // 赤色に変更する
                gameObject.GetComponent<Renderer>().material.color = Color.red;
                break;
            case PanelAttribute.Water:
                // 水色に変更する
                gameObject.GetComponent<Renderer>().material.color = Color.blue;
                break;
            case PanelAttribute.Earth:
                // 黄色に変更する
                gameObject.GetComponent<Renderer>().material.color = Color.yellow;
                break;
        }
    }
}
