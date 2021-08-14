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

    EnemyController enemyTarget = null;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        startTextPos = text.transform.position;
        text.SetActive(false);

        waitTime = CalcScript.AttackTime(Speed);
    }

    public override void Update()
    {
        base.Update();

        // 敵のパネルにオブジェクトがあった場合、攻撃する
        if(EnemyCountCheck(enemyPanel) != 0) Attack();

        // HP0になったらオブジェクトを削除
        if (Hp <= 0) Destroy(gameObject);
    }

    // 攻撃
    void Attack()
    {
        time += Time.deltaTime;

        if (time > waitTime)
        {
            time = 0;
            waitTime = CalcScript.AttackTime(Speed);

            anime.SetTrigger("attack");

            enemysObj.Clear();

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
            StartCoroutine(enemyTarget.DamageText(CalcScript.DamagePoint(at, df)));
            myTransform.Rotate(0, -1.0f, 0);
        }
    }

    // ターゲット選定
    EnemyController EnemyTarget(List<GameObject> objs)
    {
        int r = Random.Range(0, objs.Count);

        EnemyController enemy = objs[r].GetComponent<EnemyController>();

        return enemy;
    }

    // 子要素に敵が存在するか確認
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
