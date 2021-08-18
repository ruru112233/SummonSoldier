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

        startTextPos = text.transform.position;
        startTextPos.y += 1.0f;
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
        if (PlayerCountCheck(playerPanel) != 0) Attack();

        // �I�u�W�F�N�g�̐��ɕύX���������ꍇ�A�O�q�ړ��̏���������
        if (ObjCountCheck()) PositionCheck.PositionChenge(myTransform, 1.8f);

        // HP��0�ɂȂ������̏���
        if (Hp <= 0)
        {
            GetRandomItem();
            Destroy(gameObject);
        }
    }

    // �U��
    void Attack()
    {
        time += Time.deltaTime;

        if (time > waitTime)
        {
            time = 0;
            waitTime = CalcScript.AttackTime(Speed);

            anime.SetTrigger("attack");

            playersObj.Clear();

            // �G�l�~�[�̃I�u�W�F�N�g���i�[
            foreach (GameObject panel in playerPanel.panel)
            {
                if (panel.transform.childCount != 0)
                {
                    Transform t = panel.GetComponentInChildren<Transform>();
                    GameObject obj = t.GetChild(0).gameObject;
                    playersObj.Add(obj);
                }
            }

            PlayerController playerTarget = PlayerTarget(playersObj);

            // �U������
            StartCoroutine(playerTarget.DamageText(CalcScript.DamagePoint(at, df)));
            myTransform.Rotate(0, -1.0f, 0);
        }
    }

    // �^�[�Q�b�g�I��
    PlayerController PlayerTarget(List<GameObject> objs)
    {
        int r = Random.Range(0, objs.Count);

        PlayerController player = objs[r].GetComponent<PlayerController>();

        return player;
    }

    // �q�v�f�ɓG�����݂��邩�m�F
    int PlayerCountCheck(SummonPanelList playerPanel)
    {
        int count = 0;

        foreach (GameObject panel in playerPanel.panel)
        {
            if (panel.transform.childCount == 1)
            {
                count++;
            }
        }

        return count;
    }

    public void setDamage(int damage)
    {
        this.Hp -= damage;
    }

    public IEnumerator DamageText(int damage)
    {
        text.SetActive(true);

        text.GetComponent<Text>().text = damage.ToString();
        text.transform.position = startTextPos;

        Hp -= damage;

        yield return new WaitForSeconds(0.8f);

        if (text != null)
        {
            text.SetActive(false);
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
