using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public UnitManager player;
    public UnitManager enemy;

    // Start is called before the first frame update
    void Start()
    {
        player.Attack(enemy);
        enemy.Attack(player);
    }

}
