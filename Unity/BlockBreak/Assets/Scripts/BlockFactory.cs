using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFactory : MonoBehaviour
{
    public GameObject block;
    private int _blockNum;
    public int BlockNum
    {
        get { return _blockNum; }
        set
        {
            _blockNum = value;
        }
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

    public void BreakBlock()
    {
        _blockNum--;
    }
}
