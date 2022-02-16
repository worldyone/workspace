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
    }

    void Start()
    {
        _blockNum = 10;

        for (int i = 0; i < _blockNum; i++)
        {
            Instantiate(block, new Vector3(-10.0f + i * 2, 1f, 20f), transform.rotation);
        }
    }

    void Update()
    {

    }
}
