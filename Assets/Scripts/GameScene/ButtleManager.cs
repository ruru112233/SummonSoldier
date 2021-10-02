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

    // ��������Flag
    private int winFlag = 0; // 0:�o�g�����A1:�v���C���[�̏����A2:�G�l�~�[�̏���

    public int WinFlag { get { return winFlag; } set { winFlag = value; } }

    // �I���t���O
    private bool endFlag = false;
    public bool EndFlag { get { return endFlag; } set { endFlag = value; } }

    // �����̈ꎞ�ۑ�
    private int money = 0;

    public int Money { get { return money; } set { money = value; } }

    // �o���l�̈ꎞ�ۑ�
    private int exp = 0;
    public int Exp { get { return exp; } set { exp = value; } }

    [SerializeField]
    private GameObject resultPanel;
    Text resultText = null;

    MasterData masterData;

    float duration = 1.0f;

    ResultText moneyResultText;
    ResultText expResultText;

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
        moneyResultText = resultPanel.transform.GetChild(2).GetChild(1).GetComponent<ResultText>();
        expResultText = resultPanel.transform.GetChild(3).GetChild(1).GetComponent<ResultText>();

    }

    // Update is called once per frame
    void Update()
    {
        if (winFlag != 0)
        {
            OpenResultPanel(winFlag);
        }
    }

    // ���U���g�p�l���̐���
    public void OpenResultPanel(int winFlag)
    {
        StartCoroutine(ResultCol(winFlag));
        this.winFlag = 0; // winFlag�̏�����
    }

    IEnumerator ResultCol(int winFlag)
    {
        yield return new WaitForSeconds(1);
        // ���U���g��ʂ̕\��
        resultText.text = winOrLoseText(winFlag);
        resultPanel.SetActive(true);
        if (winFlag != 2)
        {
            moneyResultText.SlideToNumber(0, Money, duration, "G");
            expResultText.SlideToNumber(0, Exp, duration);

            // �}�X�^�[�f�[�^�֔��f        
            masterData.GameMoney += Money;
            masterData.Exp += Exp;
        }

        yield return new WaitForSeconds(5);

        SceneManager.LoadScene("StrategyScene");

    }

    // �������s�k�̕�����Ԃ�
    string winOrLoseText(int winFlag)
    {
        
        if (winFlag == 2)
        {
            return "�s�k";
        }

        return "����";
    }

}
