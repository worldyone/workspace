using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public bool IsPlayer;
    public int PlayerNo;

    // ゴールタイルに乗っているかどうか
    public bool IsGoal;
    // 相手の赤いコマを取ったらHP減少
    public int Hp = 4;
    // 相手の青いコマを取ったら得点
    public int Score;

    // コンストラクタ
    public Player(bool isPlayer, int playerno)
    {
        this.IsPlayer = isPlayer;
        this.PlayerNo = playerno;
    }

    // プレイヤーの名前を返す
    public string GetPlayerName()
    {
        string ret = "";

        string playerName = PlayerNo + "P";

        if (!IsPlayer)
        {
            playerName = "CPU";
        }

        ret = playerName;
        return ret;
    }

}
