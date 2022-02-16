using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallScript : MonoBehaviour
{
    private Vector3 _speed;
    Rigidbody _Rigidbody;

    void Start()
    {
        // _speed = new Vector3(Random.Range(-25f, 25f), 0, Random.Range(300f, 360f));
        // _Rigidbody = GetComponent<Rigidbody>();
        // _Rigidbody.AddForce(_speed);
    }

    void Update()
    {
        if (transform.position.z < Global.OUT_OF_FIELD_Z)
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }

    // ぶつかったら跳ね返る
    private void OnCollisionEnter(Collision collision)
    {
        string name = LayerMask.LayerToName(collision.gameObject.layer);

        // 上辺・パドルとぶつかったら縦方向座標の反転跳ね返り
        // 右辺・左辺とぶつかったら横方向座標の反転跳ね返り
        // if (name == "Paddle" || name == "WallUp")
        // {
        //     _speed = -_speed;
        // }
    }
}
