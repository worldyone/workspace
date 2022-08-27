using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 敵を管理する(ステータス/クリック検出)
public class EnemyManager : MonoBehaviour
{
    // 関数登録
    Action tapAction; // クリックされたときに実行したい関数（外部から設定したい）
    public new string name;
    public int hp;
    public int at;

    public void Attack(PlayerManager player)
    {
        player.Damage(at);
    }

    public void Damage(int damage)
    {
        transform.DOShakePosition(0.3f, 0.5f, 20, 0, false, true);
        hp -= damage;
        hp = Math.Max(hp, 0);
    }

    // tapActionに関数を登録する関数
    public void AddEventListenerOnTap(Action action)
    {
        tapAction += action;
    }

    public void OnTap()
    {
        tapAction();
    }
}
