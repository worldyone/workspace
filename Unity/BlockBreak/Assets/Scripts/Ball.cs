using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    private Vector3 _speed;
    Rigidbody _Rigidbody;

    void Start()
    {
    }

    void Update()
    {
        if (transform.position.z < Global.OUT_OF_FIELD)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    // ぶつかったら跳ね返る
    private void OnCollisionEnter(Collision collision)
    {
        string name = LayerMask.LayerToName(collision.gameObject.layer);

    }
}
