using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    // プレイヤー
    public bool[] isPlayer;
    Player[] player;
    int nowTurn;

    // ゲームモード
    enum MODE
    {
        NONE = -1,
        WAIT_TURN_START,
        MOVE_SELECT,
        FIELD_UPDATE,
        WAIT_TURN_END,
        TURN_CHANGE,
    }

    // モード
    MODE nowMode;
    MODE nextMode;

    // ウェイトの定義
    float waitTime;

    // フィールド
    int[,] tileData = new int[,]
    {
        // 手前側
        {0,8,0,0,0,0,8,0},
        {0,2,1,1,1,1,2,0},
        {0,1,1,1,1,1,1,0},
        {0,1,1,1,1,1,1,0},
        {0,1,1,1,1,1,1,0},
        {0,1,1,1,1,1,1,0},
        {0,2,1,1,1,1,2,0},
        {0,4,0,0,0,0,4,0},
        // 奥側
    };

    // ユニット初期配置
    int[,] initUnitData = new int[,]
    {
        // 手前側
        {0,0,0,0,0,0,0,0},
        {0,0,1,1,1,1,0,0},
        {0,0,1,1,1,1,0,0},
        {0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0},
        {0,0,2,2,2,2,0,0},
        {0,0,2,2,2,2,0,0},
        {0,0,0,0,0,0,0,0},
        // 奥側
    };

    // ユニット最大数
    const int UNIT_MAX = 8;

    // フィールド上のユニット
    List<GameObject>[,] unitData;

    // ユニット選択モードで使う
    GameObject selectUnit;
    int oldX, oldY;

    // ボタンなどのGameObject
    GameObject btnTurnEnd;
    GameObject txtInfo;
    GameObject objCamera;

    // Start is called before the first frame update
    void Start()
    {
        // 画面上のオブジェクト取得
        txtInfo = GameObject.Find("Info");
        btnTurnEnd = GameObject.Find("Button (1)");
        objCamera = GameObject.Find("Main Camera");

        txtInfo.GetComponent<UnityEngine.UI.Text>().text = "";

        // ユニットとフィールドを作成
        List<int> p1rnd = getRandomList(UNIT_MAX, UNIT_MAX / 2);
        List<int> p2rnd = getRandomList(UNIT_MAX, UNIT_MAX / 2);
        int p1unit = 0;
        int p2unit = 0;

        unitData = new List<GameObject>[tileData.GetLength(0), tileData.GetLength(1)];

        // プレイヤー設定
        player = new Player[2]; // 2人対戦
        player[0] = new Player(isPlayer[0], 1);
        player[1] = new Player(isPlayer[0], 2);

        // タイルとユニットの初期化
        for (int i = 0; i < tileData.GetLength(0); i++)
        {
            for (int j = 0; j < tileData.GetLength(1); j++)
            {
                float x = j - (tileData.GetLength(1) / 2 - 0.5f);
                float y = i - (tileData.GetLength(0) / 2 - 0.5f);

                // タイルの配置
                string resname = "";

                // 1:通常タイル 2:ゴールタイル 4:1Pのゴール 8:2Pのゴール
                int no = tileData[i, j];
                if (4 == no || 8 == no) no = 5;

                resname = "Cube (" + no + ")";

                resourcesInstantiate(resname, new Vector3(x, 0.5f, y), Quaternion.identity);

                // ユニット配置
                unitData[i, j] = new List<GameObject>();

                Vector3 angle = new Vector3(0, 0, 0);
                int unittype = UnitController.TYPE_BLUE;

                List<int> unitrnd = new List<int>();
                int unitnum = -1;
                resname = "Unit1";

                // 1Pユニット配置
                if (1 == initUnitData[i, j])
                {
                    unitrnd = p1rnd;
                    unitnum = p1unit++;
                }
                else if (2 == initUnitData[i, j])
                {
                    unitrnd = p2rnd;
                    unitnum = p2unit++;
                    angle.y = 180;
                }
                else
                {
                    resname = "";
                }

                // 赤のユニットを配置するかチェック
                if (-1 < unitrnd.IndexOf(unitnum))
                {
                    resname = "Unit2";
                    unittype = UnitController.TYPE_RED;
                }

                GameObject unit = resourcesInstantiate(resname, new Vector3(x, 1f, y), Quaternion.Euler(angle));

                if (null != unit)
                {
                    unit.GetComponent<UnitController>().PlayerNo = initUnitData[i, j];
                    unit.GetComponent<UnitController>().Type = unittype;
                    unitData[i, j].Add(unit);
                }
            }
        }

        nowTurn = 0;
        nextMode = MODE.MOVE_SELECT;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWait()) return;

        mode();

        if (MODE.NONE != nextMode) initMode(nextMode);

    }

    // ウェイト
    bool isWait()
    {
        bool ret = false;

        if (0 < waitTime)
        {
            waitTime -= Time.deltaTime;
            ret = true;
        }

        return ret;
    }

    // メインモード
    void mode()
    {
        if (MODE.MOVE_SELECT == nowMode)
        {
            selectMode();
        }
        else if (MODE.FIELD_UPDATE == nowMode)
        {
            fieldUpdateMode();
        }
        else if (MODE.TURN_CHANGE == nowMode)
        {
            turnChangeMode();
        }

    }

    // 次のモードの準備
    void initMode(MODE next)
    {
        updateHp();

        if (MODE.MOVE_SELECT == next)
        {
            selectUnit = null;
        }

        nowMode = next;
        nextMode = MODE.NONE;
    }

    void selectMode()
    {
        GameObject hitobj = null;

        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                hitobj = hit.collider.gameObject;
            }

        }

        if (null == hitobj) return;

        Vector3 pos = hitobj.transform.position;
        int x = (int)(pos.x + (tileData.GetLength(1) / 2 - 0.5f));
        int y = (int)(pos.z + (tileData.GetLength(0) / 2 - 0.5f));

        // ユニット選択
        if (0 < unitData[y, x].Count
            && player[nowTurn].PlayerNo == unitData[y, x][0].GetComponent<UnitController>().PlayerNo)
        {
            if (null != selectUnit)
            {
                selectUnit.GetComponent<UnitController>().Select(false);
            }

            selectUnit = unitData[y, x][0];
            oldX = x;
            oldY = y;

            selectUnit.GetComponent<UnitController>().Select();
        }

        // 移動先タイル選択
        else if (null != selectUnit)
        {
            if (movableTile(oldX, oldY, x, y))
            {
                unitData[oldY, oldX].Clear();

                pos.y += 0.5f;
                selectUnit.transform.position = pos;

                unitData[y, x].Add(selectUnit);

                nextMode = MODE.FIELD_UPDATE;
            }
        }
    }

    void fieldUpdateMode()
    {
        for (int i = 0; i < unitData.GetLength(0); i++)
        {
            for (int j = 0; j < unitData.GetLength(1); j++)
            {
                // ゴールしてたら消す
                if (1 == unitData[i, j].Count && player[nowTurn].PlayerNo * 4 == tileData[i, j])
                {
                    // 青だったら勝利
                    if (UnitController.TYPE_BLUE == unitData[i, j][0].GetComponent<UnitController>().Type)
                    {
                        player[nowTurn].IsGoal = true;
                    }

                    Destroy(unitData[i, j][0]);
                    unitData[i, j].RemoveAt(0);
                }

                // 2つ置いてあったら古いユニットを消す
                if (1 < unitData[i, j].Count)
                {
                    // 赤ならダメージ
                    if (UnitController.TYPE_RED == unitData[i, j][0].GetComponent<UnitController>().Type)
                    {
                        player[nowTurn].Hp--;
                        waitTime = 1.5f;
                    }
                    // 青ならスコア加算
                    else
                    {
                        player[nowTurn].Score++;
                        waitTime = 1.5f;
                    }

                    Destroy(unitData[i, j][0]);
                    unitData[i, j].RemoveAt(0);
                }
            }
        }

        nextMode = MODE.TURN_CHANGE;
    }

    void turnChangeMode()
    {
        // 次のターンへ
        nowTurn = getNextTurn();
        nextMode = MODE.MOVE_SELECT;
    }

    int getNextTurn()
    {
        int ret = nowTurn;

        ret++;
        if (1 < ret) ret = 0;

        return ret;
    }

    // ランダムの被らない数値をリストで返す
    List<int> getRandomList(int range = 8, int count = 4)
    {
        List<int> ret = new List<int>();

        if (range < count)
        {
            Debug.LogError("リスト作成エラー");
            return ret;
        }

        while (true)
        {
            int no = UnityEngine.Random.Range(0, range);

            // 入ってないならば追加する
            if (-1 == ret.IndexOf(no))
            {
                ret.Add(no);
            }

            if (count <= ret.Count)
            {
                break;
            }
        }

        return ret;
    }

    // Resourcesからオブジェクトを生成
    GameObject resourcesInstantiate(string name, Vector3 pos, Quaternion ang)
    {
        GameObject prefab = (GameObject)Resources.Load(name);

        if (null == prefab)
        {
            return null;
        }

        GameObject ret = Instantiate(prefab, pos, ang);
        return ret;
    }

    // そこへ移動可能かどうか
    bool movableTile(int oldx, int oldy, int x, int y)
    {
        bool ret = false;

        // 差分を取得
        int dx = Mathf.Abs(oldx - x);
        int dy = Mathf.Abs(oldy - y);

        // 斜めは進めない
        if (1 < dx + dy)
        {
            ret = false;
        }
        // 壁以外
        else if (1 == tileData[y, x]
            || 2 == tileData[y, x]
            || player[nowTurn].PlayerNo * 4 == tileData[y, x])
        {
            // 誰も乗っていない
            if (0 == unitData[y, x].Count)
            {
                ret = true;

            }
            // 誰か乗っている
            else
            {
                if (unitData[y, x][0].GetComponent<UnitController>().PlayerNo != player[nowTurn].PlayerNo)
                {
                    ret = true;
                }
            }
        }

        return ret;
    }

    // HPのアップデート
    void updateHp()
    {
        for (int i = 0; i < player.Length; i++)
        {
            GameObject obj = GameObject.Find(player[i].PlayerNo + "PText");
            print(obj.name);
            if (null == obj) continue;
            string t = player[i].GetPlayerName() + " Hp : " + player[i].Hp + " Score : " + player[i].Score;

            obj.GetComponent<UnityEngine.UI.Text>().text = t;
        }
    }
}
