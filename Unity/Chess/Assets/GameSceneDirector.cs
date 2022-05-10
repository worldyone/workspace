using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneDirector : MonoBehaviour
{
    // ゲーム設定
    public const int TILE_X = 8;
    public const int TILE_Y = 8;
    const int PLAYER_MAX = 2;

    // タイルのプレハブ
    public GameObject[] prefabTile;
    // カーソルのプレハブ
    public GameObject prefabCursor;

    // 内部データ
    GameObject[,] tiles;
    UnitController[,] units;

    // ユニットのプレハブ（色ごと）
    public List<GameObject> prefabWhiteUnits;
    public List<GameObject> prefabBlackUnits;

    // 1:ポーン 2:ルーク 3:ナイト 4:ビショップ 5:クイーン 6:キング
    public int[,] unitType =
    {
        {2, 1, 0, 0, 0, 0, 11, 12 },
        {3, 1, 0, 0, 0, 0, 11, 13 },
        {4, 1, 0, 0, 0, 0, 11, 14 },
        {5, 1, 0, 0, 0, 0, 11, 15 },
        {6, 1, 0, 0, 0, 0, 11, 16 },
        {4, 1, 0, 0, 0, 0, 11, 14 },
        {3, 1, 0, 0, 0, 0, 11, 13 },
        {2, 1, 0, 0, 0, 0, 11, 12 },

        // {2, 0, 0, 0, 0, 0, 11, 12 },
        // {0, 0, 0, 0, 0, 0, 11, 13 },
        // {0, 0, 0, 0, 0, 0, 11, 14 },
        // {5, 0, 0, 0, 0, 0, 11, 15 },
        // {6, 0, 0, 0, 0, 0, 11, 16 },
        // {0, 0, 0, 0, 0, 0, 11, 14 },
        // {0, 0, 0, 0, 0, 0, 11, 13 },
        // {2, 0, 0, 0, 0, 0, 11, 12 },
    };

    // UI関連
    GameObject txtTurnInfo;
    GameObject txtResultInfo;
    GameObject btnApply;
    GameObject btnCancel;

    // 選択ユニット
    UnitController selectUnit;

    // 移動関連
    List<Vector2Int> movableTiles;
    List<GameObject> cursors;

    // モード
    enum MODE
    {
        NONE,
        CHECK_MATE,
        NORMAL,
        STATUS_UPDATE,
        TURN_CHANGE,
        RESULT,
    }

    MODE nowMode, nextMode;
    int nowPlayer;

    // 前回ユニット削除からの経過ターン
    int prevDestoryTurn;

    // 前回の盤面
    List<UnitController[,]> prevUnits;

    // Start is called before the first frame update
    void Start()
    {
        // UIオブジェクト取得
        txtTurnInfo = GameObject.Find("TextTurnInfo");
        txtResultInfo = GameObject.Find("TextResultInfo");
        btnApply = GameObject.Find("ButtonApply");
        btnCancel = GameObject.Find("ButtonCancel");

        // リザルト関連は非表示
        btnApply.SetActive(false);
        btnCancel.SetActive(false);

        // 内部データの初期化
        tiles = new GameObject[TILE_X, TILE_Y];
        units = new UnitController[TILE_X, TILE_Y];
        cursors = new List<GameObject>();
        prevUnits = new List<UnitController[,]>();

        for (int i = 0; i < TILE_X; i++)
        {
            for (int j = 0; j < TILE_Y; j++)
            {
                // タイルとユニットのポジション
                float x = i - TILE_X / 2;
                float y = j - TILE_Y / 2;

                // 作成
                int idx = (i + j) % 2;
                Vector3 pos = new Vector3(x, 0, y);
                GameObject tile = Instantiate(prefabTile[idx], pos, Quaternion.identity);

                tiles[i, j] = tile;

                // ユニットの生成
                int type = unitType[i, j] % 10;
                int player = unitType[i, j] / 10;

                GameObject prefab = getPrefabUnit(player, type);
                GameObject unit = null;
                UnitController ctrl = null;

                if (null == prefab) continue;

                pos.y += 1.5f;
                unit = Instantiate(prefab);

                // 初期化処理
                ctrl = unit.GetComponent<UnitController>();
                ctrl.SetUnit(player, (UnitController.TYPE)type, tile);

                // 内部データセット
                units[i, j] = ctrl;
            }
        }

        nowPlayer = -1;
        nowMode = MODE.NONE;
        nextMode = MODE.TURN_CHANGE;
    }

    // Update is called once per frame
    void Update()
    {
        if (MODE.CHECK_MATE == nowMode)
        {
            checkMateMode();
        }
        else if (MODE.NORMAL == nowMode)
        {
            normalMode();
        }
        else if (MODE.STATUS_UPDATE == nowMode)
        {
            statusUpdateMode();
        }
        else if (MODE.TURN_CHANGE == nowMode)
        {
            turnChangeMode();
        }

        // モード変更
        if (MODE.NONE != nextMode)
        {
            nowMode = nextMode;
            nextMode = MODE.NONE;
        }
    }

    // ターン変更モード
    void turnChangeMode()
    {
        // ターンの処理
        nowPlayer = getNextPlayer();

        // Infoの更新
        txtTurnInfo.GetComponent<UnityEngine.UI.Text>().text = "" + (nowPlayer + 1) + "Pの番です";

        // 経過ターン（1P側にきたら1ターン経過する）
        if (0 == nowPlayer)
        {
            prevDestoryTurn++;
        }

        nextMode = MODE.CHECK_MATE;
    }

    int getNextPlayer()
    {
        int next = nowPlayer + 1;
        if (PLAYER_MAX <= next) next = 0;

        return next;
    }

    // 移動後の処理
    void statusUpdateMode()
    {
        // キャスリング
        if (selectUnit.Status.Contains(UnitController.STATUS.QSIDE_CASTLING))
        {
            // 左端のルーク
            UnitController unit = units[0, selectUnit.Pos.y];
            Vector2Int tile = new Vector2Int(selectUnit.Pos.x + 1, selectUnit.Pos.y);

            moveUnit(unit, tile);
        }
        else if (selectUnit.Status.Contains(UnitController.STATUS.KSIDE_CASTLING))
        {
            // 右端のルーク
            UnitController unit = units[TILE_X - 1, selectUnit.Pos.y];
            Vector2Int tile = new Vector2Int(selectUnit.Pos.x - 1, selectUnit.Pos.y);

            moveUnit(unit, tile);
        }

        // アンパッサンとプロモーション
        if (UnitController.TYPE.PAWN == selectUnit.Type)
        {
            // アンパッサン
            foreach (var v in getUnits(getNextPlayer()))
            {
                if (!v.Status.Contains(UnitController.STATUS.EN_PASSANT)) continue;

                // 置いた場所がアンパッサン対象か
                if (selectUnit.Pos == v.OldPos)
                {
                    Destroy(v.gameObject);
                }
            }

            // プロモーション
            int py = TILE_Y - 1;
            if (selectUnit.Player == 1) py = 0;

            // 端に到達
            if (py == selectUnit.Pos.y)
            {
                // クイーン固定
                GameObject prefab = getPrefabUnit(nowPlayer, (int)UnitController.TYPE.QUEEN);
                UnitController unit = Instantiate(prefab).GetComponent<UnitController>();
                GameObject tile = tiles[selectUnit.Pos.x, selectUnit.Pos.y];

                unit.SetUnit(selectUnit.Player, UnitController.TYPE.QUEEN, tile);
                moveUnit(unit, new Vector2Int(selectUnit.Pos.x, selectUnit.Pos.y));

            }
        }

        // ターン経過
        foreach (var v in getUnits(nowPlayer))
        {
            v.ProgressTurn();
        }

        // カーソルをリセット
        setSelectCursors();

        nextMode = MODE.TURN_CHANGE;

    }

    // チェックメイトモード
    void checkMateMode()
    {
        // 次のモード
        nextMode = MODE.NORMAL;
        UnityEngine.UI.Text info = txtResultInfo.GetComponent<UnityEngine.UI.Text>();
        info.text = "";

        // --------------------
        // 引き分けのチェック(簡易版)
        // --------------------

        // ユニット数が1体ずつになったら引き分け
        if (3 > getUnits().Count)
        {
            info.text = "チェックメイトできないので\n引き分け";
            nextMode = MODE.RESULT;
        }

        // 50ターンの間、どのユニットも削除されなければ引き分け
        if (50 < prevDestoryTurn)
        {
            info.text = "50ターンルールで\n引き分け";
            nextMode = MODE.RESULT;
        }

        // 3回同じ盤面になったら引き分け
        int prevcount = 0;
        foreach (var v in prevUnits)
        {
            bool check = true;
            for (int i = 0; i < v.GetLength(0); i++)
            {
                for (int j = 0; j < v.GetLength(1); j++)
                {
                    if (v[i, j] != units[i, j]) check = false;
                }
            }

            if (check) prevcount++;
        }

        if (2 < prevcount)
        {
            info.text = "同じ盤面が続いたので\n引き分け";
            nextMode = MODE.RESULT;
        }

        // --------------------
        // チェックのチェック
        // --------------------
        // 今回のプレイヤーのキング
        UnitController target = getUnit(nowPlayer, UnitController.TYPE.KING);
        // チェックしているユニット
        List<UnitController> checkunits = GetCheckUnits(units, nowPlayer);
        // チェック状態セット
        bool ischeck = (0 < checkunits.Count) ? true : false;

        if (null != target)
        {
            target.SetCheckStatus(ischeck);
        }

        // ゲームが続くならチェックと表示
        if (ischeck && MODE.RESULT != nextMode)
        {
            info.text = "チェック！！";
        }

        // --------------------
        // 移動可能範囲を調べる
        // --------------------
        int tilecount = 0;

        // 移動可能範囲をカウント
        foreach (var v in getUnits(nowPlayer))
        {
            tilecount += getMovableTiles(v).Count;
        }

        // 動かせない
        if (1 > tilecount)
        {
            info.text = "ステイルメイト\n" + "引き分け";
            if (ischeck)
            {
                info.text = "チェックメイト\n" + (getNextPlayer() + 1) + "Pの勝ち！！";
            }

            nextMode = MODE.RESULT;
        }

        // 今回の盤面をコピー
        UnitController[,] copyunits = GetCopyArray(units);
        prevUnits.Add(copyunits);

        // 次のモードの準備
        if (MODE.RESULT == nextMode)
        {
            btnApply.SetActive(true);
            btnCancel.SetActive(true);
        }

    }

    // ノーマルモード
    void normalMode()
    {
        GameObject tile = null;
        UnitController unit = null;

        // プレイヤーの処理
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // ユニットにも当たり判定があるのでヒットした全てのオブジェクト情報を取得
            foreach (RaycastHit hit in Physics.RaycastAll(ray))
            {
                if (hit.transform.name.Contains("Tile"))
                {
                    tile = hit.transform.gameObject;
                    break;
                }
            }
        }

        // タイルが押されていなければ処理しない
        if (null == tile) return;

        // 選択されたタイルからユニット取得
        Vector2Int tilepos = new Vector2Int(
            (int)tile.transform.position.x + TILE_X / 2,
            (int)tile.transform.position.z + TILE_Y / 2
        );

        // ユニット
        unit = units[tilepos.x, tilepos.y];

        // ユニット選択
        if (null != unit
            && selectUnit != unit
            && nowPlayer == unit.Player)
        {
            // 移動可能範囲を取得
            List<Vector2Int> tiles = getMovableTiles(unit);

            // 移動できない場合
            if (1 > tiles.Count) return;

            movableTiles = tiles;
            setSelectCursors(unit);
        }
        // 移動
        else if (null != selectUnit && movableTiles.Contains(tilepos))
        {
            moveUnit(selectUnit, tilepos);
            nextMode = MODE.STATUS_UPDATE;
        }

    }

    // 指定のユニットを取得する
    UnitController getUnit(int player, UnitController.TYPE type)
    {
        foreach (var v in getUnits(player))
        {
            if (player != v.Player) continue;
            if (type == v.Type) return v;
        }

        return null;
    }

    // 指定されたプレイヤー番号のユニットを取得する
    List<UnitController> getUnits(int player = -1)
    {
        List<UnitController> ret = new List<UnitController>();

        foreach (var v in units)
        {
            if (null == v) continue;
            if (player == v.Player)
            {
                ret.Add(v);
            }
            else if (0 > player)
            {
                ret.Add(v);
            }
        }

        return ret;
    }

    // 指定された配列をコピーして返す
    public static UnitController[,] GetCopyArray(UnitController[,] org)
    {
        UnitController[,] ret = new UnitController[org.GetLength(0), org.GetLength(1)];
        Array.Copy(org, ret, org.Length);
        return ret;
    }

    // 移動可能範囲取得
    List<Vector2Int> getMovableTiles(UnitController unit)
    {
        // そこをどいたらチェックされてしまうか
        UnitController[,] copyunits = GetCopyArray(units);
        copyunits[unit.Pos.x, unit.Pos.y] = null;

        // チェックされるかどうか
        List<UnitController> checkunits = GetCheckUnits(copyunits, unit.Player);

        // チェックを回避できるタイルを返す
        if (0 < checkunits.Count)
        {
            // 移動可能範囲
            List<Vector2Int> ret = new List<Vector2Int>();

            // 移動可能範囲
            List<Vector2Int> movetiles = unit.GetMovableTiles(units);

            // 移動してみる
            foreach (var v in movetiles)
            {
                // 移動した状態を作る
                UnitController[,] copyunits2 = GetCopyArray(units);
                copyunits2[unit.Pos.x, unit.Pos.y] = null;
                copyunits2[v.x, v.y] = unit;

                int checkcount = GetCheckUnits(copyunits2, unit.Player, false).Count;

                if (1 > checkcount) ret.Add(v);
            }

            return ret;
        }

        // 通常移動可能範囲を返す
        return unit.GetMovableTiles(units);
    }

    // 選択時の関数
    void setSelectCursors(UnitController unit = null, bool setunit = true)
    {
        // カーソル解除
        foreach (var v in cursors)
        {
            Destroy(v);
        }

        // 選択ユニットの非選択状態
        if (null != selectUnit)
        {
            selectUnit.SelectUnit(false);
            selectUnit = null;
        }

        // なにもセットされないなら終了
        if (null == unit) return;

        // カーソル作成
        foreach (var v in getMovableTiles(unit))
        {
            Vector3 pos = tiles[v.x, v.y].transform.position;
            pos.y += 0.51f;

            GameObject obj = Instantiate(prefabCursor, pos, Quaternion.identity);
            cursors.Add(obj);
        }

        // 選択状態
        if (setunit)
        {
            selectUnit = unit;
            selectUnit.SelectUnit();
        }
    }

    // ユニット移動
    void moveUnit(UnitController unit, Vector2Int tilepos)// 選択時の関数
    {
        // 現在地
        Vector2Int unitpos = unit.Pos;

        // 誰かいたら消す
        if (null != units[tilepos.x, tilepos.y])
        {
            Destroy(units[tilepos.x, tilepos.y].gameObject);
            prevDestoryTurn = 0;
        }

        // 新しい場所へ移動
        unit.MoveUnit(tiles[tilepos.x, tilepos.y]);

        // 内部データ更新（元の場所）
        units[unitpos.x, unitpos.y] = null;

        // 内部データ更新（新しい場所）
        units[tilepos.x, tilepos.y] = unit;

    }

    // ユニットのプレハブを取得
    GameObject getPrefabUnit(int player, int type)
    {
        int idx = type - 1;

        if (0 > idx) return null;

        GameObject prefab = prefabWhiteUnits[idx];
        if (1 == player) prefab = prefabBlackUnits[idx];

        return prefab;
    }

    // 指定された配置でチェックされているかチェック
    static public List<UnitController> GetCheckUnits(UnitController[,] units, int player, bool checkking = true)
    {
        List<UnitController> ret = new List<UnitController>();

        foreach (var v in units)
        {
            if (null == v) continue;
            if (player == v.Player) continue;

            // 敵1体の移動可能範囲
            List<Vector2Int> enemytiles = v.GetMovableTiles(units, checkking);

            foreach (var t in enemytiles)
            {
                if (null == units[t.x, t.y]) continue;

                if (UnitController.TYPE.KING == units[t.x, t.y].Type)
                {
                    ret.Add(v);
                }
            }
        }

        return ret;
    }

    public void Retry()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Title()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
