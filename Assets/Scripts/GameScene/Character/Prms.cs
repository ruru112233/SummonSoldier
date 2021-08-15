using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prms : MonoBehaviour
{
    [SerializeField]
    protected GameObject text = null;
    protected Vector3 startTextPos = new Vector3();

    [SerializeField]
    protected int hp = 0
                , at = 0
                , df = 0
                , speed = 0
                , cost = 0;

    protected SummonPanelList playerPanel = null
                            , enemyPanel = null;

    // �U���^�C�~���O
    protected float waitTime = 0;
    protected float time = 0;

    // �����X�^�[���i�[����
    protected List<GameObject> playersObj = new List<GameObject>();
    protected List<GameObject> enemysObj = new List<GameObject>();

    protected Animator anime = new Animator();

    // �I�u�W�F�N�g�������Ă���
    protected int objCount = 0;
    int count1 = 0;
    int count2 = 0;

    // �����ʒu
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

    // �L�����̃A���O����K���ʒu�ɂ���
    protected void StartAngle()
    {
        Vector3 localAngle = myTransform.localEulerAngles;
        localAngle.y = 0f;
        myTransform.localEulerAngles = localAngle;
    }

    // �S�̂̃I�u�W�F�N�g�̐��ɕω������������ǂ���
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

        Debug.Log(check);

        return check;
    }
}
