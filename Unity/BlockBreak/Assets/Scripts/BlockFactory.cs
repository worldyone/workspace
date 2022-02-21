using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFactory : MonoBehaviour
{
    public GameObject block;
    GameObject panelFactory;
    PanelFactory panelFactoryComponent;
    int _blockNum;
    public int BlockNum
    {
        get { return _blockNum; }
        set
        {
            _blockNum = value;
        }
    }

    void Start()
    {
        panelFactory = GameObject.Find("PanelFactory");
        panelFactoryComponent = panelFactory.GetComponent<PanelFactory>();
    }

    internal void addPanel(Vector3 position)
    {
        panelFactoryComponent.addPanel(position);
    }

    public void BreakBlock()
    {
        _blockNum--;
    }

    public void MakeStage1()
    {
        _blockNum = 30;

        for (int h = 0; h < _blockNum / 10; h++)
        {
            for (int w = 0; w < _blockNum / 3; w++)
            {
                Instantiate(block, new Vector3(-10.0f + w * 2, 1f, 12f + h * 2), transform.rotation);
            }
        }
    }

    public void MakeStage2()
    {
        _blockNum = 3;

        for (int w = 0; w < _blockNum; w++)
        {
            Instantiate(block, new Vector3(-10.0f + w * 2, 1f, 12f), transform.rotation);
        }
    }
}
