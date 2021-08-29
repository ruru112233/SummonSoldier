using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldButtonData
{
    public string fieldName = "";
    public int index = 0;
    public bool fieldFlag = true;
}

public class StageButtonData
{
    public string stageName = "";
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

    // Start is called before the first frame update
    void Start()
    {
        stageSelectButton.onClick.SetListener(FieldSelect);
        fieldSelectPanel.SetActive(false);
        stageSelectPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // �Z���N�g�{�^���������̏���
    public void FieldSelect()
    {
        fieldSelectPanel.SetActive(true);
        FieldSelectButton();
    }


    // �t�B�[���h�Z���N�g�֌W
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

        data.fieldName = "���ǂ�";
        data.fieldFlag = false;

        AddFieldButton(data);




    }

    // �t�B�[���h�̖��̂�Ԃ�
    string FieldName(int index)
    {   
        string fieldName = "";

        switch (index)
        {
            case 0:
                fieldName = "�X";
                break;
            case 1:
                fieldName = "�r��";
                break;
            case 2:
                fieldName = "����";
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


    // �X�e�[�W�Z���N�g�֌W
    void StageSelect(int fieldNo)
    {
        int stageNo = int.Parse(MasterData.instance.ClearStage.Substring(2, 1));

        var data = new StageButtonData();

        for (int i = 0; i < stageNo; i++)
        {

            string stageName = StageName(fieldNo + 1, i);

            data.stageName = stageName;
            data.index = i;
            data.stageFlag = true;

            AddStageButton( data );

        }

        string reverseName = "���ǂ�";

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
        var stageFlag = data.stageFlag;

        instance.transform.SetParent(stageSelectPanel.transform, false);

        var fieldText = instance.transform.GetChild(0).GetComponent<Text>();

        var button = instance.GetComponent<Button>();

        fieldText.text = fieldName;

        button.onClick.AddListener(() =>
        {
            if (stageFlag)
            {

            }
            else
            {
                ReverseButton(false);
            }
        });

        instance.gameObject.SetActive(true);
    }

    // �X�e�[�W�Z���N�g�̏ꍇ


    // �߂�{�^���̏ꍇ
    void ReverseButton(bool field)
    {
        if (field)
        {
            GameObject[] buttons = GameObject.FindGameObjectsWithTag("FieldButton");

            foreach (GameObject button in buttons)
            {
                Destroy(button);
            }

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
