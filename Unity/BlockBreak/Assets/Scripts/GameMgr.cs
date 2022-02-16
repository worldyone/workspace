using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    GameObject blockFactory;
    BlockFactory blockFactoryComponent;
    GameObject ballFactory;
    BallFactory ballFactoryComponent;

    void Start()
    {
        blockFactory = GameObject.Find("BlockFactory");
        blockFactoryComponent = blockFactory.GetComponent<BlockFactory>();

        ballFactory = GameObject.Find("BallFactory");
        ballFactoryComponent = ballFactory.GetComponent<BallFactory>();

        blockFactoryComponent.MakeStage2();
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
}
