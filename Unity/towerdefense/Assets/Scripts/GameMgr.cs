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
        Upgrade, // アップグレード
    }
    eSelMode _selMode = eSelMode.None;
    // 選択中のオブジェクト
    GameObject _selObj = null;
    // 選択中のタワー
    Tower _selTower = null;
    List<Vec2D> _path;
    Cursor _cursor;
    Layer2D _lCollision;
    Gui _gui;
    EnemyGenerator _enemyGenerator;
    WaveStart _waveStart;
    CursorRange _cursorRange;

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

        // 射程範囲カーソルを取得する
        _cursorRange = GameObject.Find("CursorRange").GetComponent<CursorRange>();

        // 初期状態は選択しないモード
        ChangeSelMode(eSelMode.None);
    }

    void Update()
    {
        // GUIを更新
        _gui.Update(_selMode, _selTower);

        // カーソルを更新
        _cursor.Proc(_lCollision);

        switch (_state)
        {
            case eState.Wait:
                // Wave開始
                _tWait -= Time.deltaTime;
                if (_tWait < 0)
                {
                    _enemyGenerator.Start();
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

        // カーソルの下にあるオブジェクトをチェック
        int mask = 1 << LayerMask.NameToLayer("Tower");
        Collider2D col = Physics2D.OverlapPoint(_cursor.GetPosition(), mask);
        _selObj = null;
        if (col != null)
        {
            // 選択中のオブジェクトを格納
            _selObj = col.gameObject;
        }

        // マウスクリック判定
        if (Input.GetMouseButtonDown(0) == false)
        {
            // クリックしていないので以下の処理は不要
            return;
        }

        if (_selObj != null)
        {
            // タワーをクリックした
            _selTower = _selObj.GetComponent<Tower>();

            // アップグレードモードに移行する
            ChangeSelMode(eSelMode.Upgrade);
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

    /// アップグレード・射程範囲をクリックした
    public void OnClickRange()
    {
        ExecUpgrade(Tower.eUpgrade.Range);
    }

    /// アップグレード・連射速度をクリックした
    public void OnClickFirerate()
    {
        ExecUpgrade(Tower.eUpgrade.Firerate);
    }

    /// アップグレード・攻撃威力をクリックした
    public void OnClickPower()
    {
        ExecUpgrade(Tower.eUpgrade.Power);
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
                // タワー情報は非表示
                MyCanvas.SetActive("TextTowerInfo", false);
                // 射程範囲を非表示
                _cursorRange.SetVisible(false, 0);
                // アップグレードモードボタンを非表示
                SetActiveUpgrade(false);
                break;
            case eSelMode.Buy:
                // 購入モード
                // 購入ボタンを非表示にする
                MyCanvas.SetActive("ButtonBuy", false);
                // タワー情報は非表示
                MyCanvas.SetActive("TextTowerInfo", false);
                // 射程範囲を非表示
                _cursorRange.SetVisible(false, 0);
                // アップグレードモードボタンを非表示
                SetActiveUpgrade(false);
                break;
            case eSelMode.Upgrade:
                // アップグレードモード
                // 購入ボタンを表示する
                MyCanvas.SetActive("ButtonBuy", true);
                // タワー情報は非表示
                MyCanvas.SetActive("TextTowerInfo", false);
                // 射程範囲を表示
                _cursorRange.SetVisible(true, _selTower.LvRange);
                _cursorRange.SetPosition(_cursor);
                // アップグレードモードボタンを表示
                SetActiveUpgrade(true);
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

    /// アップグレードモードボタンの表示・非表示を切替
    void SetActiveUpgrade(bool b)
    {
        // 各種ボタンの表示制御
        MyCanvas.SetActive("ButtonRange", b);
        MyCanvas.SetActive("ButtonFirerate", b);
        MyCanvas.SetActive("ButtonPower", b);
    }

    /// アップグレードを実行する
    void ExecUpgrade(Tower.eUpgrade type)
    {
        // コストを取得する
        int cost = _selTower.GetCost(type);

        // 所持金を減らす
        Global.UseMoney(cost);

        // アップグレード実行
        _selTower.Upgrade(type);

        // 射程範囲カーソルの大きさを反映
        _cursorRange.SetVisible(true, _selTower.LvRange);
    }
}
