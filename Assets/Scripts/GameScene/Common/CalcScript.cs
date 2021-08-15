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

        int damage = randAt * 3 - randDf * 3;

        if (damage <= 3)
        {
            damage = Random.Range(0, 4);
        }

        return damage;
    }

    // スピードによる攻撃速度の計算
    public static float AttackTime(int speed)
    {
        float defaltTime = 5.0f;

        float randTime = Random.Range(speed * 0.01f - 0.3f, speed * 0.01f + 0.3f);

        float attackTime = defaltTime - randTime;

        if (attackTime <= 0.3f)
        {
            attackTime = 0.3f;
        }

        return attackTime;
    }
    

    


}
