using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prms : MonoBehaviour
{
    [SerializeField]
    protected GameObject text = null;
    protected Vector3 startTextPos = new Vector3();

    // 遠距離可能か判定
    [SerializeField]
    protected bool longRangeFlag = false;

    [SerializeField]
    protected int hp = 0
                , at = 0
                , df = 0
                , speed = 0
                , cost = 0;

    protected SummonPanelList playerPanel = null
                            , enemyPanel = null;

    // 攻撃タイミング
    protected float waitTime = 0;
    protected float time = 0;

    // モンスターを格納する
    protected List<GameObject> playersObj = new List<GameObject>();
    protected List<GameObject> enemysObj = new List<GameObject>();

    protected Animator anime = new Animator();

    // オブジェクト数を入れておく
    protected int objCount = 0;
    int count1 = 0;
    int count2 = 0;

    // 初期位置
    protected Transform myTransform;

    // Start is called before the first frame update
    public virtual void Start()
    {
        playerPanel = GameObject.FindWithTag("PlayerPanel").GetComponent<SummonPanelList>();
        enemyPanel = GameObject.FindWithTag("EnemyPanel").GetComponent<SummonPanelList>();

        anime = this.GetComponent<Animator>();
        myTransform = this.transform;
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
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


    // 前衛のパネルで子要素に敵が存在するか確認
    protected bool FrontCountCheck(SummonPanelList playerPanel)
    {

        for (int i = 0; i < 3; i++)
        {
            if (enemyPanel.panel[i].transform.childCount == 1)
            {
                return true;
            }
        }

        return false;
    }

    // 全体のパネルで子要素に敵が存在するか確認
    protected bool AllCountCheck(SummonPanelList playerPanel)
    {

        foreach (GameObject panel in playerPanel.panel)
        {
            if (panel.transform.childCount == 1)
            {
                return true;
            }
        }

        return false;
    }
}
