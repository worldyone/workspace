using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    /// ぶつかり処理
    // ぶつかるのはボールとのみ
    // ステージのブロック総数を1つ減らし、ブロックが消える.
    void OnCollisionEnter(Collision other)
    {
        GameObject factory = GameObject.Find("BlockFactory");
        BlockFactory factoryComponent = factory.GetComponent<BlockFactory>();
        factoryComponent.BlockNum--;

        Destroy(gameObject);
    }
}
