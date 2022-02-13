using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    int _tAppear = 0;
    List<Vec2D> _path;

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

        // マウスクリック判定 0は左クリック
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 posScreen = Input.mousePosition;

            // ワールド座標に変換
            Vector2 posWorld = Camera.main.ScreenToWorldPoint(posScreen);

            Tower.Add(posWorld.x, posWorld.y);
        }
    }
}
