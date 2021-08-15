using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OnClick : MonoBehaviour
{
    [SerializeField]
    private Button atButton = null
                 , gameSceneButton = null
                 , strategySceneButton = null;

    [SerializeField]
    private SummonPanelList playerPanel = null
                          , enemyPanel = null;

    // �����X�^�[���i�[����
    public List<GameObject> playersObj = null;
    public List<GameObject> enemysObj = null;

    // Start is called before the first frame update
    void Start()
    {
        if (atButton != null) atButton.onClick.SetListener(AttackButton);
        if (gameSceneButton != null) gameSceneButton.onClick.SetListener(OnLoadGameScene);
        if (strategySceneButton != null) strategySceneButton.onClick.SetListener(OnLoadStrategyScene);
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
}
