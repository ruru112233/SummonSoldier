using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : CharaController
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        text.SetActive(false);

        PositionCheck.PositionChenge(myTransform, 0f);

        waitTime = CalcScript.AttackTime(Speed);
    }

    public override void Update()
    {
        base.Update();

        // ���̏����͏d�����H
        startTextPos = new Vector3(myTransform.position.x, myTransform.position.y + 2.0f, myTransform.position.z);

        // �G�̃p�l���ɃI�u�W�F�N�g���������ꍇ�A�U������
        if (longRangeFlag)
        {
            Debug.Log("������");
            if (ChildCheck.AllCountCheck(enemyPanel)) AllAttack(enemyPanel, enemysObj);
        }
        else
        {
            Debug.Log("�ߋ���");
            if (ChildCheck.FrontCountCheck(enemyPanel)&&
                ChildCheck.FrontCheck(this.transform)) 
                FrontAttack(enemyPanel, enemysObj);
        }

        // �I�u�W�F�N�g�̐��ɕύX���������ꍇ�A�O�q�ړ��̏���������
        if (ObjCountCheck()) PositionCheck.PositionChenge(myTransform, 0f);

        // HP0�ɂȂ�����I�u�W�F�N�g���폜
        if (Hp <= 0) Destroy(gameObject);
    }

    
}
