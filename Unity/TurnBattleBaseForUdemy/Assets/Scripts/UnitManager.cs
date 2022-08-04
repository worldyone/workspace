using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public int hp;
    public int at;

    public void Attack(UnitManager target)
    {
        target.Damage(at);
    }

    void Damage(int damage)
    {
        hp -= damage;
        Debug.Log(name + "は" + damage + "ダメージを受けた");
    }
}
