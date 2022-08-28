using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int hp;
    public int at;

    // 攻撃
    public int Attack(EnemyManager enemy)
    {
        int damage = enemy.Damage(at);
        return damage;
    }

    // ダメージ
    public int Damage(int damage)
    {
        hp -= damage;
        hp = Mathf.Max(hp, 0);

        return damage;
    }
}
