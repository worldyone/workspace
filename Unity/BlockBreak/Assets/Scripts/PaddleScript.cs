using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour
{
    private float _speed;
    float Speed
    {
        get { return _speed; }
    }
    private float _size;
    float Size
    {
        get { return _size; }
    }

    void Start()
    {
        _speed = 10.0f;
        _size = 5.0f;

        transform.localScale = new Vector3(_size, transform.localScale.y, transform.localScale.z);
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal") * Time.deltaTime * _speed;
        float dx = transform.position.x + moveX;

        // パドルがフィールドの外にまで動けないようにする
        dx = Mathf.Max(dx, -5.0f + _size / 2.0f);
        dx = Mathf.Min(dx, 5.0f - _size / 2.0f);
        transform.position = new Vector3(dx, transform.position.y, transform.position.z);
    }
}
