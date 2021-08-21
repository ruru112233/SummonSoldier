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

        // この処理は重いか？
        startTextPos = new Vector3(myTransform.position.x, myTransform.position.y + 2.0f, myTransform.position.z);

        // 敵のパネルにオブジェクトがあった場合、攻撃する
        // 攻撃タイプによって攻撃範囲を分ける
        
        if (ATTACK_TYPE.SINGLE_RANGE == attack_type)
        {
            // 単体
            if (longRangeFlag)
            {
                Debug.Log("遠距離");
                if (ChildCheck.AllCountCheck(enemyPanel)) AllAttack(enemyPanel, enemysObj);
            }
            else
            {
                Debug.Log("近距離");
                if (ChildCheck.FrontCountCheck(enemyPanel) &&
                    ChildCheck.FrontCheck(this.transform))
                    FrontAttack(enemyPanel, enemysObj);
            }
        }
        else if(ATTACK_TYPE.COLUMN_RANGE == attack_type)
        {
            // 縦一列
            ColumnAttack(enemyPanel, enemysObj);

        }
        else if(ATTACK_TYPE.ROW_RANGE == attack_type)
        {
            // 横一列
        }
        else if (ATTACK_TYPE.ALL_RANGE == attack_type)
        {
            // 全体
        }

        // オブジェクトの数に変更があった場合、前衛移動の処理をする
        if (ObjCountCheck()) PositionCheck.PositionChenge(myTransform, 0f);

        // HP0になったらオブジェクトを削除
        if (Hp <= 0) Destroy(gameObject);
    }

    // 縦1列を攻撃
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

            // 攻撃処理
            foreach (CharaController target in targets)
            {
                StartCoroutine(target.DamageText(CalcScript.DamagePoint(At, Df)));
                myTransform.Rotate(0, -1.0f, 0);
            }
        }
    }


    // ターゲット選定（列）
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
