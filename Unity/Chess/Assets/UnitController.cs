using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    // このユニットのプレイヤー番号
    public int Player;
    // ユニットの種類
    public TYPE Type;
    // 置いてからの経過ターン
    public int ProgressTurnCount;
    // 置いてる場所
    public Vector2Int Pos, OldPos;
    // 移動状態
    public List<STATUS> Status;

    // 1:ポーン 2:ルーク 3:ナイト 4:ビショップ 5:クイーン 6:キング
    public enum TYPE
    {
        NONE = -1,
        PAWN = 1,
        ROOK,
        KNIGHT,
        BISHOP,
        QUEEN,
        KING,
    }

    public enum STATUS
    {
        NONE = -1,
        QSIDE_CASTLING = 1,
        KSIDE_CASTLING,
        EN_PASSANT,
        CHECK,
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // 初期設定
    public void SetUnit(int player, TYPE type, GameObject tile)
    {
        Player = player;
        Type = type;
        MoveUnit(tile);
        ProgressTurnCount = -1; // 初期状態に戻す
    }

    // 移動可能範囲取得
    public List<Vector2Int> GetMovableTiles(UnitController[,] units, bool checkking = true)
    {
        List<Vector2Int> ret = new List<Vector2Int>();

        // クイーン
        if (TYPE.QUEEN == Type)
        {
            // ルークとビショップの動きを合成
            ret = getMovableTiles(units, TYPE.ROOK);
            foreach (var v in getMovableTiles(units, TYPE.BISHOP))
            {
                if (!ret.Contains(v)) ret.Add(v);
            }
        }
        else if (TYPE.KING == Type)
        {
            ret = getMovableTiles(units, TYPE.KING);

            // 相手の移動範囲を考慮しない
            if (!checkking) return ret;

            // 削除対象のタイル
            List<Vector2Int> removetiles = new List<Vector2Int>();
            // 敵の移動範囲にはいけないようにする
            foreach (var v in ret)
            {
                // 移動した状態を作る
                UnitController[,] copyunits2 = GameSceneDirector.GetCopyArray(units);
                copyunits2[Pos.x, Pos.y] = null;
                copyunits2[v.x, v.y] = this;

                int checkcount = GameSceneDirector.GetCheckUnits(copyunits2, Player, false).Count;

                if (0 < checkcount) removetiles.Add(v);
            }

            // 敵の移動可能範囲と被るタイルを削除する
            foreach (var v in removetiles)
            {
                ret.Remove(v);

                // キャスリングできる時だけ真横のタイルも全て削除する
                if (-1 < ProgressTurnCount || Pos.y != v.y) continue;

                // 方向
                int dir = -1;
                int cnt = units.GetLength(0);
                if (0 > Pos.x - v.x) dir = 1;

                for (int i = 0; i < cnt; i++)
                {
                    Vector2Int del = new Vector2Int(v.x + (i * dir), v.y);
                    ret.Remove(del);
                }
            }
        }
        else
        {
            ret = getMovableTiles(units, Type);
        }

        return ret;
    }

    // 移動可能範囲を返す（プレーンな状態）
    List<Vector2Int> getMovableTiles(UnitController[,] units, TYPE type)
    {
        List<Vector2Int> ret = new List<Vector2Int>();

        // ポーン
        if (TYPE.PAWN == type)
        {
            int dir = 1;
            if (1 == Player) dir = -1;

            // 前方2マス
            List<Vector2Int> vec = new List<Vector2Int>()
            {
                new Vector2Int(0, 1 * dir),
                new Vector2Int(0, 2 * dir),
            };

            // 2回目以降は1マスしかすすめない
            if (-1 < ProgressTurnCount) vec.RemoveAt(vec.Count - 1);

            // 前方
            foreach (var v in vec)
            {
                Vector2Int checkpos = Pos + v;
                if (!isCheckable(units, checkpos)) continue;
                if (null != units[checkpos.x, checkpos.y]) break;

                ret.Add(checkpos);
            }

            // 取れる時は斜めに進める
            vec = new List<Vector2Int>()
            {
                new Vector2Int(-1, 1 * dir),
                new Vector2Int(1, 1 * dir),
            };

            foreach (var v in vec)
            {
                Vector2Int checkpos = Pos + v;
                if (!isCheckable(units, checkpos)) continue;

                // アンパッサン
                if (null != getEnPassantUnit(units, checkpos))
                {
                    ret.Add(checkpos);
                    continue;
                }

                // なにもない
                if (null == units[checkpos.x, checkpos.y]) continue;

                // 自軍のユニットは無視
                if (Player == units[checkpos.x, checkpos.y].Player) continue;

                // 追加
                ret.Add(checkpos);
            }
        }

        // ルーク
        else if (TYPE.ROOK == type)
        {
            // 上下左右にユニットがぶつかるまでどこまでも進める
            List<Vector2Int> vec = new List<Vector2Int>()
            {
                new Vector2Int(0, 1),
                new Vector2Int(0, -1),
                new Vector2Int(1, 0),
                new Vector2Int(-1, 0),
            };

            foreach (var v in vec)
            {
                Vector2Int checkpos = Pos + v;
                while (isCheckable(units, checkpos))
                {
                    // 誰かいたら終了
                    if (null != units[checkpos.x, checkpos.y])
                    {
                        if (Player != units[checkpos.x, checkpos.y].Player)
                        {
                            ret.Add(checkpos);
                        }

                        break;
                    }

                    ret.Add(checkpos);
                    checkpos += v;
                }
            }
        }

        // ナイト
        else if (TYPE.KNIGHT == type)
        {
            // 八方桂の位置に進める
            List<Vector2Int> vec = new List<Vector2Int>()
            {
                new Vector2Int(-1, 2),
                new Vector2Int(-2, 1),
                new Vector2Int(1, 2),
                new Vector2Int(2, 1),
                new Vector2Int(-1, -2),
                new Vector2Int(-2, -1),
                new Vector2Int(1, -2),
                new Vector2Int(2, -1),
            };

            foreach (var v in vec)
            {
                Vector2Int checkpos = Pos + v;
                if (!isCheckable(units, checkpos)) continue;

                // 同じプレイヤーの場所へは行けない
                if (null != units[checkpos.x, checkpos.y]
                    && Player == units[checkpos.x, checkpos.y].Player)
                {
                    continue;
                }

                ret.Add(checkpos);
            }
        }

        // ビショップ
        else if (TYPE.BISHOP == type)
        {
            // 上下左右斜めにユニットがぶつかるまでどこまでも進める
            List<Vector2Int> vec = new List<Vector2Int>()
            {
                new Vector2Int(1, 1),
                new Vector2Int(1, -1),
                new Vector2Int(-1, 1),
                new Vector2Int(-1, -1),
            };

            foreach (var v in vec)
            {
                Vector2Int checkpos = Pos + v;
                while (isCheckable(units, checkpos))
                {
                    // 誰かいたら終了
                    if (null != units[checkpos.x, checkpos.y])
                    {
                        if (Player != units[checkpos.x, checkpos.y].Player)
                        {
                            ret.Add(checkpos);
                        }

                        break;
                    }

                    ret.Add(checkpos);
                    checkpos += v;
                }
            }
        }

        // キング
        else if (TYPE.KING == type)
        {
            // 周り1マスすべてに進める
            List<Vector2Int> vec = new List<Vector2Int>()
            {
                new Vector2Int(1, 1),
                new Vector2Int(1, 0),
                new Vector2Int(1, -1),
                new Vector2Int(0, 1),
                new Vector2Int(0, -1),
                new Vector2Int(-1, 1),
                new Vector2Int(-1, 0),
                new Vector2Int(-1, -1),
            };

            foreach (var v in vec)
            {
                Vector2Int checkpos = Pos + v;
                if (!isCheckable(units, checkpos)) continue;

                // 同じプレイヤーの場所へは行けない
                if (null != units[checkpos.x, checkpos.y]
                    && Player == units[checkpos.x, checkpos.y].Player)
                {
                    continue;
                }

                ret.Add(checkpos);
            }

            // キャスリング
            // 初動じゃないならば出来ない
            if (-1 != ProgressTurnCount) return ret;

            // チェックされていたら出来ない
            if (Status.Contains(STATUS.CHECK)) return ret;

            // キャスリングの移動場所
            vec = new List<Vector2Int>()
            {
                new Vector2Int(-2, 0),
                new Vector2Int(2, 0),
            };

            foreach (var v in vec)
            {
                // 左ルーク
                int posx = 0;
                int dir = -1;
                // 右ルーク
                if (0 < v.x)
                {
                    dir = 1;
                    posx = units.GetLength(0) - 1;
                }

                // 端にいるかどうか
                if (null == units[posx, Pos.y]) continue;

                // ルークじゃない
                if (TYPE.ROOK != units[posx, Pos.y].Type) continue;

                // 初動じゃない
                if (-1 != units[posx, Pos.y].ProgressTurnCount) continue;

                // 移動する途中に誰かいる
                bool lineok = true;
                int cnt = Mathf.Abs(Pos.x - posx);
                for (int i = 1; i < cnt; i++)
                {
                    if (null != units[Pos.x + (i * dir), Pos.y])
                    {
                        lineok = false;
                    }
                }
                if (!lineok) continue;

                Vector2Int checkpos = Pos + v;
                ret.Add(checkpos);

            }

        }

        return ret;
    }

    bool isCheckable(UnitController[,] ary, Vector2Int idx)
    {
        if (idx.x < 0 || ary.GetLength(0) <= idx.x
            || idx.y < 0 || ary.GetLength(1) <= idx.y)
        {
            return false;
        }
        return true;
    }

    // 選択時の処理
    public void SelectUnit(bool select = true)
    {
        // 少し上に上げて、重力状態を解除
        Vector3 pos = transform.position;
        pos.y += 2;
        GetComponent<Rigidbody>().isKinematic = true;

        // 選択解除
        if (!select)
        {
            pos.y = 1.35f;
            GetComponent<Rigidbody>().isKinematic = false;
        }

        transform.position = pos;
    }

    // 移動処理
    public void MoveUnit(GameObject tile)
    {
        // 移動時は非選択状態にする
        SelectUnit(false);

        // タイルのポジションから配列番号に戻す
        Vector2Int idx = new Vector2Int((int)tile.transform.position.x + GameSceneDirector.TILE_X / 2, (int)tile.transform.position.z + GameSceneDirector.TILE_Y / 2);

        // 新しい場所へ移動
        Vector3 pos = tile.transform.position;
        pos.y = 1.35f;
        transform.position = pos;

        // 移動状態リセット
        Status.Clear();

        // アンパッサン等処理
        if (TYPE.PAWN == Type)
        {
            // 縦に2タイル進んだ時
            if (1 < Mathf.Abs(idx.y - Pos.y))
            {
                Status.Add(STATUS.EN_PASSANT);

                // 移動した一歩手前に残像が残る
                int dir = -1;
                if (1 == Player) dir = 1;

                Pos.y = idx.y + dir;
            }

        }

        // キャスリング
        if (TYPE.KING == Type)
        {
            // 横に2タイル進んだら
            if (1 < idx.x - Pos.x)
            {
                Status.Add(STATUS.KSIDE_CASTLING);
            }

            if (-1 > idx.x - Pos.x)
            {
                Status.Add(STATUS.QSIDE_CASTLING);
            }
        }

        // インデックスの更新
        OldPos = Pos;
        Pos = idx;

        // 置いてからのターンをリセット
        ProgressTurnCount = 0;
    }

    // 前回移動してからのターンをカウント
    public void ProgressTurn()
    {
        // 初動は無視
        if (0 > ProgressTurnCount) return;

        ProgressTurnCount++;

        // アンパッサンフラグチェック
        if (TYPE.PAWN == Type)
        {
            if (1 < ProgressTurnCount)
            {
                Status.Remove(STATUS.EN_PASSANT);
            }
        }
    }

    // 相手のアンパッサン状態のユニットを返す
    UnitController getEnPassantUnit(UnitController[,] units, Vector2Int pos)
    {
        foreach (var v in units)
        {
            if (null == v) continue;
            if (Player == v.Player) continue;
            if (!v.Status.Contains(STATUS.EN_PASSANT)) continue;

            if (v.OldPos == pos) return v;
        }

        return null;
    }

    // 今回のターンのチェック状態をセット(キング専用)
    public void SetCheckStatus(bool flag = true)
    {
        Status.Remove(STATUS.CHECK);
        if (flag) Status.Add(STATUS.CHECK);
    }
}
