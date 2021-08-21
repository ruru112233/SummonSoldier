using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class CalcScript
{

    // �_���[�W�ʂ̌v�Z
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

    // �X�s�[�h�ɂ��U���^�C�~���O�̌v�Z
    public static float AttackTime(int speed, float correction)
    {
        float defaltTime = 5.0f * correction;

        float randTime = Random.Range(speed * 0.01f - 0.3f, speed * 0.01f + 0.3f);

        float attackTime = defaltTime - randTime;

        if (attackTime <= 0.3f)
        {
            attackTime = 0.3f;
        }

        return attackTime;
    }
    

    


}
