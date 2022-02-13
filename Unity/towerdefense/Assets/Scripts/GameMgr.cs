using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    int _tAppear = 0;
    List<Vec2D> _path;
    Cursor _cursor;
    Layer2D _lCollision;

    // Start is called before the first frame update
    void Start()
    {
        // 敵管理を生成
        Enemy.parent = new TokenMgr<Enemy>("Enemy", 128);

        // ショット管理を生成
        Shot.parent = new TokenMgr<Shot>("Shot", 128);

        // パーティクル管理を生成
        Particle.parent = new TokenMgr<Particle>("Particle", 256);

        // タワー管理を生成
        Tower.parent = new TokenMgr<Tower>("Tower", 64);

        // マップ管理を生成
        GameObject prefab = null;
        prefab = Util.GetPrefab(prefab, "Field");

        // インスタンス生成
        Field field = Field.CreateInstance2<Field>(prefab, 0, 0);

        // マップ読み込み
        field.Load();

        // パスを取得
        _path = field.Path;

        // コリジョンレイヤーを取得
        _lCollision = field.lCollision;

        // カーソルを取得
        _cursor = GameObject.Find("Cursor").GetComponent<Cursor>();
    }

    // Update is called once per frame
    void Update()
    {
        _tAppear++;
        if (_tAppear % 240 == 0)
        {
            // 敵を生成するテスト
            Enemy.Add(_path);
        }

        // カーソルを更新
        _cursor.Proc(_lCollision);

        // 配置できるかどうか判定
        if (_cursor.Placeable == false)
        {
            return;
        }

        // マウスクリックがされていないことの判定 0は左クリック
        if (Input.GetMouseButtonDown(0) == false)
        {
            return;
        }
        if (_cursor.SelObj == null)
        {
            Tower.Add(_cursor.X, _cursor.Y);

        }
    }
}
