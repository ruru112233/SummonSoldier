using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaController : Prms
{
    public int Hp { get { return hp; } set { hp = value; } }

    public int At { get { return at; } set { at = value; } }

    public int Df { get { return df; } set { df = value; } }

    public int Speed { get { return speed; } set { speed = value; } }

    public int Cost { get { return cost; } set { cost = value; } }

    // �I�u�W�F�N�g�������Ă���
    protected int objCount = 0;
    int count1 = 0;
    int count2 = 0;

    // �A�j���[�V����
    protected Animator anime = new Animator();

    // �����X�^�[���i�[����
    protected List<GameObject> playersObj = new List<GameObject>();
    protected List<GameObject> enemysObj = new List<GameObject>();

    protected SummonPanelList playerPanel = null
                            , enemyPanel = null;

    // �����ʒu
    protected Transform myTransform;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        anime = this.GetComponent<Animator>();
        myTransform = this.transform;

        playerPanel = GameObject.FindWithTag("PlayerPanel").GetComponent<SummonPanelList>();
        enemyPanel = GameObject.FindWithTag("EnemyPanel").GetComponent<SummonPanelList>();

    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        StartAngle();

        objCount = ChildCheck.ChildObjCount();
    }


    // �L�����̃A���O����K���ʒu�ɂ���
    protected void StartAngle()
    {
        Vector3 localAngle = myTransform.localEulerAngles;
        localAngle.y = 0f;
        myTransform.localEulerAngles = localAngle;
    }

    // �S�̂̃I�u�W�F�N�g�̐��ɕω������������ǂ���
    protected bool ObjCountCheck()
    {
        bool check = false;

        if (Time.frameCount % 2 == 0)
        {
            count1 = objCount;
        }
        else
        {
            count2 = objCount;
        }

        if (count1 != count2)
        {
            check = true;
        }

        return check;
    }

    // �e�L�X�g�̃|�W�V�������Z�b�g����
    protected Vector3 DamageTextPos(Transform transform)
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 2.0f, transform.position.z);

        return pos;
    }


    // �����X�^�[�I�u�W�F�N�g���Z�b�g����
    protected GameObject SetObj(GameObject panel)
    {

        Transform t = panel.GetComponentInChildren<Transform>();
        GameObject obj = t.GetChild(0).gameObject;

        return obj;

    }

    // �O�q����I�����čU��
    protected void FrontAttack(SummonPanelList panels, List<GameObject> objs)
    {
        time += Time.deltaTime;

        if (time > waitTime)
        {
            time = 0;
            waitTime = CalcScript.AttackTime(Speed);

            anime.SetTrigger("attack");

            objs.Clear();

            for (int i = 0; i < 3; i++)
            {
                GameObject panel = panels.panel[i];

                if (panel.transform.childCount != 0)
                {
                    // �p�l���I�u�W�F�N�g���i�[
                    objs.Add(SetObj(panel));
                }
            }

            CharaController target = Target(objs);

            // �U������
            StartCoroutine(target.DamageText(CalcScript.DamagePoint(At, Df)));
            myTransform.Rotate(0, -1.0f, 0);
        }
    }


    // �S�̂���I�����čU��
    protected void AllAttack(SummonPanelList panels, List<GameObject> objs)
    {
        time += Time.deltaTime;

        if (time > waitTime)
        {
            time = 0;
            waitTime = CalcScript.AttackTime(Speed);

            anime.SetTrigger("attack");

            objs.Clear();

            // �p�l���I�u�W�F�N�g���i�[
            foreach (GameObject panel in panels.panel)
            {
                objs.Add(SetObj(panel));
            }

            CharaController target = Target(objs);

            // �U������
            StartCoroutine(target.DamageText(CalcScript.DamagePoint(At, Df)));
            myTransform.Rotate(0, -1.0f, 0);
        }
    }

    // �^�[�Q�b�g�I��
    protected CharaController Target(List<GameObject> objs)
    {
        int r = Random.Range(0, objs.Count);

        CharaController chara = objs[r].GetComponent<CharaController>();

        return chara;
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
