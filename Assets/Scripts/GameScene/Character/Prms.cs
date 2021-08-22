using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prms : MonoBehaviour
{
    [SerializeField]
    protected GameObject text = null;
    protected Vector3 startTextPos = new Vector3();

    // エフェクトの格納
    [SerializeField]
    protected GameObject effectPrefab = null;

    // 遠距離可能か判定
    [SerializeField]
    protected bool longRangeFlag = false;

    [SerializeField]
    protected string unitName = null;

    [SerializeField]
    protected int id = 0
                , hp = 0
                , at = 0
                , df = 0
                , speed = 0
                , cost = 0;

    // 攻撃タイミング
    protected float waitTime = 0;
    protected float time = 0;

    // 攻撃タイプ選定
    public enum ATTACK_TYPE
    {
        SINGLE_RANGE, // 単体
        COLUMN_RANGE, // 縦1列
        ROW_RANGE, // 横1列
        ALL_RANGE, // 全体
    }

    public ATTACK_TYPE attack_type;


    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        


    }

    

}
