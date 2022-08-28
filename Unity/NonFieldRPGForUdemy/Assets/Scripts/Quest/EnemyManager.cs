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
    public GameObject hitEffect;

    public int Attack(PlayerManager player)
    {
        int damage = player.Damage(at);
        return damage;
    }

    public int Damage(int damage)
    {
        Instantiate(hitEffect, this.transform, false);
        transform.DOShakePosition(0.3f, 0.5f, 20, 0, false, true);
        hp -= damage;
        hp = Math.Max(hp, 0);

        return damage;
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
