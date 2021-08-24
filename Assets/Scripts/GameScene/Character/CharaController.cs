using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaController : Prms
{
    public string UnitName { get { return unitName; } }

    public int Hp { get { return hp; } set { hp = value; } }

    public int At { get { return at; } set { at = value; } }

    public int Df { get { return df; } set { df = value; } }

    public int Speed { get { return speed; } set { speed = value; } }

    public string Explanatory { get { return explanatory; } }

    public int Cost { get { return cost; } set { cost = value; } }

    // オブジェクト数を入れておく
    protected int objCount = 0;
    int count1 = 0;
    int count2 = 0;

    // アタックタイプによる攻撃タイミングの補正値
    float speedCorrection = 1;

    // アニメーション
    protected Animator anime = new Animator();

    // モンスターを格納する
    protected List<GameObject> playersObj = new List<GameObject>();
    protected List<GameObject> enemysObj = new List<GameObject>();

    protected SummonPanelList playerPanel = null
                            , enemyPanel = null;

    // 初期位置
    protected Transform myTransform;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        anime = this.GetComponent<Animator>();
        myTransform = this.transform;

        // アタックタイプによる補正値をセット
        AttackTypeCor();

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


    // キャラのアングルを適正位置にする
    protected void StartAngle()
    {
        Vector3 localAngle = myTransform.localEulerAngles;
        localAngle.y = 0f;
        myTransform.localEulerAngles = localAngle;
    }

    // アタックタイプによる補正
    void AttackTypeCor()
    {
        if (ATTACK_TYPE.ROW_RANGE == attack_type)
        {
            speedCorrection = 1.1f;
        }
        else if (ATTACK_TYPE.COLUMN_RANGE == attack_type)
        {
            speedCorrection = 1.3f;
        }
        else if (ATTACK_TYPE.ALL_RANGE == attack_type)
        {
            speedCorrection = 1.5f;
        }
        else
        {
            speedCorrection = 1f;
        }

    }

    // 全体のオブジェクトの数に変化があったかどうか
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

    // テキストのポジションをセットする
    protected Vector3 DamageTextPos(Transform transform)
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 2.0f, transform.position.z);

        return pos;
    }


    // モンスターオブジェクトをセットする
    protected GameObject SetObj(GameObject panel)
    {

        Transform t = panel.GetComponentInChildren<Transform>();
        GameObject obj = t.GetChild(0).gameObject;

        return obj;

    }

    // 前衛から選択して攻撃
    protected void FrontAttack(SummonPanelList panels, List<GameObject> objs)
    {
        time += Time.deltaTime;

        if (time > waitTime)
        {
            time = 0;
            waitTime = CalcScript.AttackTime(Speed, speedCorrection);

            anime.SetTrigger("attack");

            objs.Clear();

            for (int i = 0; i < 3; i++)
            {
                GameObject panel = panels.panel[i];

                if (panel.transform.childCount != 0)
                {
                    // パネルオブジェクトを格納
                    objs.Add(SetObj(panel));
                }
            }

            CharaController target = Target(objs);

            // 攻撃処理
            StartCoroutine(target.DamageText(CalcScript.DamagePoint(At, Df)));
            // エフェクト処理
            EffectPool(target);
            myTransform.Rotate(0, -1.0f, 0);
        }
    }

    
    // 全体から選択して攻撃
    protected void AllAttack(SummonPanelList panels, List<GameObject> objs)
    {
        time += Time.deltaTime;

        if (time > waitTime)
        {
            time = 0;
            waitTime = CalcScript.AttackTime(Speed, speedCorrection);

            anime.SetTrigger("attack");

            objs.Clear();

            // パネルオブジェクトを格納
            foreach (GameObject panel in panels.panel)
            {
                if (panel.transform.childCount != 0)
                {
                    objs.Add(SetObj(panel));
                }
            }

            CharaController target = Target(objs);

            // 攻撃処理
            StartCoroutine(target.DamageText(CalcScript.DamagePoint(At, Df)));
            // エフェクト処理
            EffectPool(target);
            myTransform.Rotate(0, -1.0f, 0);
        }
    }

    // ターゲット選定
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

    // ターゲットのCharaControllerを格納して返す
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

    // 縦1列を攻撃
    protected void ColumnAttack(SummonPanelList panels, List<GameObject> objs)
    {
        time += Time.deltaTime;

        if (time > waitTime)
        {
            time = 0;
            waitTime = CalcScript.AttackTime(Speed, speedCorrection);

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

            // 攻撃処理
            foreach (CharaController target in targets)
            {
                StartCoroutine(target.DamageText(CalcScript.DamagePoint(At, Df)));
                // エフェクト処理
                EffectPool(target);
            }
            myTransform.Rotate(0, -1.0f, 0);
        }
    }


    // ターゲット選定（列）
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

    // ターゲット選定（行）
    protected int RowTarget(SummonPanelList panel)
    {
        List<int> targetList = ChildCheck.RowNoCheck(panel);

        int randNo = Random.Range(0, targetList.Count);
        int targetRow = targetList[randNo];

        return targetRow;
    }

    // 横一行（前衛）へ攻撃
    protected void FrontRowAttack(SummonPanelList panels, List<GameObject> objs)
    {
        time += Time.deltaTime;

        if (time > waitTime)
        {
            time = 0;
            waitTime = CalcScript.AttackTime(Speed, speedCorrection);

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

            // 攻撃処理
            foreach (CharaController target in targets)
            {
                StartCoroutine(target.DamageText(CalcScript.DamagePoint(At, Df)));
                // エフェクト処理
                EffectPool(target);
            }
            myTransform.Rotate(0, -1.0f, 0);
        }
    }

    // 横1行（前衛・後衛）
    protected void AllRowAttack(SummonPanelList panels, List<GameObject> objs)
    {
        time += Time.deltaTime;

        if (time > waitTime)
        {
            time = 0;
            waitTime = CalcScript.AttackTime(Speed, speedCorrection);

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

            // 攻撃処理
            foreach (CharaController target in targets)
            {
                StartCoroutine(target.DamageText(CalcScript.DamagePoint(At, Df)));
                // エフェクト処理
                EffectPool(target);
            }
            myTransform.Rotate(0, -1.0f, 0);
        }
    }

    // 全体のターゲット設定
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

    // 全体攻撃
    protected void AllRangeAttack(SummonPanelList panels, List<GameObject> objs)
    {
        time += Time.deltaTime;

        if (time > waitTime)
        {
            time = 0;
            waitTime = CalcScript.AttackTime(Speed, speedCorrection);

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

            // 攻撃処理
            foreach (CharaController target in targets)
            {
                StartCoroutine(target.DamageText(CalcScript.DamagePoint(At, Df)));
                // エフェクト処理
                EffectPool(target);
                //Vector3 targetPos = target.transform.position;
                //if (effectPrefab) Instantiate(effectPrefab, targetPos, Quaternion.identity);
            }
            myTransform.Rotate(0, -1.0f, 0);
        }
    }

    // エフェクトのオブジェクトプール
    void EffectPool(CharaController target)
    {
        foreach (Transform t in effectPrefab.transform)
        {
            if (!t.gameObject.activeSelf)
            {
                t.SetPositionAndRotation(target.transform.position, Quaternion.identity);
                t.gameObject.SetActive(true);
                return;
            }
        }

        GameObject obj = effectPrefab.transform.GetChild(0).gameObject;
        Instantiate(obj, target.transform.position, Quaternion.identity);

    }

    // masterオブジェクトの攻撃

}
