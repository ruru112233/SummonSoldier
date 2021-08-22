using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitData
{
    public Sprite sprite = null;
    public string unitName = null;
    public int unitIndex = 0;
}

public class UnitViewSctipt : MonoBehaviour
{
    [SerializeField]
    private GameObject unitContent = null;

    [SerializeField]
    private Button unitButtonPrefab;

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
        var index = data.unitIndex;

        instance.transform.SetParent(unitContent.transform, false);

        instance.GetComponent<Image>().sprite = sprite;

        var button = instance.GetComponent<Button>();

        button.onClick.AddListener(() => { Debug.Log(data.unitName + "�������ꂽ"); });
        instance.gameObject.SetActive(true);

    }


}
