using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Token
{
    float _range;

    // Start is called before the first frame update
    void Start()
    {
        _range = Field.GetChipSize() * 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        Enemy e = Enemy.parent.Nearest(this);
        if (e == null)
        {
            // 敵がいない場合は何もしない
            return;
        }

        // 敵への距離を取得する
        float dist = Util.DistanceBetween(this, e);
        if (dist > _range)
        {
            // 射程範囲外なので何もしない
            return;
        }

        // 敵への角度を取得
        float targetAngle = Util.AngleBetween(this, e);
        float dAngle = Mathf.DeltaAngle(Angle, targetAngle);
        // 差の0.2だけ回転する
        Angle += dAngle * 0.2f;
        // もう一度角度差を求める
        float dAngle2 = Mathf.DeltaAngle(Angle, targetAngle);
        if (Mathf.Abs(dAngle2) > 16)
        {
            // 角度が大きい場合は撃たない
            return;
        }

        // ショットを撃つ
    }
}
