using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    GameObject ballFactory;
    BallFactory ballFactoryComponent;
    Vector3 _speed;
    Rigidbody _Rigidbody;

    void Start()
    {
        ballFactory = GameObject.Find("BallFactory");
        ballFactoryComponent = ballFactory.GetComponent<BallFactory>();
    }

    void Update()
    {
        if (transform.position.z < Global.OUT_OF_FIELD)
        {
            // ボールの総数を1つ減らす
            ballFactoryComponent.BallNum--;

            // ボールを削除
            Destroy(gameObject);

            // もしボールの上下方向が0になってしまっているなら、少し下方向に追加修正
            // todo:
        }
    }

    // ぶつかったら跳ね返る
    void OnCollisionEnter(Collision collision)
    {
        string name = LayerMask.LayerToName(collision.gameObject.layer);
    }
}
