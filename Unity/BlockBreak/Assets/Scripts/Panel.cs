using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{

    void Update()
    {
        // 画面外なら削除
        if (transform.position.z <= Global.OUT_OF_FIELD)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        string name = LayerMask.LayerToName(collision.gameObject.layer);

        // パドルと当たったら、取得処理
        if (name == "Paddle")
        {
            Destroy(gameObject);
        }

    }

}
