using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour
{
    private float _speed;
    public float Speed
    {
        get { return _speed; }
    }
    private float _size;
    public float Size
    {
        get { return _size; }
    }
    private Vector3 _position;
    public Vector3 Position
    {
        get { return _position; }
    }

    void Start()
    {
        _speed = 10.0f;
        _size = 5.0f;
        _position = transform.position;

        transform.localScale = new Vector3(_size, transform.localScale.y, transform.localScale.z);
    }

    void Update()
    {
        _position = transform.position;
        float moveX = Input.GetAxis("Horizontal") * Time.deltaTime * _speed;
        float dx = transform.position.x + moveX;

        // パドルがフィールドの外にまで動けないようにする
        dx = Mathf.Max(dx, -5.0f + _size / 2.0f);
        dx = Mathf.Min(dx, 5.0f - _size / 2.0f);
        transform.position = new Vector3(dx, transform.position.y, transform.position.z);
    }
}