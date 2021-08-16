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

        startTextPos = myTransform.position;
        startTextPos.y += 2.0f;
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
        if (EnemyCountCheck(enemyPanel) != 0) Attack();

        // �I�u�W�F�N�g�̐��ɕύX���������ꍇ�A�O�q�ړ��̏���������
        if (ObjCountCheck()) PositionCheck.PositionChenge(myTransform, 0f);

        // HP0�ɂȂ�����I�u�W�F�N�g���폜
        if (Hp <= 0) Destroy(gameObject);
    }

    // �U��
    void Attack()
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
                if (panel.transform.childCount != 0)
                {
                    Transform t = panel.GetComponentInChildren<Transform>();
                    GameObject obj = t.GetChild(0).gameObject;
                    enemysObj.Add(obj);
                }
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

    // �q�v�f�ɓG�����݂��邩�m�F
    int EnemyCountCheck(SummonPanelList enemyPanel)
    {
        int count = 0;

        foreach (GameObject panel in enemyPanel.panel)
        {
            if (panel.transform.childCount == 1)
            {
                count++;
            }
        }

        return count;
    }

    public IEnumerator DamageText(int damage)
    {
        text.SetActive(true);

        text.GetComponent<Text>().text = damage.ToString();
        text.transform.position = startTextPos;

        Hp -= damage;

        yield return new WaitForSeconds(0.5f);

        if (text != null)
        {
            text.SetActive(false);
        }

    }

}
