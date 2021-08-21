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
        // �U���^�C�v�ɂ���čU���͈͂𕪂���
        
        if (ATTACK_TYPE.SINGLE_RANGE == attack_type)
        {
            // �P��
            if (longRangeFlag)
            {
                Debug.Log("������");
                if (ChildCheck.AllCountCheck(enemyPanel)) AllAttack(enemyPanel, enemysObj);
            }
            else
            {
                Debug.Log("�ߋ���");
                if (ChildCheck.FrontCountCheck(enemyPanel) &&
                    ChildCheck.FrontCheck(this.transform))
                    FrontAttack(enemyPanel, enemysObj);
            }
        }
        else if(ATTACK_TYPE.COLUMN_RANGE == attack_type)
        {
            // �c���
            ColumnAttack(enemyPanel, enemysObj);

        }
        else if(ATTACK_TYPE.ROW_RANGE == attack_type)
        {
            // �����
        }
        else if (ATTACK_TYPE.ALL_RANGE == attack_type)
        {
            // �S��
        }

        // �I�u�W�F�N�g�̐��ɕύX���������ꍇ�A�O�q�ړ��̏���������
        if (ObjCountCheck()) PositionCheck.PositionChenge(myTransform, 0f);

        // HP0�ɂȂ�����I�u�W�F�N�g���폜
        if (Hp <= 0) Destroy(gameObject);
    }

    // �c1����U��
    protected void ColumnAttack(SummonPanelList panels, List<GameObject> objs)
    {
        time += Time.deltaTime;

        if (time > waitTime)
        {
            time = 0;
            waitTime = CalcScript.AttackTime(Speed);

            anime.SetTrigger("attack");

            objs.Clear();

            int columnNo = ColumnTarget();

            foreach (GameObject panel in panels.panel)
            {
                if (panel.transform.childCount != 0)
                {
                    objs.Add(SetObj(panel));
                }
            }

            List<CharaController> targets = CoulumTargets(objs, columnNo);

            // �U������
            foreach (CharaController target in targets)
            {
                StartCoroutine(target.DamageText(CalcScript.DamagePoint(At, Df)));
                myTransform.Rotate(0, -1.0f, 0);
            }
        }
    }


    // �^�[�Q�b�g�I��i��j
    protected int ColumnTarget()
    {
        List<int> enemyTargetList = ChildCheck.ColumnNoCheck(enemyPanel);

        int randNo = Random.Range(0, enemyTargetList.Count);
        int targetColumn = enemyTargetList[randNo];

        return targetColumn;
    }

    List<CharaController> CoulumTargets(List<GameObject> objs, int columnNo)
    {
        List<CharaController> charas = new List<CharaController>();
        CharaController chara = new CharaController();

        switch (columnNo)
        {
            case 1:
                foreach (GameObject obj in objs)
                {
                    if (obj.transform.parent.name == "SummonPanel1" ||
                        obj.transform.parent.name == "SummonPanel4")
                    {
                        chara = obj.GetComponent<CharaController>();
                        charas.Add(chara);
                    }

                }
                break;
            case 2:
                foreach (GameObject obj in objs)
                {
                    if (obj.transform.parent.name == "SummonPanel2" ||
                        obj.transform.parent.name == "SummonPanel5")
                    {
                        chara = obj.GetComponent<CharaController>();
                        charas.Add(chara);
                    }

                }
                break;
            case 3:
                foreach (GameObject obj in objs)
                {
                    if (obj.transform.parent.name == "SummonPanel3" ||
                        obj.transform.parent.name == "SummonPanel6")
                    {
                        chara = obj.GetComponent<CharaController>();
                        charas.Add(chara);
                    }

                }
                break;
        }

        return charas;
    }

}
