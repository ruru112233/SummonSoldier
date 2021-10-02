using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtleManager : MonoBehaviour
{
    [SerializeField]
    private SummonPanelList playerPanel = null
                          , enemyPanel = null;

    // 勝利判定Flag
    private int winFlag = 0; // 0:バトル中、1:プレイヤーの勝利、2:エネミーの勝利

    public int WinFlag { get { return winFlag; } set { winFlag = value; } }

    // 終了フラグ
    private bool endFlag = false;
    public bool EndFlag { get { return endFlag; } set { endFlag = value; } }

    // お金の一時保存
    private int money = 0;

    public int Money { get { return money; } set { money = value; } }

    // 経験値の一時保存
    private int exp = 0;
    public int Exp { get { return exp; } set { exp = value; } }

    [SerializeField]
    private GameObject resultPanel;
    Text resultText = null;
    Text moneyText = null;
    Text expText = null;

    MasterData masterData;

    // Start is called before the first frame update
    void Start()
    {
        Money = 0;
        Exp = 0;
        WinFlag = 0;
        EndFlag = false;
        resultPanel.SetActive(false);

        masterData = MasterData.instance;

        resultText = resultPanel.transform.GetChild(1).GetComponent<Text>();
        moneyText = resultPanel.transform.GetChild(2).GetChild(0).GetComponent<Text>();
        expText = resultPanel.transform.GetChild(3).GetChild(0).GetComponent<Text>();
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
        StartCoroutine(ResultCol(winFlag));
        this.winFlag = 0; // winFlagの初期化
    }

    IEnumerator ResultCol(int winFlag)
    {
        yield return new WaitForSeconds(1);
        // リザルト画面の表示
        resultText.text = winOrLoseText(winFlag);
        resultPanel.SetActive(true);
        if (winFlag != 2)
        {
            moneyText.text = Money.ToString() + "G";
            expText.text = Exp.ToString();

            // マスターデータへ反映        
            masterData.GameMoney += Money;
            masterData.Exp += Exp;
        }

        yield return new WaitForSeconds(4);

        SceneManager.LoadScene("StrategyScene");

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
