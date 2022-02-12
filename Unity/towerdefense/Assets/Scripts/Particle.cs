using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : Token
{
    public Sprite spr0; // 塗りつぶしの円
    public Sprite spr1; // リング
    public enum eType
    {
        Ball, // 塗りつぶしの円
        Ring, // リング
        Ellipse, // 楕円

    }

    // パーティクル管理
    public static TokenMgr<Particle> parent;
    public static Particle Add(eType type, int timer, float px, float py, float direction, float speed)
    {
        Particle p = parent.Add(px, py, direction, speed);
        if (p == null)
        {
            return null;
        }

        p.Init(type, timer);

        return p;
    }

    // メンバ変数定義
    // 種別
    eType _type;

    // 消滅タイマー
    int _tDestroy;
    // 拡大タイマー
    const float SCALE_MAX = 4;
    float _tScale;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        switch (_type)
        {
            case eType.Ball:
                // 速度を減衰し、小さくする
                MulVelocity(0.9f);
                MulScale(0.93f);
                break;
            case eType.Ring:
                // スケール値を設定
                _tScale *= 0.9f;
                Scale = (SCALE_MAX - _tScale);
                // アルファ値を小さくする
                Alpha -= 0.05f;
                break;
            case eType.Ellipse:
                // スケール値を設定
                _tScale *= 0.9f;
                ScaleX = (SCALE_MAX - _tScale) * 2;
                ScaleY = (SCALE_MAX - _tScale);
                // アルファ値を小さくする
                Alpha -= 0.05f;
                break;
        }

        // 消滅チェック
        _tDestroy--;
        if (_tDestroy < 1)
        {
            Vanish();
        }
    }

    /// 初期化
    void Init(eType type, int timer)
    {
        switch (type)
        {
            case eType.Ball:
                SetSprite(spr0);
                break;
            case eType.Ring:
            case eType.Ellipse:
                SetSprite(spr1);
                _tScale = SCALE_MAX;
                break;
        }
        _type = type;

        // タイマー設定
        _tDestroy = timer;

        // 初期化
        Scale = 1.0f;
        Alpha = 1.0f;
    }
}
