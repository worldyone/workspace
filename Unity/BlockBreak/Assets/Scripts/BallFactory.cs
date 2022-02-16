using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFactory : MonoBehaviour
{
    public GameObject ball;

    void Start()
    {
        GameObject paddle = GameObject.Find("Paddle");
        Vector3 position = paddle.transform.position;
        Vector3 speed = new Vector3(200.0f, 0, Random.Range(800f, 880f));
        SpawnBall(position, speed);
    }

    void SpawnBall(Vector3 position, Vector3 speed)
    {
        GameObject newBall = Instantiate(ball, position, transform.rotation);
        Rigidbody _rigidbody = newBall.GetComponent<Rigidbody>();
        _rigidbody.AddForce(speed);
    }

    void Update()
    {

    }
}
