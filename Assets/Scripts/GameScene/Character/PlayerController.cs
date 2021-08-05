using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerController : Prms
{
    public int Hp { get { return hp; }  set { hp = value; } }

    public int At { get { return at; }  set { at = value; } }

    public int Df { get { return df; }  set { df = value; } }

    public int Speed { get { return speed; }  set { speed = value; } }

    // 攻撃タイミング
    float waitTime = 0;
    float time = 0;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        waitTime = CalcScript.AttackTime(Speed);
    }

    public override void Update()
    {
        base.Update();

        time += Time.deltaTime;

        Debug.Log(transform.rotation);

        if (time > waitTime)
        {
            Debug.Log("プレイヤーの攻撃");
            time = 0;
            Attack();
            waitTime = CalcScript.AttackTime(Speed);
        }

        if (Hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    // 攻撃
    void Attack()
    {
        anime.SetTrigger("attack");

        enemysObj.Clear();

        Debug.Log("enemyPanel.panel>>>:" + enemyPanel.panel);

        // エネミーのオブジェクトを格納
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

        // 攻撃処理
        enemyTarget.DamageText(CalcScript.DamagePoint(at, df));

        //await Task.Delay(500);

        this.transform.Rotate(0, -1.0f, 0);
    }

    // ターゲット選定
    EnemyController EnemyTarget(List<GameObject> objs)
    {
        int r = Random.Range(0, objs.Count);

        EnemyController enemy = objs[r].GetComponent<EnemyController>();

        return enemy;
    }

    



}
