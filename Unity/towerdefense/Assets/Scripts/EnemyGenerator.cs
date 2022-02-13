using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator
{
    /// メンバ変数を定義
    // 敵の移動経路
    List<Vec2D> _pathList;

    // 敵生成のインターバル
    float _interval;
    float _tInterval;
    // 同一Wave内で敵を生成する数
    int _number;
    public int Number
    {
        get { return _number; }
    }

    /// コンストラクタ
    public EnemyGenerator(List<Vec2D> pathList)
    {
        _pathList = pathList;
    }

    public void Start(int nWave)
    {
        // 出現間隔
        _interval = 1.5f; // 1.5秒
        _tInterval = 0;
        // 出現数
        _number = 5 + nWave;
    }

    public void Update()
    {
        if (_number <= 0)
        {
            // すべての敵が出現したので何もしない
            return;
        }

        // 経過時間を足し込む
        _tInterval += Time.deltaTime;
        if (_tInterval >= _interval)
        {
            // インターバルを超えたので敵出現
            _tInterval -= _interval;
            // 敵を生成
            Enemy.Add(_pathList);
            // 敵生成カウンタを減らす
            _number--;
        }
    }
}
