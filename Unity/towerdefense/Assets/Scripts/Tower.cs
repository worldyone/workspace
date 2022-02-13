using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Token
{
    // タワー管理
    public static TokenMgr<Tower> parent;

    // ショットの速度
    const float SHOT_SPEED = 5.0f;
    // 射程範囲
    float _range;
    // 連射速度
    float _firerate;
    // 連射速度インターバルタイマー
    float _tFirerate;

    // void Start()
    void Init()
    {
        _range = Field.GetChipSize() * 1.5f;
        _firerate = 2.0f; // 2sec
        _tFirerate = 0; // 連射速度インターバル初期化
    }

    // Update is called once per frame
    void Update()
    {
        // インターバルタイマー更新
        _tFirerate += Time.deltaTime;

        // 一番近い敵を求める
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

        // インターバルチェック
        if (_tFirerate < _firerate)
        {
            // インターバル中（連射抑制）
            return;
        }

        // ショットを撃つ
        Shot.Add(X, Y, Angle, SHOT_SPEED);
        _tFirerate = 0;
    }

    // タワー生成
    public static Tower Add(float px, float py)
    {
        Tower t = parent.Add(px, py);
        if (t == null)
        {
            return null;
        }
        t.Init();
        return t;
    }
}
