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
                 , closeButton = null // 閉じるボタン（メニューの）
                 , strategyButton = null // ストラテジーシーンに遷移
                 , unitProfileButton = null // ユニットプロフィール確認ボタン
                 , unitBuyButton = null // ユニット購入ボタン
                 , reinforcementButton = null;  // 強化

    [SerializeField]
    private SummonPanelList playerPanel = null
                          , enemyPanel = null;

    [SerializeField]
    private GameObject menuPanel = null
                     , itemScrollView = null;

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
        gameSceneButton.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(false);
    }

    // Cloaseボタン
    void CloseButton()
    {
        Debug.Log("クローズ");
        menuPanel.SetActive(false);
        gameSceneButton.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(true);

        GameObject[] objs = GameObject.FindGameObjectsWithTag("RotationModel");
        foreach (GameObject rotationObj in objs)
        {
            Destroy(rotationObj);
        }

    }

    // ユニットプロフィール確認ボタン
    void UnitProfileButton()
    {

    }


    // ユニット購入ボタン
    void UnitBuyButton()
    {

    }
    
    // 強化
    void UnitReinforcementButton()
    {

    }
}
