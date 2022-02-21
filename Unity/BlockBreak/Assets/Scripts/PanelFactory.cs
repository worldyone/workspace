
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelFactory : MonoBehaviour
{
    public GameObject panel;
    Vector3 speed;

    void Start()
    {
        speed = new Vector3(0.0f, 0.0f, -800.0f);
    }

    internal void addPanel(Vector3 position)
    {
        GameObject newPanel = Instantiate(panel, position, transform.rotation);

        Rigidbody _rigidbody = newPanel.GetComponent<Rigidbody>();
        _rigidbody.AddForce(speed);
    }
}
