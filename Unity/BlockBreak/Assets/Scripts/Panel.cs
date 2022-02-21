using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Global;

public class Panel : MonoBehaviour
{
    void Start()
    {
        // 属性をランダムに付与
        int no = UnityEngine.Random.Range(0, Enum.GetValues(typeof(PanelAttribute)).Length);
        PanelAttribute attribute = (PanelAttribute)Enum.ToObject(typeof(PanelAttribute), no);

        switch (attribute)
        {
            case PanelAttribute.Fire:
                // 赤色に変更する
                gameObject.GetComponent<Renderer>().material.color = Color.red;
                break;
            case PanelAttribute.Water:
                // 水色に変更する
                gameObject.GetComponent<Renderer>().material.color = Color.blue;
                break;
            case PanelAttribute.Earth:
                // 黄色に変更する
                gameObject.GetComponent<Renderer>().material.color = Color.yellow;
                break;
        }
    }

    void Update()
    {
        // 画面外なら削除
        if (transform.position.z <= Global.OUT_OF_FIELD)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        string name = LayerMask.LayerToName(other.gameObject.layer);

        // パドルと当たったら、取得処理
        if (name == "Paddle")
        {
            Destroy(gameObject);
        }
    }

}
