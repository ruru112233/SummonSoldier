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
    protected int hp = 0
                , at = 0
                , df = 0
                , speed = 0
                , cost = 0;

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
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        


    }

    

}
