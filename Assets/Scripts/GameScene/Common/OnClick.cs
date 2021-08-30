using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OnClick : MonoBehaviour
{
    [SerializeField]
    private Button atButton = null // 攻撃ボタン
                 , gameSceneButton = null // ゲーム遷移に遷移
                 , selectClearButton = null // セレクトクリアボタン
                 , menuButton = null // メニューボタン
                 , menuCloseButton = null // 閉じるボタン（メニューの）
                 , itemCloseButton = null // 閉じるボタン（アイテムウインドウの）
                 , strategyButton = null // ストラテジーシーンに遷移
                 , unitProfileButton = null // ユニットプロフィール確認ボタン
                 , unitBuyButton = null // ユニット購入ボタン
                 , reinforcementButton = null;  // 強化

    [SerializeField]
    private SummonPanelList playerPanel = null
                          , enemyPanel = null;

    [SerializeField]
    private StageButton stageButton = null;

    [SerializeField]
    private GameObject menuPanel = null
                     , itemScrollView = null
                     , buyUnitPanel = null;

    // モンスターを格納する
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
        StartFalse();
    }

    void SetButton()
    {
        if (atButton) atButton.onClick.SetListener(AttackButton);
        if (gameSceneButton) gameSceneButton.onClick.SetListener(OnLoadGameScene);
        if (selectClearButton) selectClearButton.onClick.SetListener(SelectMonsterClear);
        if (menuButton) menuButton.onClick.SetListener(MenuButton);
        if (menuCloseButton) menuCloseButton.onClick.SetListener(MenuCloseButton);
        if (itemCloseButton) itemCloseButton.onClick.SetListener(ItemCloseButton);
        if (strategyButton) strategyButton.onClick.SetListener(OnLoadStrategyScene);
        if (unitProfileButton) unitProfileButton.onClick.SetListener(UnitProfileButton);
        if (unitBuyButton) unitBuyButton.onClick.SetListener(UnitBuyButton);
        if (reinforcementButton) reinforcementButton.onClick.SetListener(UnitReinforcementButton);
    }

    // 初期非表示の設定
    void StartFalse()
    {
        if (menuPanel) menuPanel.SetActive(false);
        if (itemScrollView) itemScrollView.SetActive(false);
        if (reinforcementButton) reinforcementButton.gameObject.SetActive(false);
        if (itemCloseButton) itemCloseButton.gameObject.SetActive(false);
    }

    void AttackButton()
    {
        Debug.Log("攻撃");
        // リストのクリア
        playersObj.Clear();
        enemysObj.Clear();

        // プレイヤーのオブジェクトを格納
        foreach (GameObject panel in playerPanel.panel)
        {
            if (panel.transform.childCount != 0)
            {
                Transform t = panel.GetComponentInChildren<Transform>();
                GameObject obj = t.GetChild(0).gameObject;
                playersObj.Add(obj);
            }
        }

        // エネミーのオブジェクトを格納
        foreach (GameObject panel in enemyPanel.panel)
        {
            if (panel.transform.childCount != 0)
            {
                Transform t = panel.GetComponentInChildren<Transform>();
                GameObject obj = t.GetChild(0).gameObject;
                enemysObj.Add(obj);
            }
        }

        // 攻撃処理
        // プレイヤー→敵
        if (playersObj.Count != 0)
        {
            int at = playersObj[0].GetComponent<PlayerController>().At;
            int df = playersObj[0].GetComponent<PlayerController>().Df;
            //enemysObj[0].GetComponent<EnemyController>().Hp -= playersObj[0].GetComponent<PlayerController>().At;
            StartCoroutine(enemysObj[0].GetComponent<EnemyController>().DamageText(CalcScript.DamagePoint(at, df)));
        }
        // 敵→プレイヤー
        //playersObj[0].GetComponent<PlayerController>().Hp -= enemysObj[0].GetComponent<EnemyController>().At;

    }

    // ゲームシーンに遷移
    void OnLoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }


    // ストラテジーシーンに遷移
    void OnLoadStrategyScene()
    {
        SceneManager.LoadScene("StrategyScene");
    }


    // 選択したモンスターをクリアする
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

    // メニューボタン
    void MenuButton()
    {
        Debug.Log("メニュー");
        menuPanel.SetActive(true);
        StartCoroutine(GameManager.instance.loadUnit.UnitAddButton());
        //gameSceneButton.gameObject.SetActive(false);
        if (stageButton) stageButton.stageSelectButton.gameObject.SetActive(false);
        if (buyUnitPanel) buyUnitPanel.SetActive(false);
        menuButton.gameObject.SetActive(false);
        if (menuCloseButton) menuCloseButton.gameObject.SetActive(true);
    }

    // メニューのCloseボタン
    void MenuCloseButton()
    {
        Debug.Log("クローズ");
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

    // アイテムウインドウのCloseボタン
    void ItemCloseButton()
    {
        if (itemScrollView) itemScrollView.SetActive(false);
        if (menuCloseButton) menuCloseButton.gameObject.SetActive(true);
        if (itemCloseButton) itemCloseButton.gameObject.SetActive(false);
    }

    // ユニットプロフィール確認ボタン
    void UnitProfileButton()
    {
        if (buyUnitPanel) buyUnitPanel.SetActive(false);
        StartCoroutine(GameManager.instance.loadUnit.UnitAddButton());
    }


    // ユニット購入ボタン
    void UnitBuyButton()
    {
        if (buyUnitPanel) buyUnitPanel.SetActive(true);
        StartCoroutine( GameManager.instance.loadBuyUnit.BuyAddButton());
    }
    
    // 強化
    void UnitReinforcementButton()
    {
        if (itemScrollView) itemScrollView.SetActive(true);
        if (menuCloseButton) menuCloseButton.gameObject.SetActive(false);
        if (itemCloseButton) itemCloseButton.gameObject.SetActive(true);

    }
}
