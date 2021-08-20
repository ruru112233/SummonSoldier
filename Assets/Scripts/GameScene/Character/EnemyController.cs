using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;

public class EnemyController : Prms
{
    public int Hp { get { return hp; } set { hp = value; } }

    public int At { get { return at; } set { at = value; } }

    public int Df { get { return df; } set { df = value; } }

    public int Speed { get { return speed; } set { speed = value; } }

    [SerializeField]
    private List<int> dropItemList = new List<int>();

    ItemCount itemCount;
    MasterData masterData;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        masterData = MasterData.instance;

        text.SetActive(false);

        // 下段に召喚され、上段が召喚されていなかったら上段に上げる
        PositionCheck.PositionChenge(myTransform, 1.8f);

        waitTime = CalcScript.AttackTime(Speed);


    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        // 敵のパネルにオブジェクトがあった場合、攻撃する
        if (longRangeFlag)
        {
            if (AllCountCheck(playerPanel)) AllAttack();
        }
        else
        {
            if (FrontCountCheck(playerPanel)) FrontAttack();
        }

        // オブジェクトの数に変更があった場合、前衛移動の処理をする
        if (ObjCountCheck()) PositionCheck.PositionChenge(myTransform, 1.8f);

        // HPが0になった時の処理
        if (Hp <= 0)
        {
            GetRandomItem();
            Destroy(gameObject);
        }
    }

    // 前衛から選択して攻撃
    void FrontAttack()
    {
        time += Time.deltaTime;

        if (time > waitTime)
        {
            time = 0;
            waitTime = CalcScript.AttackTime(Speed);

            anime.SetTrigger("attack");

            playersObj.Clear();

            for (int i = 0; i < 3; i++)
            {
                GameObject panel = playerPanel.panel[i];

                if (panel.transform.childCount != 0)
                {
                    // エネミーのオブジェクトを格納
                    playersObj.Add(SetObj(panel));
                }
            }

            PlayerController playerTarget = PlayerTarget(playersObj);

            // 攻撃処理
            StartCoroutine(playerTarget.DamageText(CalcScript.DamagePoint(at, df)));
            myTransform.Rotate(0, -1.0f, 0);
        }
    }

    // 全体を選択して攻撃
    void AllAttack()
    {
        time += Time.deltaTime;

        if (time > waitTime)
        {
            time = 0;
            waitTime = CalcScript.AttackTime(Speed);

            anime.SetTrigger("attack");

            playersObj.Clear();

            // エネミーのオブジェクトを格納
            foreach (GameObject panel in playerPanel.panel)
            {
                if (panel.transform.childCount != 0)
                {
                    playersObj.Add(SetObj(panel));
                }
            }

            PlayerController playerTarget = PlayerTarget(playersObj);

            // 攻撃処理
            StartCoroutine(playerTarget.DamageText(CalcScript.DamagePoint(at, df)));
            myTransform.Rotate(0, -1.0f, 0);
        }
    }

    // ターゲット選定
    PlayerController PlayerTarget(List<GameObject> objs)
    {
        int r = Random.Range(0, objs.Count);

        PlayerController player = objs[r].GetComponent<PlayerController>();

        return player;
    }

    

    public void setDamage(int damage)
    {
        this.Hp -= damage;
    }

    public IEnumerator DamageText(int damage)
    {
        
        text.GetComponent<Text>().text = damage.ToString();
        text.transform.position = DamageTextPos(this.transform);

        text.SetActive(true);

        Hp -= damage;

        yield return new WaitForSeconds(0.8f);

        if (text != null)
        {
            text.SetActive(false);
        }
        
    }

    // HPが0になった時、ランダムでアイテムをドロップする
    ///
    /// id1 = タイリョクノビール
    /// id2 = タイリョクグントノビール
    /// id3 = アタックノビール
    /// id4 = アタックグントノビール
    /// id5 = ディフェンスノビール
    /// id6 = ディフェンスグントノビール
    /// id7 = スピードノビール
    /// id8 = スピードグントノビール
    ///
    void GetRandomItem()
    {
        int randNo = Random.Range(0, dropItemList.Count);
        int randItemId = dropItemList[randNo];

        string dropItemId = "id" + (randItemId + 1);

        if(masterData.itemCounter.ContainsKey(dropItemId))
            masterData.itemCounter[dropItemId] = masterData.itemCounter[dropItemId] + 1;

    }


}
