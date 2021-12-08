using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FieldButtonData
{
    public string fieldName = "";
    public int index = 0;
    public bool fieldFlag = true;
}

public class StageButtonData
{
    public string stageName = "";
    public string stageNo = "";
    public int index = 0;
    public bool stageFlag = true;
}

public class StageButton : MonoBehaviour
{
    public Button stageSelectButton = null
                , fieldButton = null
                , stageButton = null;

    public GameObject fieldSelectPanel
                    , stageSelectPanel;

    [SerializeField]
    private OnClick onClick;

    MasterData masterData;

    // Start is called before the first frame update
    void Start()
    {
        masterData = MasterData.instance;

        stageSelectButton.onClick.SetListener(FieldSelect);
        fieldSelectPanel.SetActive(false);
        stageSelectPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // セレクトボタン押下時の処理
    public void FieldSelect()
    {
        fieldSelectPanel.SetActive(true);
        ButtonOnOff(false);
        FieldSelectButton();
    }

    // ボタン表示非表示
    void ButtonOnOff(bool flag)
    {
        stageSelectButton.gameObject.SetActive(flag);
        onClick.menuButton.gameObject.SetActive(flag);
    }

    // フィールドセレクト関係
    void FieldSelectButton()
    {
        int fieldNo = int.Parse(MasterData.instance.ClearStage.Substring(0, 2));

        var data = new FieldButtonData();

        for (int i = 0; i < fieldNo; i++)
        {

            string fieldName = FieldName(i);

            data.fieldName = fieldName;
            data.index = i;
            data.fieldFlag = true;

            AddFieldButton( data );
        }

        data.fieldName = "もどる";
        data.fieldFlag = false;

        AddFieldButton(data);

    }

    // フィールドの名称を返す
    string FieldName(int index)
    {   
        string fieldName = "";

        switch (index)
        {
            case 0:
                fieldName = "森（昼）";
                break;
            case 1:
                fieldName = "洞窟（昼）";
                break;
            case 2:
                fieldName = "砂漠（昼）";
                break;
        }

        return fieldName;

    }


    void AddFieldButton(FieldButtonData data)
    {
        Button instance = null;

        instance = Instantiate(fieldButton);

        var fieldName = data.fieldName;
        var index = data.index;
        var fieldFlag = data.fieldFlag;

        instance.transform.SetParent(fieldSelectPanel.transform, false);

        var fieldText = instance.transform.GetChild(0).GetComponent<Text>();

        var button = instance.GetComponent<Button>();

        fieldText.text = fieldName;

        button.onClick.AddListener(() =>
        {
            if (fieldFlag)
            {
                fieldSelectPanel.SetActive(false);
                stageSelectPanel.SetActive(true);
                StageSelect(index);
            }
            else
            {
                ReverseButton(true);
            }
            
        });

        instance.gameObject.SetActive(true);
    }


    // ステージセレクト関係
    void StageSelect(int fieldNo)
    {
        int stageNo = int.Parse(MasterData.instance.ClearStage.Substring(2, 1));

        var data = new StageButtonData();

        for (int i = 0; i < stageNo; i++)
        {

            string stageName = StageName(fieldNo + 1, i);

            data.stageNo = (fieldNo + 1).ToString() + (i + 1).ToString();

            data.stageName = stageName;
            data.index = i;
            data.stageFlag = true;

            AddStageButton( data );

        }

        string reverseName = "もどる";

        data.stageName = reverseName;
        data.stageFlag = false;

        AddStageButton( data );


    }

    string StageName(int fieldNo ,int index)
    {
        string stageName = fieldNo.ToString() + "-" + (index + 1).ToString();

        return stageName;
    }

    void AddStageButton(StageButtonData data)
    {
        Button instance = null;

        instance = Instantiate(stageButton);

        var fieldName = data.stageName;
        string stageNo = "0" + data.stageNo;
        var stageFlag = data.stageFlag;

        instance.transform.SetParent(stageSelectPanel.transform, false);

        var fieldText = instance.transform.GetChild(0).GetComponent<Text>();

        var button = instance.GetComponent<Button>();

        fieldText.text = fieldName;

        button.onClick.AddListener(() =>
        {
            if (stageFlag)
            {
                masterData.CurrentStage = stageNo;
                SceneManager.LoadScene("GameScene");
            }
            else
            {
                ReverseButton(false);
            }
        });

        instance.gameObject.SetActive(true);
    }

    // ステージセレクトの場合


    // 戻るボタンの場合
    void ReverseButton(bool field)
    {
        if (field)
        {
            GameObject[] buttons = GameObject.FindGameObjectsWithTag("FieldButton");

            foreach (GameObject button in buttons)
            {
                Destroy(button);
            }
            ButtonOnOff(true);
            fieldSelectPanel.SetActive(false);
        }
        else
        {
            GameObject[] buttons = GameObject.FindGameObjectsWithTag("StageButton");

            foreach (GameObject button in buttons)
            {
                Destroy(button);
            }

            stageSelectPanel.SetActive(false);
            fieldSelectPanel.SetActive(true);
        }
    }

}
