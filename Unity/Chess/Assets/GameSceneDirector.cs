using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    };

    // UI関連
    GameObject txtTurnInfo;
    GameObject txtResultInfo;
    GameObject btnApply;
    GameObject btnCancel;

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
    }

    // Update is called once per frame
    void Update()
    {

    }

    GameObject getPrefabUnit(int player, int type)
    {
        int idx = type - 1;

        if (0 > idx) return null;

        GameObject prefab = prefabWhiteUnits[idx];
        if (1 == player) prefab = prefabBlackUnits[idx];

        return prefab;
    }
}
