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
            // �G�t�F�N�g����
            Vector3 targetPos = target.transform.position;
            if (effectPrefab) Instantiate(effectPrefab, targetPos, Quaternion.identity);
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
                if (panel.transform.childCount != 0)
                {
                    objs.Add(SetObj(panel));
                }
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

    // �^�[�Q�b�g��CharaController���i�[���ĕԂ�
    protected List<CharaController> CoulumTargets(List<GameObject> objs, int columnNo)
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

            int columnNo = ColumnTarget(panels);

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
            }
            myTransform.Rotate(0, -1.0f, 0);
        }
    }


    // �^�[�Q�b�g�I��i��j
    protected int ColumnTarget(SummonPanelList panel)
    {
        List<int> targetList = ChildCheck.ColumnNoCheck(panel);

        int randNo = Random.Range(0, targetList.Count);
        int targetColumn = targetList[randNo];

        return targetColumn;
    }

    protected List<CharaController> RowTargets(List<GameObject> objs, int rowNo)
    {
        List<CharaController> charas = new List<CharaController>();
        CharaController chara = new CharaController();

        switch (rowNo)
        {
            case 1:
                foreach (GameObject obj in objs)
                {
                    if (obj.transform.parent.name == "SummonPanel1" ||
                        obj.transform.parent.name == "SummonPanel2" ||
                        obj.transform.parent.name == "SummonPanel3")
                    {
                        chara = obj.GetComponent<CharaController>();
                        charas.Add(chara);
                    }

                }
                break;
            case 2:
                foreach (GameObject obj in objs)
                {
                    if (obj.transform.parent.name == "SummonPanel4" ||
                        obj.transform.parent.name == "SummonPanel5" ||
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

    // �^�[�Q�b�g�I��i�s�j
    protected int RowTarget(SummonPanelList panel)
    {
        List<int> targetList = ChildCheck.RowNoCheck(panel);

        int randNo = Random.Range(0, targetList.Count);
        int targetRow = targetList[randNo];

        return targetRow;
    }

    // ����s�i�O�q�j�֍U��
    protected void FrontRowAttack(SummonPanelList panels, List<GameObject> objs)
    {
        time += Time.deltaTime;

        if (time > waitTime)
        {
            time = 0;
            waitTime = CalcScript.AttackTime(Speed);

            anime.SetTrigger("attack");

            objs.Clear();

            foreach (GameObject panel in panels.panel)
            {
                if (panel.transform.childCount != 0)
                {
                    objs.Add(SetObj(panel));
                }
            }

            List<CharaController> targets = RowTargets(objs, 1);

            // �U������
            foreach (CharaController target in targets)
            {
                StartCoroutine(target.DamageText(CalcScript.DamagePoint(At, Df)));
            }
            myTransform.Rotate(0, -1.0f, 0);
        }
    }

    // ��1�s�i�O�q�E��q�j
    protected void AllRowAttack(SummonPanelList panels, List<GameObject> objs)
    {
        time += Time.deltaTime;

        if (time > waitTime)
        {
            time = 0;
            waitTime = CalcScript.AttackTime(Speed);

            anime.SetTrigger("attack");

            objs.Clear();

            int rowNo = RowTarget(panels);

            foreach (GameObject panel in panels.panel)
            {
                if (panel.transform.childCount != 0)
                {
                    objs.Add(SetObj(panel));
                }
            }

            List<CharaController> targets = RowTargets(objs, rowNo);

            // �U������
            foreach (CharaController target in targets)
            {
                StartCoroutine(target.DamageText(CalcScript.DamagePoint(At, Df)));
            }
            myTransform.Rotate(0, -1.0f, 0);
        }
    }

    // �S�̂̃^�[�Q�b�g�ݒ�
    protected List<CharaController> AllTargets(List<GameObject> objs)
    {
        List<CharaController> charas = new List<CharaController>();
        CharaController chara = new CharaController();

        foreach (GameObject obj in objs)
        {
            chara = obj.GetComponent<CharaController>();
            charas.Add(chara);
        }

        return charas;
    }

    // �S�̍U��
    protected void AllRangeAttack(SummonPanelList panels, List<GameObject> objs)
    {
        time += Time.deltaTime;

        if (time > waitTime)
        {
            time = 0;
            waitTime = CalcScript.AttackTime(Speed);

            anime.SetTrigger("attack");

            objs.Clear();

            int rowNo = RowTarget(panels);

            foreach (GameObject panel in panels.panel)
            {
                if (panel.transform.childCount != 0)
                {
                    objs.Add(SetObj(panel));
                }
            }

            List<CharaController> targets = AllTargets(objs);

            // �U������
            foreach (CharaController target in targets)
            {
                StartCoroutine(target.DamageText(CalcScript.DamagePoint(At, Df)));
            }
            myTransform.Rotate(0, -1.0f, 0);
        }
    }
}
