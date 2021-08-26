using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OnClick : MonoBehaviour
{
    [SerializeField]
    private Button atButton = null // �U���{�^��
                 , gameSceneButton = null // �Q�[���J�ڂɑJ��
                 , selectClearButton = null // �Z���N�g�N���A�{�^��
                 , menuButton = null // ���j���[�{�^��
                 , closeButton = null // ����{�^���i���j���[�́j
                 , strategyButton = null // �X�g���e�W�[�V�[���ɑJ��
                 , unitProfileButton = null // ���j�b�g�v���t�B�[���m�F�{�^��
                 , unitBuyButton = null // ���j�b�g�w���{�^��
                 , reinforcementButton = null;  // ����

    [SerializeField]
    private SummonPanelList playerPanel = null
                          , enemyPanel = null;

    [SerializeField]
    private GameObject menuPanel = null
                     , itemScrollView = null;

    // �����X�^�[���i�[����
    public List<GameObject> playersObj = null;
    public List<GameObject> enemysObj = null;

    public Button ReinforcementButton
    {
        get { return reinforcementButton; }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetButton();
        if (menuPanel) menuPanel.SetActive(false);
        if (itemScrollView) itemScrollView.SetActive(false);
        if (reinforcementButton) reinforcementButton.gameObject.SetActive(false);
    }

    void SetButton()
    {
        if (atButton) atButton.onClick.SetListener(AttackButton);
        if (gameSceneButton) gameSceneButton.onClick.SetListener(OnLoadGameScene);
        if (selectClearButton) selectClearButton.onClick.SetListener(SelectMonsterClear);
        if (menuButton) menuButton.onClick.SetListener(MenuButton);
        if (closeButton) closeButton.onClick.SetListener(CloseButton);
        if (strategyButton) strategyButton.onClick.SetListener(OnLoadStrategyScene);
        if (unitProfileButton) unitProfileButton.onClick.SetListener(UnitProfileButton);
        if (unitBuyButton) unitBuyButton.onClick.SetListener(UnitBuyButton);
        if (reinforcementButton) reinforcementButton.onClick.SetListener(UnitReinforcementButton);
    }

    void AttackButton()
    {
        Debug.Log("�U��");
        // ���X�g�̃N���A
        playersObj.Clear();
        enemysObj.Clear();

        // �v���C���[�̃I�u�W�F�N�g���i�[
        foreach (GameObject panel in playerPanel.panel)
        {
            if (panel.transform.childCount != 0)
            {
                Transform t = panel.GetComponentInChildren<Transform>();
                GameObject obj = t.GetChild(0).gameObject;
                playersObj.Add(obj);
            }
        }

        // �G�l�~�[�̃I�u�W�F�N�g���i�[
        foreach (GameObject panel in enemyPanel.panel)
        {
            if (panel.transform.childCount != 0)
            {
                Transform t = panel.GetComponentInChildren<Transform>();
                GameObject obj = t.GetChild(0).gameObject;
                enemysObj.Add(obj);
            }
        }

        // �U������
        // �v���C���[���G
        if (playersObj.Count != 0)
        {
            int at = playersObj[0].GetComponent<PlayerController>().At;
            int df = playersObj[0].GetComponent<PlayerController>().Df;
            //enemysObj[0].GetComponent<EnemyController>().Hp -= playersObj[0].GetComponent<PlayerController>().At;
            StartCoroutine(enemysObj[0].GetComponent<EnemyController>().DamageText(CalcScript.DamagePoint(at, df)));
        }
        // �G���v���C���[
        //playersObj[0].GetComponent<PlayerController>().Hp -= enemysObj[0].GetComponent<EnemyController>().At;

    }

    // �Q�[���V�[���ɑJ��
    void OnLoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }


    // �X�g���e�W�[�V�[���ɑJ��
    void OnLoadStrategyScene()
    {
        SceneManager.LoadScene("StrategyScene");
    }


    // �I�����������X�^�[���N���A����
    void SelectMonsterClear()
    {
        MasterData md = GameObject.FindWithTag("MasterData").GetComponent<MasterData>();

        for(int i = 0; i < md.selectMonsterList.selectMonsterList.Length; i++)
        {
            md.selectMonsterList.selectMonsterList[i] = -1;
        }

        GameObject[] buttleMonsters = GameObject.FindGameObjectsWithTag("BattleMonster");
        foreach (GameObject monster in buttleMonsters)
        {
            Image image = monster.GetComponent<Image>();
            image.sprite = null;
        }
    }

    // ���j���[�{�^��
    void MenuButton()
    {
        Debug.Log("���j���[");
        menuPanel.SetActive(true);
        StartCoroutine(GameManager.instance.loadUnit.UnitAddButton());
        gameSceneButton.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(false);
    }

    // Cloase�{�^��
    void CloseButton()
    {
        Debug.Log("�N���[�Y");
        if (itemScrollView) itemScrollView.SetActive(false);
        menuPanel.SetActive(false);
        gameSceneButton.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(true);

        GameObject[] objs = GameObject.FindGameObjectsWithTag("RotationModel");
        foreach (GameObject rotationObj in objs)
        {
            Destroy(rotationObj);
        }

    }

    // ���j�b�g�v���t�B�[���m�F�{�^��
    void UnitProfileButton()
    {

    }


    // ���j�b�g�w���{�^��
    void UnitBuyButton()
    {

    }
    
    // ����
    void UnitReinforcementButton()
    {
        if (itemScrollView) itemScrollView.SetActive(true);
    }
}
