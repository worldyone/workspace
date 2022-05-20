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
