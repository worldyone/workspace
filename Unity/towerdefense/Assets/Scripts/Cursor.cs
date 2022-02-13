using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : Token
{
    /// 表示スプライト
    public Sprite sprRect; // 四角
    public Sprite sprCross; // バツ

    GameObject _selObj = null; // カーソルにあるオブジェクト
    public GameObject SelObj
    {
        get { return _selObj; }
    }

    bool _bPlaceable = true;
    public bool Placeable
    {
        get { return _bPlaceable; }
        set
        {
            if (value)
            {
                // 配置できるので四角
                SetSprite(sprRect);
            }
            else
            {
                // 配置できないのでバツ
                SetSprite(sprCross);
            }
            _bPlaceable = value;
        }
    }

    public void Proc(Layer2D lCollision)
    {
        Vector3 posScreen = Input.mousePosition;
        Vector2 posWorld = Camera.main.ScreenToWorldPoint(posScreen);
        int i = Field.ToChipX(posWorld.x);
        int j = Field.ToChipY(posWorld.y);
        X = Field.ToWorldX(i);
        Y = Field.ToWorldY(j);

        Placeable = (lCollision.Get(i, j) == 0);
        Visible = (lCollision.IsOutOfRange(i, j) == false);

        // 選択しているオブジェクトを設定
        SetSelObj();
    }

    /// カーソルの下にあるオブジェクトをチェック
    void SetSelObj()
    {
        int mask = 1 << LayerMask.NameToLayer("Tower");
        Collider2D col = Physics2D.OverlapPoint(GetPosition(), mask);
        _selObj = null;
        if (col != null)
        {
            _selObj = col.gameObject;
        }
    }
}
