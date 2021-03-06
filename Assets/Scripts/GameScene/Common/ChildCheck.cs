using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ChildCheck
{
 
    // 一番左から埋めていく
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

    // 指定したパネルにセットする
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

    // 前衛にオブジェクトがないかチェックする
    public static Transform AvantGardeCheck(int idx, Transform transform)
    {
        Transform t = null;
        // 親の親を取得
        Transform parent = transform.parent.parent;
        // parentから移動先のオブジェクトを選択
        Transform chengeParent = parent.transform.GetChild(idx);

        if (chengeParent.childCount == 0)
        {
            t = chengeParent;
        }

        return t;
    }

    // 全体のオブジェクトの数をカウント
    public static int ChildObjCount()
    {
        int playerCount = GameObject.FindGameObjectsWithTag("PlayerMonster").Length;
        int enemyCount = GameObject.FindGameObjectsWithTag("EnemyMonster").Length;

        int sumCount = playerCount + enemyCount;

        return sumCount;
    }

    // Playerの子オブジェクトの数をカウント
    public static int PlayerChildObjectCount()
    {
        int playerCount = GameObject.FindGameObjectsWithTag("PlayerMonster").Length;

        return playerCount;
    }

    // Enemyの子オブジェクトの数をカウント
    public static int EnemyChildObjectCount()
    {
        int enemyCount = GameObject.FindGameObjectsWithTag("EnemyMonster").Length;

        return enemyCount;
    }
    // 前衛のパネルで子要素に敵が存在するか確認
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

    // 全体のパネルで子要素に敵が存在するか確認
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

    // 縦1列以上存在しているかチェック
    public static bool ColumnCheck(SummonPanelList panels)
    {
        if (ColumnNoCheck(panels).Count != 0)
        {
            return true;
        }

        return false;
    }

    // 縦1列のパネルで子要素に敵が存在するか確認（何列目に存在するかを返す）
    public static List<int> ColumnNoCheck(SummonPanelList panels)
    {
        // 一時格納用
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

        // 列を返す変数
        List<int> columnCheck = columns.Distinct().ToList();

        return columnCheck;

    }

    // 横1行以上存在しているかチェック
    public static bool RowCheck(SummonPanelList panels)
    {
        if (RowNoCheck(panels).Count != 0)
        {
            return true;
        }

        return false;
    }

    // 前衛に存在しているかチェック
    public static bool FrontRowCheck(SummonPanelList panels)
    {
        if (RowNoCheck(panels).Contains(1))
        {
            return true;
        }

        return false;
    }

    // 横一列のパネルで子要素に敵が存在するか確認（何列目に存在するかを返す）
    public static List<int> RowNoCheck(SummonPanelList panels)
    {
        // 一時格納用
        List<int> rows = new List<int>();

        foreach (GameObject panel in panels.panel)
        {
            if (panel.transform.childCount == 1)
            {
                switch (panel.transform.GetSiblingIndex())
                {
                    case 0:
                    case 1:
                    case 2:
                        rows.Add(1);
                        break;
                    case 3:
                    case 4:
                    case 5:
                        rows.Add(2);
                        break;
                }
            }
        }

        // 列を返す変数
        List<int> rowCheck = rows.Distinct().ToList();

        return rowCheck;

    }

    // 自身の位置が前衛かをチェックする
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
