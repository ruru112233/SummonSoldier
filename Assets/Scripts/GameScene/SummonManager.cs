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
                //summonButtons[idx].GetComponent<Button>().onClick.AddListener(() => UpperRowSummon(idx));
            }
            else
            {
                // 下段のセット
                //summonButtons[idx].GetComponent<Button>().onClick.AddListener(() => LowerRowSummon(idx));
            }
        }

        for (int i = 0; i < summonButtons.Count; i++)
        {
            AddSummonButton(i);
        }

    }

    public void UpperRowSummon(int idx, int panelNo)
    {
        // 上段のパネルを取得
        GameObject[] upperPanels = new GameObject[3];

        for (int i = 0; i < 3; i++)
        {
            upperPanels[i] = playerPanel.panel[i];
        }

        // パネルの子要素チェック
        GameObject panelObj = ChildCheck.PanelCheck2(upperPanels[panelNo]);

        // パネルの子要素が空だったら、空のパネルにセットする。
        if (panelObj != null)
        {
            Summon(panelObj, idx);
        }
    }

    public void LowerRowSummon(int idx, int panelNo)
    {
        GameObject[] lowerPanels = new GameObject[3];

        Debug.Log("idx>>>:" + idx + "panelNo>>>:" + panelNo);


        for (int i = 3; i < 6; i++)
        {
            lowerPanels[i - 3] = playerPanel.panel[i];
        }

        // パネルの子要素チェック
        GameObject panelObj = ChildCheck.PanelCheck2(lowerPanels[panelNo]);

        // パネルの子要素が空だったら、空のパネルにセットする。
        if (panelObj != null)
        {
            Summon(panelObj, idx);
        }
    }

    // 召喚
    public void Summon(GameObject panelObj, int idx)
    {
        Transform transform = panelObj.transform;

        // 召喚時の位置を設定
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        // プレハブからインスタンスを生成
        GameObject obj = (GameObject)Instantiate(soldierPrefabs[idx], pos, Quaternion.identity);

        // 生成したオブジェクトを子として登録
        obj.transform.parent = panelObj.transform;
    }


}
