using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParam
{
    public static int Hp()
    {
        // 1 + (Global.Wave / 3)
        return 1 + (Global.Wave / 3);
    }

    public static float Speed()
    {
        // 3 + (0.1 * Global.Wave数)
        return 3 + (0.1f * Global.Wave);
    }

    public static int Money()
    {
        if (Global.Wave < 5)
        {
            // Wave4以下は2
            return 2;
        }

        else
        {
            // Wave5以上は1
            return 1;
        }
    }

    public static int GenerationNumber()
    {
        return 5 + Global.Wave;
    }

    public static float GenerationInterval()
    {
        return 1.5f;
    }
}
