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

    }

    // Update is called once per frame
    void Update()
    {

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
            int no = Random.Range(0, range);

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
}