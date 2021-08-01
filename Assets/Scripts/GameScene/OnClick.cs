using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class OnClick : MonoBehaviour
{
    [SerializeField]
    private Button atButton = null;

    [SerializeField]
    private SummonPanelList playerPanel = null
                          , enemyPanel = null;

    // モンスターを格納する
    public List<GameObject> playersObj = null;
    public List<GameObject> enemysObj = null;

    // Start is called before the first frame update
    void Start()
    {
        atButton.onClick.SetListener(AttackButton);
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
            enemysObj[0].GetComponent<EnemyController>().Hp -= playersObj[0].GetComponent<PlayerController>().At;
        }
        // 敵→プレイヤー
        //playersObj[0].GetComponent<PlayerController>().Hp -= enemysObj[0].GetComponent<EnemyController>().At;

    }

}
