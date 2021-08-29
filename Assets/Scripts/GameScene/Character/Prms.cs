using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prms : MonoBehaviour
{
    [SerializeField]
    protected GameObject text = null;
    protected Vector3 startTextPos = new Vector3();

    // �G�t�F�N�g�̊i�[
    [SerializeField]
    protected GameObject effectPrefab = null;

    // �������\������
    [SerializeField]
    protected bool longRangeFlag = false;

    [SerializeField]
    protected string unitName = null;

    // �w�����z
    [SerializeField]
    protected int money = 0;

    [SerializeField]
    protected int id = 0
                , hp = 0
                , at = 0
                , df = 0
                , speed = 0
                , cost = 0;

    [SerializeField]
    protected string explanatory = null;

    // �U���^�C�~���O
    protected float waitTime = 0;
    protected float time = 0;

    // �U���^�C�v�I��
    public enum ATTACK_TYPE
    {
        SINGLE_RANGE, // �P��
        COLUMN_RANGE, // �c1��
        ROW_RANGE, // ��1��
        ALL_RANGE, // �S��
    }

    public ATTACK_TYPE attack_type;


    // Start is called before the first frame update
    public virtual void Start()
    {
        if (ATTACK_TYPE.SINGLE_RANGE == attack_type)
        {
            // �P��
            effectPrefab = GameObject.FindWithTag("SingleEffect");
        }
        else if (ATTACK_TYPE.COLUMN_RANGE == attack_type)
        {
            // �c�P��
            effectPrefab = GameObject.FindWithTag("ColumnEffect");
        }
        else if (ATTACK_TYPE.ROW_RANGE == attack_type)
        {
            // ���P��
            effectPrefab = GameObject.FindWithTag("RowEffect");
        }
        else if (ATTACK_TYPE.ALL_RANGE == attack_type)
        {
            // �S��
            effectPrefab = GameObject.FindWithTag("AllEffect");
        }

    }

    // Update is called once per frame
    public virtual void Update()
    {
        


    }

    

}
