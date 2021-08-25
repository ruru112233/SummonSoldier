using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitData
{
    public Sprite sprite = null;
    public string unitName = null;
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
            Debug.Log(data.unitName + "�������ꂽ");
            // �p�����[�^�̔��f
            parmText.unitName.text = unitName;
            parmText.atText.text = at.ToString();
            parmText.dfText.text = df.ToString();
            parmText.speedText.text = speed.ToString();
            parmText.attackRange.text = attackRange;
            parmText.attackType.text = attackType;
            parmText.explanatoryText.text = explanatory;

            // ��]����I�u�W�F�N�g�𐶐�
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
