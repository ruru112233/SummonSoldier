using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonManager : MonoBehaviour
{
    [SerializeField]
    private List<Button> summonButtons = null;

    [SerializeField]
    private SummonPanelList playerPanel = null;

    public List<GameObject> soldierPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        SummonSoldier();
        // 上段のボタンセット
        //for (int i = 0; i < 3; i++)
        //{
        //    summonButtons[i].onClick.SetListener(UpperRowSummon);
        //}

        // 下段のボタンセット
        //for (int i = 3; i < 6; i++)
        //{
        //    summonButtons[i].onClick.SetListener(LowerRowSummon);
        //}

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SummonSoldier()
    {

        void AddSummonButton(int idx)
        {
            if (idx < 3)
            {
                // 上段のセット
                summonButtons[idx].GetComponent<Button>().onClick.AddListener(() => UpperRowSummon(idx));
            }
            else
            {
                // 下段のセット
                summonButtons[idx].GetComponent<Button>().onClick.AddListener(() => LowerRowSummon(idx));
            }

        }

        for (int i = 0; i < summonButtons.Count; i++)
        {
            AddSummonButton(i);
        }

    }

    void UpperRowSummon(int idx)
    {
        Debug.Log("上段");
        // 上段のパネルを取得
        GameObject[] upperPanels = new GameObject[3];

        for (int i = 0; i < 3; i++)
        {
            upperPanels[i] = playerPanel.panel[i];
        }

        // パネルの子要素チェック
        GameObject panelObj = ChildCheck(upperPanels);

        // パネルの子要素が空だったら、空のパネルにセットする。
        if (panelObj != null)
        {
            Summon(panelObj, idx);
        }
    }

    void LowerRowSummon(int idx)
    {
        Debug.Log("下段");
        GameObject[] LowerPanels = new GameObject[3];

        for (int i = 3; i < 6; i++)
        {
            LowerPanels[i - 3] = playerPanel.panel[i];
        }

        // パネルの子要素チェック
        GameObject panelObj = ChildCheck(LowerPanels);

        // パネルの子要素が空だったら、空のパネルにセットする。
        if (panelObj != null)
        {
            Summon(panelObj, idx);
        }
    }

    // パネルの子要素チェック
    GameObject ChildCheck(GameObject[] panels)
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

    // 召喚
    void Summon(GameObject panelObj, int idx)
    {
        Transform transform = panelObj.transform;

        // 召喚時の位置を設定
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);

        // プレハブからインスタンスを生成
        GameObject obj = (GameObject)Instantiate(soldierPrefabs[idx], pos, Quaternion.identity);

        // 生成したオブジェクトを子として登録
        obj.transform.parent = panelObj.transform;
    }
    

    /// <summary>
    /// 
    /// </summary>
    /// <param name="setPanel"> セットするパネルナンバー </param>

    void SetPanel(int idx)
    {
        // サモンパネルを取得
        GameObject panelObj = playerPanel.panel[idx];
        
        Transform transform = panelObj.transform;

        // 召喚時の位置を設定
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);

        // サモンパネルがソルジャーが存在していない場合、ソルジャーを生成
        if (panelObj.transform.childCount == 0)
        {
            // プレハブからインスタンスを生成
            GameObject obj = (GameObject)Instantiate(soldierPrefabs[idx], pos, Quaternion.identity);

            // 生成したオブジェクトを子として登録
            obj.transform.parent = panelObj.transform;
        }
    }

}
