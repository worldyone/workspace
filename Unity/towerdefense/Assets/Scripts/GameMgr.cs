using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    /// 選択モード
    public enum eSelMode
    {
        None, // なし
        Buy, // 購入モード
    }
    eSelMode _selMode = eSelMode.None;
    int _tAppear = 0;
    List<Vec2D> _path;
    Cursor _cursor;
    Layer2D _lCollision;
    Gui _gui;

    // Start is called before the first frame update
    void Start()
    {
        // ゲームパラメータ初期化
        Global.Init();

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

        // GUIを生成
        _gui = new Gui();

        // カーソルを取得
        _cursor = GameObject.Find("Cursor").GetComponent<Cursor>();
    }

    // Update is called once per frame
    void Update()
    {
        // GUIを更新
        _gui.Update(_selMode);

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

        // マウスクリック判定
        if (Input.GetMouseButtonDown(0) == false)
        {
            // クリックしていないので以下の処理は不要
            return;
        }

        switch (_selMode)
        {
            case eSelMode.Buy:
                if (_cursor.SelObj == null)
                {
                    // 所持金を減らす
                    int cost = Cost.TowerProduction();
                    Global.UseMoney(cost);

                    // タワーを設置
                    Tower.Add(_cursor.X, _cursor.Y);

                    // 次のタワーの生産コストを取得する
                    int cost2 = Cost.TowerProduction();
                    if (Global.Money < cost2)
                    {
                        // お金が足りないので通常モードに戻る
                        ChangeSelMode(eSelMode.None);
                    }
                }
                break;
        }
    }

    /// 購入ボタンをクリックした
    public void OnClickBuy()
    {
        ChangeSelMode(eSelMode.Buy);
    }

    /// 選択モードの変更
    void ChangeSelMode(eSelMode mode)
    {
        switch (mode)
        {
            case eSelMode.None:
                // 初期状態に戻す
                // 購入ボタンを表示する
                MyCanvas.SetActive("ButtonBuy", true);
                break;
            case eSelMode.Buy:
                // 購入モード
                // 購入ボタンを非表示にする
                MyCanvas.SetActive("ButtonBuy", false);
                break;
        }
        _selMode = mode;
    }
}
