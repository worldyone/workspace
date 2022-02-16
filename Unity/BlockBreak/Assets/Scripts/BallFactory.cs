using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFactoryScript : MonoBehaviour
{
    public GameObject ball;

    void Start()
    {
        GameObject paddle = GameObject.Find("Paddle");
        Vector3 position = paddle.transform.position;
        Vector3 speed = new Vector3(200.0f, 0, Random.Range(600f, 660f));
        SpawnBall(position, speed);
    }

    void SpawnBall(Vector3 position, Vector3 speed)
    {
        GameObject newBall = Instantiate(ball, position, transform.rotation);
        Rigidbody _rigidbody = newBall.GetComponent<Rigidbody>();
        _rigidbody.AddForce(speed);
        // Instantiate(ball, new Vector3(Random.Range(-5f, 5f), transform.position.y, transform.position.z - 3), transform.rotation);
    }

    void Update()
    {

    }
}
