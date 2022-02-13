using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : Token
{
    /// 開始地点
    public const int CHIP_PATH_START = 26;
    /// 何もない
    public const int CHIP_NONE = 0;
    /// パス（座標リスト）
    List<Vec2D> _path;
    public List<Vec2D> Path
    {
        get { return _path; }
    }

    /// コリジョンレイヤー
    Layer2D _lCollision;
    public Layer2D lCollision
    {
        get { return _lCollision; }
    }

    // void Start()
    public void Load()
    {
        // マップ読み込み
        TMXLoader tmx = new TMXLoader();
        tmx.Load("Levels/map");

        // 経路レイヤーを取得
        Layer2D lPath = tmx.GetLayer("path");

        // 開始地点を検索
        Vec2D pos = lPath.Search(CHIP_PATH_START);

        // 座標リストを作成
        _path = new List<Vec2D>();

        // 開始座標を座標リストに登録
        _path.Add(new Vec2D(pos.X, pos.Y));

        // 通路をふさぐ
        lPath.Set(pos.X, pos.Y, CHIP_NONE);

        // 座標リスト作成
        CreatePath(lPath, pos.X, pos.Y, _path);

        // コリジョンレイヤーを取得する
        _lCollision = tmx.GetLayer("collision");

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// チップ1マスのサイズを取得する
    public static float GetChipSize()
    {
        var spr = GetChipSprite();
        return spr.bounds.size.x;
    }

    /// チップサイズの基準となるスプライトを取得する
    static Sprite GetChipSprite()
    {
        return Util.GetSprite("Levels/tileset", "tileset_0");
    }

    /// チップ座標をワールドのX座標として取得する
    public static float ToWorldX(int i)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        var spr = GetChipSprite();
        var sprW = spr.bounds.size.x;

        return min.x + (sprW * i) + sprW / 2;
    }

    /// チップ座標をワールドのY座標として取得する
    public static float ToWorldY(int j)
    {
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        var spr = GetChipSprite();
        var sprH = spr.bounds.size.y;

        return max.y - (sprH * j) - sprH / 2;
    }

    /// パスを作る
    void CreatePath(Layer2D layer, int x, int y, List<Vec2D> path)
    {
        // 左・上・右・下
        int[] xTbl = { -1, 0, 1, 0 };
        int[] yTbl = { 0, -1, 0, 1 };
        for (var i = 0; i < 4/*4方向*/; i++)
        {
            int x2 = x + xTbl[i];
            int y2 = y + yTbl[i];
            int val = layer.Get(x2, y2);

            if (val > CHIP_NONE)
            {
                // 経路を発見
                layer.Set(x2, y2, CHIP_NONE);
                path.Add(new Vec2D(x2, y2));

                CreatePath(layer, x2, y2, path);
            }
        }
    }

    /// ワールド座標をチップ座標系のX座標に変換する
    public static int ToChipX(float x)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        var spr = GetChipSprite();
        var sprW = spr.bounds.size.x;

        return (int)((x - min.x) / sprW);
    }

    /// ワールド座標をチップ座標系のY座標に変換する
    public static int ToChipY(float y)
    {
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        var spr = GetChipSprite();
        var sprH = spr.bounds.size.y;

        return (int)((y - max.y) / -sprH);
    }
}
