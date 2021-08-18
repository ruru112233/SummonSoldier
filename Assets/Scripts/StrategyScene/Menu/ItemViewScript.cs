using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// アイテムデータ
public class ItemData
{
    public Sprite sprite = null;
    public string itemName = null;
    public int count = 0;
    public int itemIndex = 0;
}

public class ItemViewScript : MonoBehaviour
{
    [SerializeField]
    private GameObject itemContent = null;

    [SerializeField]
    private Button itemButtonPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddItemButton(ItemData data)
    {
        Button instance = null;

        instance = Instantiate(itemButtonPrefab);

        var sprite = data.sprite;
        var itemName = data.itemName;
        var count = data.count;
        var index = data.itemIndex;

        instance.transform.SetParent(itemContent.transform, false);

        var child0 = instance.transform.GetChild(0);
        Debug.Log(child0.name);

        child0.GetComponent<Image>().sprite = sprite;
        var nameText = instance.transform.GetChild(1).GetComponent<Text>();
        var countText = instance.transform.GetChild(2).GetComponent<Text>();

        var button = instance.GetComponent<Button>();

        if (data.count > 0)
        {
            button.onClick.AddListener(() => { Debug.Log(data.itemName + "が押された"); });
            nameText.text = itemName;
            countText.text = count.ToString();

            instance.gameObject.SetActive(true);
        }
        else
        {
            instance.gameObject.SetActive(false);
        }

    }

}
