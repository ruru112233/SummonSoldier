using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : CharaController
{
    MasterController enemyMaster = null;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        text.SetActive(false);

        enemyMaster = GameObject.FindWithTag("MasterEnemy").GetComponent<MasterController>();

        PositionCheck.PositionChenge(myTransform, 0f);

    }

    public override void Update()
    {
        base.Update();

        // ���̏����͏d�����H
        startTextPos = new Vector3(myTransform.position.x, myTransform.position.y + 2.0f, myTransform.position.z);

        // �G�̃p�l���ɃI�u�W�F�N�g���������ꍇ�A�U������
        // �U���^�C�v�ɂ���čU���͈͂𕪂���
        
        if (ATTACK_TYPE.SINGLE_RANGE == attack_type)
        {
            // �P��
            if (longRangeFlag)
            {
                Debug.Log("������");
                if (ChildCheck.AllCountCheck(enemyPanel)) 
                    AllAttack(enemyPanel, enemysObj, At);
            }
            else
            {
                Debug.Log("�ߋ���");
                if (ChildCheck.FrontCountCheck(enemyPanel) &&
                    ChildCheck.FrontCheck(this.transform))
                    FrontAttack(enemyPanel, enemysObj, At);
            }
        }
        else if(ATTACK_TYPE.COLUMN_RANGE == attack_type)
        {
            // �c���
            if(ChildCheck.ColumnCheck(enemyPanel)) 
                ColumnAttack(enemyPanel, enemysObj, At);

        }
        else if(ATTACK_TYPE.ROW_RANGE == attack_type)
        {
            // �����
            if (longRangeFlag)
            {
                if(ChildCheck.RowCheck(enemyPanel)) 
                    AllRowAttack(enemyPanel, enemysObj, At);
            }
            else
            {
                if(ChildCheck.FrontRowCheck(enemyPanel)) 
                    FrontRowAttack(enemyPanel, enemysObj, At);
            }
        }
        else if (ATTACK_TYPE.ALL_RANGE == attack_type)
        {
            // �S��
            if (ChildCheck.AllCountCheck(enemyPanel))
                AllRangeAttack(enemyPanel, enemysObj, At);
        }

        // �I�u�W�F�N�g�̐��ɕύX���������ꍇ�A�O�q�ړ��̏���������
        if (ObjCountCheck()) PositionCheck.PositionChenge(myTransform, 0f);

        if (enemyMaster && ChildCheck.EnemyChildObjectCount() <= 0)
            MasterObjectAttack(enemyMaster, At);

        // HP0�ɂȂ�����I�u�W�F�N�g���폜
        if (Hp <= 0) Destroy(gameObject);
    }

}
