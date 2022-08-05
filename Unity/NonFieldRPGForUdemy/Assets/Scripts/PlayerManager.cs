using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int hp;
    public int at;

    // 攻撃
    public void Attack(EnemyManager enemy)
    {
        enemy.Damage(at);
    }

    // ダメージ
    public void Damage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {

        }

    }
}
