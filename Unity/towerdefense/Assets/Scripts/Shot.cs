using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : Token
{
    // ショットオブジェクト管理
    public static TokenMgr<Shot> parent;

    void Update()
    {
        if (IsOutside())
        {
            Vanish();
        }
    }

    // ショットを撃つ
    public static Shot Add(float px, float py, float direction, float speed)
    {
        return parent.Add(px, py, direction, speed);
    }
}
