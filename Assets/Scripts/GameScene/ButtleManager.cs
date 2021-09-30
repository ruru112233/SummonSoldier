using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtleManager : MonoBehaviour
{
    [SerializeField]
    private SummonPanelList playerPanel = null
                          , enemyPanel = null;

    // 勝利判定Flag
    private int winFlag = 0; // 0:バトル中、1:プレイヤーの勝利、2:エネミーの勝利

    public int WinFlag { get { return winFlag; } set { winFlag = value; } }

    // お金の一時保存
    private int money = 0;

    public int Money { get { return money; } set { money = value; } }

    // 経験値の一時保存
    private int exp = 0;
    public int Exp { get { return exp; } set { exp = value; } }


    [SerializeField]
    private GameObject resultPanel;
    Text resultText = null; 
    

    // Start is called before the first frame update
    void Start()
    {
        Money = 0;
        Exp = 0;
        WinFlag = 0;
        resultPanel.SetActive(false);
        resultText = resultPanel.transform.GetChild(1).GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        if (winFlag != 0)
        {
            OpenResultPanel(winFlag);
        }
    }

    // リザルトパネルの制御
    public void OpenResultPanel(int winFlag)
    {
        winOrLoseText(winFlag);
        this.winFlag = 0; // winFlagの初期化
        resultPanel.SetActive(true);
    }

    // 勝利か敗北の文字を返す
    string winOrLoseText(int winFlag)
    {
        
        if (winFlag == 2)
        {
            return "敗北";
        }

        return "勝利";
    }
}
