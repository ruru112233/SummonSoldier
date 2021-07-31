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
            summonButtons[idx].GetComponent<Button>().onClick.AddListener(() => SetPanel(idx));
        }

        for (int i = 0; i < summonButtons.Count; i++)
        {
            AddSummonButton(i);
        }

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
