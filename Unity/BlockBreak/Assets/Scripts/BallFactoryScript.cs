using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFactoryScript : MonoBehaviour
{
    public GameObject ball;

    void Start()
    {
        InvokeRepeating("SpawnBall", 0f, 10f);
    }

    void SpawnBall()
    {
        Instantiate(ball, new Vector3(Random.Range(-5f, 5f), transform.position.y, transform.position.z - 3), transform.rotation);
    }

    void Update()
    {

    }
}
