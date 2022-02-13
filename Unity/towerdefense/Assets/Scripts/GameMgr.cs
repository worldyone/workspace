using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMgr : MonoBehaviour
{
    /// 停止タイマー
    // 2sec停止する
    const float TIMER_WAIT = 2.0f;
    float _tWait = TIMER_WAIT;

    /// 状態
    enum eState
    {
        Wait, // Wave開始前
        Main, // メイン
        Gameover, // ゲームオーバー
    }
    eState _state = eState.Wait;

    /// 選択モード
    public enum eSelMode
    {
        None, // なし
        Buy, // 購入モード
    }
    eSelMode _selMode = eSelMode.None;
    List<Vec2D> _path;
    Cursor _cursor;
    Layer2D _lCollision;
    Gui _gui;
    EnemyGenerator _enemyGenerator;
    WaveStart _waveStart;

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

        // 敵生成管理を生成
        _enemyGenerator = new EnemyGenerator(_path);

        // Wave開始演出を取得
        _waveStart = MyCanvas.Find<WaveStart>("TextWaveStart");

        // カーソルを取得
        _cursor = GameObject.Find("Cursor").GetComponent<Cursor>();
    }

    void Update()
    {
        // GUIを更新
        _gui.Update(_selMode);

        // カーソルを更新
        _cursor.Proc(_lCollision);

        switch (_state)
        {
            case eState.Wait:
                // Wave開始
                _tWait -= Time.deltaTime;
                if (_tWait < 0)
                {
                    _enemyGenerator.Start(Global.Wave);
                    // Wave開始演出を呼び出す
                    _waveStart.Begin(Global.Wave);
                    // メイン状態に遷移する
                    _state = eState.Main;
                }
                break;
            case eState.Main:
                // メインの更新
                UpdateMain();

                // ゲームオーバーチェック
                if (Global.Life <= 0)
                {
                    _state = eState.Gameover;
                    MyCanvas.SetActive("TextGameover", true);
                    break;
                }

                // Waveクリアチェック
                if (IsWaveClear())
                {
                    // 次のWaveへ
                    Global.NextWave();
                    // 停止タイマー設定
                    _tWait = TIMER_WAIT;
                    _state = eState.Wait;
                }

                break;
            case eState.Gameover:
                if (Input.GetMouseButton(0))
                {
                    // やり直し
                    SceneManager.LoadScene("Main");
                }
                break;
        }
    }

    void UpdateMain()
    {
        // 敵生成管理の更新
        _enemyGenerator.Update();

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

    /// Waveをクリアしたかどうか
    bool IsWaveClear()
    {
        if (_enemyGenerator.Number > 0)
        {
            // 敵がまだ出現する
            return false;
        }

        if (Enemy.parent.Count() > 0)
        {
            // 敵が存在するのでクリアしていない
            return false;
        }

        // クリア
        return true;
    }
}
