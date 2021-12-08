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

    // Lvupの定数（仮）
    int lvUpPoint = 100;

    // Start is called before the first frame update
    void Start()
    {
        // ステータスの初期化
        StatusInit();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ステータスの初期化
    public void StatusInit()
    {
        Lv = LvCalc(MasterData.instance.Exp);
        MaxManaPoint += lv;

        lvText.text = lv.ToString();
        MasterData.instance.MaxManaPoint = MaxManaPoint;
    }


    // Expから取得した結果をからレベルを計算して返す
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
