using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ChildCheck
{
 
    // ��ԍ����疄�߂Ă���
    public static GameObject PanelCheck(GameObject[] panels)
    {

        foreach (GameObject obj in panels)
        {
            if (obj.transform.childCount == 0)
            {
                return obj;
            }
        }

        return null;
    }

    // �w�肵���p�l���ɃZ�b�g����
    public static GameObject PanelCheck2(GameObject obj)
    {

        if (obj.transform.childCount == 0)
        {
            return obj;
        }

        return null;
    }


    public static GameObject RandPanel(List<GameObject> panels)
    {
        List<GameObject> newPanels = new List<GameObject>();
        newPanels.Clear();

        foreach (GameObject obj in panels)
        {
            if (obj.transform.childCount == 0)
            {
                newPanels.Add(obj);
            }
        }

        if(newPanels.Count != 0)
        {
            int randNum = Random.Range(0, newPanels.Count);
            return newPanels[randNum];
        }

        return null;
    }

    // �O�q�ɃI�u�W�F�N�g���Ȃ����`�F�b�N����
    public static Transform AvantGardeCheck(int idx, Transform transform)
    {
        Transform t = null;
        // �e�̐e���擾
        Transform parent = transform.parent.parent;
        // parent����ړ���̃I�u�W�F�N�g��I��
        Transform chengeParent = parent.transform.GetChild(idx);

        if (chengeParent.childCount == 0)
        {
            t = chengeParent;
        }

        return t;
    }

    // �S�̂̃I�u�W�F�N�g�̐����J�E���g
    public static int ChildObjCount()
    {
        int playerCount = GameObject.FindGameObjectsWithTag("PlayerMonster").Length;
        int enemyCount = GameObject.FindGameObjectsWithTag("EnemyMonster").Length;

        int sumCount = playerCount + enemyCount;

        return sumCount;
    }

    // �O�q�̃p�l���Ŏq�v�f�ɓG�����݂��邩�m�F
    public static bool FrontCountCheck(SummonPanelList panels)
    {

        for (int i = 0; i < 3; i++)
        {
            if (panels.panel[i].transform.childCount == 1)
            {
                return true;
            }
        }

        return false;
    }

    // �S�̂̃p�l���Ŏq�v�f�ɓG�����݂��邩�m�F
    public static bool AllCountCheck(SummonPanelList panels)
    {

        foreach (GameObject panel in panels.panel)
        {
            if (panel.transform.childCount == 1)
            {
                return true;
            }
        }

        return false;
    }

    // �c1��ȏ㑶�݂��Ă��邩�`�F�b�N
    public static bool ColumnCheck(SummonPanelList panels)
    {
        if (ColumnNoCheck(panels).Count != 0)
        {
            return true;
        }

        return false;
    }

    // �c1��̃p�l���Ŏq�v�f�ɓG�����݂��邩�m�F�i����ڂɑ��݂��邩��Ԃ��j
    public static List<int> ColumnNoCheck(SummonPanelList panels)
    {
        // �ꎞ�i�[�p
        List<int> columns = new List<int>();

        foreach (GameObject panel in panels.panel)
        {
            if (panel.transform.childCount == 1)
            {
                switch (panel.transform.GetSiblingIndex())
                {
                    case 0:
                    case 3:
                        columns.Add(1);
                        break;
                    case 1:
                    case 4:
                        columns.Add(2);
                        break;
                    case 2:
                    case 5:
                        columns.Add(3);
                        break;
                }
            }
        }

        // ���Ԃ��ϐ�
        List<int> columnCheck = columns.Distinct().ToList();

        return columnCheck;

    }


    // ���g�̈ʒu���O�q�����`�F�b�N����
    public static bool FrontCheck(Transform transform)
    {
        GameObject parent = transform.parent.gameObject;

        int idxNo = parent.transform.GetSiblingIndex();

        if (idxNo < 3)
        {
            return true;
        }

        return false;
    }

}
