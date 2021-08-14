using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class CalcScript
{

    // ダメージ量の計算
    public static int DamagePoint(int at, int df)
    {

        int randAt = Random.Range(at - 3, at + 3);
        int randDf = Random.Range(df - 2, df + 2);

        int damage = randAt * 3 - randDf * 2;

        if (damage <= 3)
        {
            damage = Random.Range(1, 4);
        }

        return damage;
    }

    // スピードによる攻撃速度の計算
    public static float AttackTime(int speed)
    {
        float randTime = Random.Range((speed - 4.0f), (speed + 3.5f));

        float attackTime = 100 - randTime * 3;

        if (attackTime < 1.0f)
        {
            attackTime = 1.0f;
        }

        return attackTime;
    }
    

    


}
