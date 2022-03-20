using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Global;

public class GameMgr : MonoBehaviour
{
    public GameObject element;
    public GameObject gotPanel;
    BlockFactory blockFactory;
    PanelFactory panelFactory;
    List<GameObject> displayedPanels;

    // グローバル
    static BallFactory ballFactory;
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
            SceneManager.LoadScene("GameOver");
        }

        // パネル表示
        displayGotPanel();

    }

    public static void getPanel(PanelAttribute attribute)
    {
        PanelAttributes.Add(attribute);

        // Panelが3つ揃った時、3つのパターンによって効果を発揮して、取得Panelを削除する
        if (PanelAttributes.Count >= 3)
        {
            List<PanelAttribute> pa = PanelAttributes;
            exerciseByPanelPattern(pa);
            PanelAttributes.Clear();
        }
    }

    private static void exerciseByPanelPattern(List<PanelAttribute> panelAttributes)
    {
        List<PanelAttribute> pa = panelAttributes;
        pa.Sort(); // Fire, Water, Earthの順番でソートされる
        // PanelAttribute.Fire  : 0
        // PanelAttribute.Water : 1
        // PanelAttribute.Earth : 2

        if (pa[0] == PanelAttribute.Fire
            && pa[1] == PanelAttribute.Water
            && pa[2] == PanelAttribute.Earth)
        {
            Paddle paddle = GameObject.Find("Paddle").GetComponent<Paddle>();
            paddle.Size += 5.0f;
            paddle.Speed += 10.0f;
            Debug.Log("Power Up!!");
        }
        else if (pa[0] == PanelAttribute.Fire
            && pa[1] == PanelAttribute.Fire
            && pa[2] == PanelAttribute.Water)
        {

        }
        else
        {
            Paddle paddle = GameObject.Find("Paddle").GetComponent<Paddle>();
            Vector3 position = paddle.transform.position;
            Vector3 speed = new Vector3(200.0f, 0, UnityEngine.Random.Range(800f, 880f));
            ballFactory.SpawnBall(position, speed);
        }
    }

    void displayGotPanel()
    {
        for (int i = 0; i < PanelAttributes.Count; i++)
        {
            coloredGotPanel(displayedPanels[i], PanelAttributes[i]);
        }
        for (int i = 0; i < displayedPanels.Count - PanelAttributes.Count; i++)
        {
            coloredGotPanel(displayedPanels[displayedPanels.Count - 1 - i], null);
        }
    }

    private void coloredGotPanel(GameObject gameObject, PanelAttribute? panelAttribute)
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
            default:
                // 白色に変更する
                gameObject.GetComponent<Renderer>().material.color = Color.white;
                break;
        }
    }
}
