using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;

public class EnemyController : CharaController
{
    [SerializeField]
    private List<int> dropItemList = new List<int>();

    MasterData masterData;

    MasterController masterPlayer = null;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        masterData = MasterData.instance;

        text.SetActive(false);

        masterPlayer = GameObject.FindWithTag("MasterPlayer").GetComponent<MasterController>();

        // ���i�ɏ�������A��i����������Ă��Ȃ��������i�ɏグ��
        PositionCheck.PositionChenge(myTransform, 1.8f);

    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        // �G�̃p�l���ɃI�u�W�F�N�g���������ꍇ�A�U������
        if (ATTACK_TYPE.SINGLE_RANGE == attack_type)
        {
            // �P��
            if (longRangeFlag)
            {
                Debug.Log("������");
                if (ChildCheck.AllCountCheck(playerPanel))
                    AllAttack(playerPanel, playersObj, At);
            }
            else
            {
                Debug.Log("�ߋ���");
                if (ChildCheck.FrontCountCheck(playerPanel) &&
                    ChildCheck.FrontCheck(this.transform))
                    FrontAttack(playerPanel, playersObj, At);
            }
        }
        else if (ATTACK_TYPE.COLUMN_RANGE == attack_type)
        {
            // �c���
            if (ChildCheck.ColumnCheck(playerPanel))
                ColumnAttack(playerPanel, playersObj, At);

        }
        else if (ATTACK_TYPE.ROW_RANGE == attack_type)
        {
            // �����
            if (longRangeFlag)
            {
                if (ChildCheck.RowCheck(playerPanel))
                    AllRowAttack(playerPanel, playersObj, At);
            }
            else
            {
                if (ChildCheck.FrontRowCheck(playerPanel))
                    FrontRowAttack(playerPanel, playersObj, At);
            }
        }
        else if (ATTACK_TYPE.ALL_RANGE == attack_type)
        {
            // �S��
            if (ChildCheck.AllCountCheck(playerPanel))
                AllRangeAttack(playerPanel, playersObj, At);
        }

        // �I�u�W�F�N�g�̐��ɕύX���������ꍇ�A�O�q�ړ��̏���������
        if (ObjCountCheck()) PositionCheck.PositionChenge(myTransform, 1.8f);

        // HP��0�ɂȂ������̏���
        if (Hp <= 0)
        {
            GetRandomItem();
            Destroy(gameObject);
        }
    }

    
    // HP��0�ɂȂ������A�����_���ŃA�C�e�����h���b�v����
    ///
    /// id1 = �^�C�����N�m�r�[��
    /// id2 = �^�C�����N�O���g�m�r�[��
    /// id3 = �A�^�b�N�m�r�[��
    /// id4 = �A�^�b�N�O���g�m�r�[��
    /// id5 = �f�B�t�F���X�m�r�[��
    /// id6 = �f�B�t�F���X�O���g�m�r�[��
    /// id7 = �X�s�[�h�m�r�[��
    /// id8 = �X�s�[�h�O���g�m�r�[��
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
