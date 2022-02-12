using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Token
{
    /// 管理オブジェクト
    public static TokenMgr<Enemy> parent = null;
    public Sprite spr0;
    public Sprite spr1;

    // HP
    int _hp;

    int _tAnim = 0;

    float _speed = 0;
    float _tSpeed = 0; // 補完値(0.0 ~ 100.0)
    List<Vec2D> _path;
    int _pathIdx;
    Vec2D _prev;
    Vec2D _next;

    void FixedUpdate()
    {
        _tAnim++;
        if (_tAnim % 32 < 16)
        {
            SetSprite(spr0);
        }
        else
        {
            SetSprite(spr1);
        }

        _tSpeed += _speed;
        if (_tSpeed >= 100.0f)
        {
            // 移動先を次に進める
            _tSpeed -= 100.0f;
            MoveNext();
        }
        X = Mathf.Lerp(_prev.x, _next.x, _tSpeed / 100.0f);
        Y = Mathf.Lerp(_prev.y, _next.y, _tSpeed / 100.0f);

        // 画像の角度を更新
        UpdateAngle();
    }

    /// 衝突判定
    void OnTriggerEnter2D(Collider2D other)
    {
        string name = LayerMask.LayerToName(other.gameObject.layer);
        if (name == "Shot")
        {
            Shot s = other.gameObject.GetComponent<Shot>();
            s.Vanish();

            Damage(1);
        }
    }

    void MoveNext()
    {
        if (_pathIdx >= _path.Count)
        {
            // ゴールに辿り着いた
            _tSpeed = 100.0f;
            return;
        }

        // 移動先を移動元にコピーする
        _prev.Copy(_next);

        // チップ座標を取り出す
        Vec2D v = _path[_pathIdx];
        _next.x = Field.ToWorldX(v.X);
        _next.y = Field.ToWorldY(v.Y);
        _pathIdx++;
    }
    public void Init(List<Vec2D> path)
    {
        _path = path;
        _pathIdx = 0;
        _speed = 2.0f;
        _tSpeed = 0;

        // アニメーションの違和感をなくすため、画面外から出るようにする
        MoveNext();
        _prev.Copy(_next);
        _prev.x -= Field.GetChipSize();
        FixedUpdate();

        // HPを設定する
        _hp = 2;
    }

    void UpdateAngle()
    {
        float dx = _next.x - _prev.x;
        float dy = _next.y - _prev.y;
        Angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
    }

    // プレハブから敵を生成
    public static Enemy Add(List<Vec2D> path)
    {
        Enemy e = parent.Add(0, 0);
        if (e == null)
        {
            return null;
        }
        e.Init(path);
        return e;
    }

    /// ダメージを受ける処理
    void Damage(int val)
    {
        _hp -= val;
        if (_hp <= 0)
        {
            // HPがなくなったので死亡
            Vanish();
        }
    }
}
