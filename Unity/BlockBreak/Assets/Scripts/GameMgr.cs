using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    GameObject blockFactory;

    void Start()
    {
        blockFactory = GameObject.Find("BlockFactory");
        BlockFactory blockFactoryComponent = blockFactory.GetComponent<BlockFactory>();
        blockFactoryComponent.MakeStage1();
    }

    void Update()
    {

    }
}
