using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitData
{
    public Sprite sprite = null;
    public string unitName = null;
    public int hp = 0;
    public int at = 0;
    public int df = 0;
    public int speed = 0;
    public string attackRange = null;
    public string attackType = null;
    public string explanatory = null;
    public int unitIndex = 0;
    public GameObject rotationObj = null;
}

public class UnitViewSctipt : MonoBehaviour
{
    [SerializeField]
    private GameObject unitContent = null;

    [SerializeField]
    private Button unitButtonPrefab;

    [SerializeField]
    private ParmText parmText = null;

    [SerializeField]
    private Transform pos = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddUnitButton(UnitData data)
    {
        Button instance = null;

        instance = Instantiate(unitButtonPrefab);

        var sprite = data.sprite;
        var unitName = data.unitName;
        var hp = data.hp;
        var at = data.at;
        var df = data.df;
        var speed = data.speed;
        var attackRange = data.attackRange;
        var attackType = data.attackType;
        var explanatory = data.explanatory;

        var unitRotationObj = data.rotationObj;
        var index = data.unitIndex;

        instance.transform.SetParent(unitContent.transform, false);

        instance.GetComponent<Image>().sprite = sprite;

        var button = instance.GetComponent<Button>();

        button.onClick.AddListener(() => 
        { 
            Debug.Log(data.unitName + "が押された");
            // パラメータの反映
            parmText.unitName.text = unitName;
            parmText.hpText.text = hp.ToString();
            parmText.atText.text = at.ToString();
            parmText.dfText.text = df.ToString();
            parmText.speedText.text = speed.ToString();
            parmText.attackRange.text = attackRange;
            parmText.attackType.text = attackType;
            parmText.explanatoryText.text = explanatory;

            // masterデータの更新
            MasterData.instance.statusUpTargetNo = index;

            // 回転するオブジェクトを生成
            GameObject[] objs = GameObject.FindGameObjectsWithTag("RotationModel");

            foreach (GameObject rotationObj in objs)
            {
                Destroy(rotationObj);
            }

            Vector3 pos = new Vector3(this.pos.position.x, this.pos.position.y, this.pos.position.z);
            GameObject obj = Instantiate(unitRotationObj, pos, Quaternion.identity);
            GameManager.instance.onClick.ReinforcementButton.gameObject.SetActive(true);
        });
        instance.gameObject.SetActive(true);
    }

}
