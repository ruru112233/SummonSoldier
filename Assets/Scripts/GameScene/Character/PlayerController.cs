using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Prms
{
    public int Hp { get { return hp; }  set { hp = value; } }

    public int At { get { return at; }  set { at = value; } }

    public int Df { get { return df; }  set { df = value; } }

    public int Speed { get { return speed; }  set { speed = value; } }

    public int Cost { get { return cost; } set { cost = value; } }

    EnemyController enemyTarget = null;

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
            if (ChildCheck.AllCountCheck(enemyPanel)) AllAttack();
        }
        else
        {
            Debug.Log("�ߋ���");
            if (ChildCheck.FrontCountCheck(enemyPanel)&&
                ChildCheck.FrontCheck(this.transform)) 
                FrontAttack();
        }

        // �I�u�W�F�N�g�̐��ɕύX���������ꍇ�A�O�q�ړ��̏���������
        if (ObjCountCheck()) PositionCheck.PositionChenge(myTransform, 0f);

        // HP0�ɂȂ�����I�u�W�F�N�g���폜
        if (Hp <= 0) Destroy(gameObject);
    }

    // �O�q����I�����čU��
    void FrontAttack()
    {
        time += Time.deltaTime;

        if (time > waitTime)
        {
            time = 0;
            waitTime = CalcScript.AttackTime(Speed);

            anime.SetTrigger("attack");

            enemysObj.Clear();

            for (int i = 0; i < 3; i++)
            {
                GameObject panel = enemyPanel.panel[i];

                if (panel.transform.childCount != 0)
                {
                    // �G�l�~�[�̃I�u�W�F�N�g���i�[
                    enemysObj.Add(SetObj(panel));
                }
            }

            EnemyController enemyTarget = EnemyTarget(enemysObj);

            // �U������
            StartCoroutine(enemyTarget.DamageText(CalcScript.DamagePoint(at, df)));
            myTransform.Rotate(0, -1.0f, 0);
        }
    }


    // �S�̂���I�����čU��
    void AllAttack()
    {
        time += Time.deltaTime;

        if (time > waitTime)
        {
            time = 0;
            waitTime = CalcScript.AttackTime(Speed);

            anime.SetTrigger("attack");

            enemysObj.Clear();

            // �G�l�~�[�̃I�u�W�F�N�g���i�[
            foreach (GameObject panel in enemyPanel.panel)
            {
                enemysObj.Add(SetObj(panel));
            }

            EnemyController enemyTarget = EnemyTarget(enemysObj);

            // �U������
            StartCoroutine(enemyTarget.DamageText(CalcScript.DamagePoint(at, df)));
            myTransform.Rotate(0, -1.0f, 0);
        }
    }

    // �^�[�Q�b�g�I��
    EnemyController EnemyTarget(List<GameObject> objs)
    {
        int r = Random.Range(0, objs.Count);

        EnemyController enemy = objs[r].GetComponent<EnemyController>();

        return enemy;
    }

    
    public IEnumerator DamageText(int damage)
    {
        
        text.GetComponent<Text>().text = damage.ToString();
        
        text.transform.position = DamageTextPos(this.transform);

        text.SetActive(true);

        Hp -= damage;

        yield return new WaitForSeconds(0.5f);

        if (text != null)
        {
            text.SetActive(false);
        }

    }

}
