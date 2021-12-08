using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//public class PlayerStatus
//{
//    public int lv = 1;
//    public int manaPoint = 10;
//}

public class LvCounter : MonoBehaviour
{
    [SerializeField] Text lvText = null;

    private int lv = 1;
    private int maxManaPoint = 10;

    public int Lv
    {
        get { return lv; }
        set { lv = value; }
    }

    public int MaxManaPoint
    {
        get { return maxManaPoint; }
        set { maxManaPoint = value; }
    }

    // Lvup�̒萔�i���j
    int lvUpPoint = 100;

    // Start is called before the first frame update
    void Start()
    {
        // �X�e�[�^�X�̏�����
        StatusInit();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // �X�e�[�^�X�̏�����
    public void StatusInit()
    {
        Lv = LvCalc(MasterData.instance.Exp);
        MaxManaPoint += lv;

        lvText.text = lv.ToString();
        MasterData.instance.MaxManaPoint = MaxManaPoint;
    }


    // Exp����擾�������ʂ����烌�x�����v�Z���ĕԂ�
    public int LvCalc(int exp)
    {
        int lvPoint = 1;
        bool lvFlag = true;

        while (lvFlag)
        {
            exp = exp - lvUpPoint;

            if (exp < 0)
            {
                lvFlag = false;
                return lvPoint;
            }

            lvPoint++;
        }

        return lvPoint;
    }
}
