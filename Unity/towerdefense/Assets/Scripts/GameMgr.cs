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
        if (_tAppear % 120 == 0)
        {
            // 敵を生成するテスト
            Enemy.Add(_path);
        }
    }
}
