using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    GameObject blockFactory;
    BlockFactory blockFactoryComponent;

    void Start()
    {
        blockFactory = GameObject.Find("BlockFactory");
        blockFactoryComponent = blockFactory.GetComponent<BlockFactory>();
    }

    /// ぶつかり処理
    // ぶつかるのはボールとのみを想定している
    // ステージのブロック総数を1つ減らし、ブロックが消える.
    void OnCollisionEnter(Collision other)
    {
        blockFactoryComponent.BlockNum--;

        Destroy(gameObject);
    }
}
