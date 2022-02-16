using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFactory : MonoBehaviour
{
    public GameObject ball;
    int _ballNum;
    public int BallNum
    {
        get { return _ballNum; }
        set
        {
            _ballNum = value;
        }
    }

    void Start()
    {
        GameObject paddle = GameObject.Find("Paddle");
        Vector3 position = paddle.transform.position;
        Vector3 speed = new Vector3(200.0f, 0, Random.Range(800f, 880f));
        _ballNum = 0;

        SpawnBall(position, speed);
    }

    void SpawnBall(Vector3 position, Vector3 speed)
    {
        GameObject newBall = Instantiate(ball, position, transform.rotation);
        _ballNum++;

        Rigidbody _rigidbody = newBall.GetComponent<Rigidbody>();
        _rigidbody.AddForce(speed);
    }

    void Update()
    {

    }
}
