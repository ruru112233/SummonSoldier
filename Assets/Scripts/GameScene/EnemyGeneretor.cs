using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneretor : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> monsterPrefabs = null;

    [SerializeField]
    private SummonPanelList enemyPanels = null;

    // 生成時間
    float intervalTime = 3.0f;
    float startTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        startTime += Time.deltaTime;

        if (startTime > intervalTime)
        {
            MonsterInstance();
            startTime = 0;
        }
    }

    // モンスターの生成
    void MonsterInstance()
    {
        // 敵のパネルを取得する
        GameObject panel = ChildCheck.RandPanel(enemyPanels.panel);

        if (panel != null && monsterPrefabs.Count != 0)
        {
            EnemyInstance(panel);
        }

    }

    void EnemyInstance(GameObject panelObj)
    {
        Transform transform = panelObj.transform;

        int idx = Random.Range(0, monsterPrefabs.Count);

        // 召喚時の位置を設定
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        // プレハブからインスタンスを生成
        GameObject obj = (GameObject)Instantiate(monsterPrefabs[idx], pos, Quaternion.identity);

        // 生成したオブジェクトを子として登録
        obj.transform.parent = panelObj.transform;
    }
}
