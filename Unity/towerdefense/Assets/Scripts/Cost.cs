using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// コスト計算するクラスメソッドのみのクラス
public class Cost
{
    /// タワーを買うために必要なコストを取得する
    public static int TowerProduction()
    {
        // 生存数を取得
        int num = Tower.parent.Count();

        // タワー生産コスト = 8 * (1.3 ^ タワーの存在数)
        int basic = 8;
        float ratio = Mathf.Pow(1.3f, num);

        // 小数点を切り捨て
        return (int)(basic * ratio);
    }
}
