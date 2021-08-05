using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prms : MonoBehaviour
{
    [SerializeField]
    protected int hp = 0
                , at = 0
                , df = 0
                , speed = 0;

    protected SummonPanelList playerPanel = null
                            , enemyPanel = null;

    // モンスターを格納する
    protected List<GameObject> playersObj = new List<GameObject>();
    protected List<GameObject> enemysObj = new List<GameObject>();

    protected Animator anime = new Animator();

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
    }

    // キャラのアングルを適正位置にする
    protected void StartAngle()
    {
        Vector3 localAngle = myTransform.localEulerAngles;
        localAngle.y = 0f;
        myTransform.localEulerAngles = localAngle;
    }
}
