using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonManager : MonoBehaviour
{
    [SerializeField]
    private Button summonButton = null;

    public List<GameObject> soldierPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        summonButton.onClick.SetListener(SummonSoldier);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SummonSoldier()
    {
        SetPanel(1);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="setPanel"> セットするパネルナンバー </param>

    void SetPanel(int setPanel)
    {
        // サモンパネルを取得
        GameObject[] panelObj = GameObject.FindGameObjectsWithTag("panel1");

        // 召喚時の位置を設定
        Vector3 pos = new Vector3(0, 0.1f, 0);

        // サモンパネルがソルジャーが存在していない場合、ソルジャーを生成
        if (panelObj[setPanel - 1].transform.childCount == 0)
        {
            // プレハブからインスタンスを生成
            GameObject obj = (GameObject)Instantiate(soldierPrefabs[0], pos, Quaternion.identity);

            // 生成したオブジェクトを子として登録
            obj.transform.parent = panelObj[setPanel - 1].transform;
        }

    }

}
