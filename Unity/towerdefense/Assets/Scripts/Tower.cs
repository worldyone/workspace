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
    // 攻撃威力
    int _power;

    /// レベル
    // 射程範囲
    int _lvRange;
    public int LvRange
    {
        get { return _lvRange; }
    }

    // 連射速度
    int _lvFirerate;
    public int LvFirerate
    {
        get { return _lvFirerate; }
    }
    // 攻撃威力
    int _lvPower;
    public int LvPower
    {
        get { return _lvPower; }
    }

    /// アップグレードの種類
    public enum eUpgrade
    {
        Range, // 射程範囲
        Firerate, // 連射速度
        Power, // 攻撃威力
    }

    /// コスト
    // 射程範囲
    public int CostRange
    {
        get { return Cost.TowerUpgrade(eUpgrade.Range, _lvRange); }
    }

    // 連射速度
    public int CostFirerate
    {
        get { return Cost.TowerUpgrade(eUpgrade.Firerate, _lvFirerate); }
    }

    // 攻撃威力
    public int CostPower
    {
        get { return Cost.TowerUpgrade(eUpgrade.Power, _lvPower); }
    }

    /// アップグレード種別に対応したコストを取得する
    public int GetCost(eUpgrade type)
    {
        switch (type)
        {
            case eUpgrade.Range: return CostRange;
            case eUpgrade.Firerate: return CostFirerate;
            case eUpgrade.Power: return CostPower;
        }
        return 0;
    }

    // void Start()
    void Init()
    {
        // レベル初期化
        _lvRange = 1;
        _lvFirerate = 1;
        _lvPower = 1;

        // パラメータ更新
        UpdateParam();
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
        Shot.Add(X, Y, Angle, SHOT_SPEED, _power);
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

    /// パラメータ更新
    void UpdateParam()
    {
        // 射程範囲
        _range = TowerParam.Range(_lvRange);
        // 連射速度
        _firerate = TowerParam.Firerate(_lvFirerate);
        // 攻撃威力
        _power = TowerParam.Power(_lvPower);
    }

    /// アップグレードする
    public void Upgrade(eUpgrade type)
    {
        switch (type)
        {
            case eUpgrade.Range:
                // 射程範囲のレベルアップ
                _lvRange++;
                break;
            case eUpgrade.Firerate:
                // 連射速度のアップグレード
                _lvFirerate++;
                break;
            case eUpgrade.Power:
                // 攻撃威力のアップグレード
                _lvPower++;
                break;
        }

        // パラメータ更新
        UpdateParam();

        // アップグレードエフェクト生成
        Particle p = Particle.Add(Particle.eType.Ellipse, 20, X, Y, 0, 0);
        if (p)
        {
            p.SetColor(0.2f, 0.2f, 1);
        }
    }
}
