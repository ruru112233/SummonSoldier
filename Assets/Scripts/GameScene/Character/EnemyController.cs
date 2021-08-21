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

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        masterData = MasterData.instance;

        text.SetActive(false);

        // ���i�ɏ�������A��i����������Ă��Ȃ��������i�ɏグ��
        PositionCheck.PositionChenge(myTransform, 1.8f);

        waitTime = CalcScript.AttackTime(Speed);


    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        // �G�̃p�l���ɃI�u�W�F�N�g���������ꍇ�A�U������
        if (longRangeFlag)
        {
            if (ChildCheck.AllCountCheck(playerPanel)) AllAttack(playerPanel, playersObj);
        }
        else
        {
            if (ChildCheck.FrontCountCheck(playerPanel)) FrontAttack(playerPanel, playersObj);
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
