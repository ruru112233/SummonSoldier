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

    [SerializeField]
    private ParmText parmText = null;

    MasterData masterData = new MasterData();

    // Start is called before the first frame update
    void Start()
    {
        masterData = MasterData.instance;

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

        child0.GetComponent<Image>().sprite = sprite;
        var nameText = instance.transform.GetChild(1).GetComponent<Text>();
        var countText = instance.transform.GetChild(2).GetComponent<Text>();

        var button = instance.GetComponent<Button>();

        if (data.count > 0)
        {
            button.onClick.AddListener(() => 
            { 
                Debug.Log(data.itemName + "が押された");
                count--;
                countText.text = count.ToString();
                UseItem(index);
                if (count < 1)
                {
                    instance.gameObject.SetActive(false);
                }

            });
            nameText.text = itemName;
            countText.text = count.ToString();

            instance.gameObject.SetActive(true);
        }
        else
        {
            instance.gameObject.SetActive(false);
        }

    }

    // アイテム使用の処理
    void UseItem(int itemIdx)
    {
        Debug.Log("UsinItem");
        if (masterData.statusUpTargetNo >= 0)
        {
            if (masterData.itemList[itemIdx].item_type == ItemSO.ITEM_TYPE.MAXHPUP)
            {
                Debug.Log("hpが呼ばれた");
                // MaxHpUp
                int value = masterData.itemList[itemIdx].GetValue();
                masterData.statusList.hpList[masterData.statusUpTargetNo] += value;
                ParmView(parmText.hpText, value);
            }
            else if (masterData.itemList[itemIdx].item_type == ItemSO.ITEM_TYPE.ATUP)
            {
                Debug.Log("atが呼ばれた");

                // AtUp
                int value = masterData.itemList[itemIdx].GetValue();
                masterData.statusList.atList[masterData.statusUpTargetNo] += value;
                ParmView(parmText.atText, value);
            }
            else if (masterData.itemList[itemIdx].item_type == ItemSO.ITEM_TYPE.DFUP)
            {
                Debug.Log("dfが呼ばれた");

                // DfUp
                int value = masterData.itemList[itemIdx].GetValue();
                masterData.statusList.dfList[masterData.statusUpTargetNo] += value;
                ParmView(parmText.dfText, value);
            }
            else if (masterData.itemList[itemIdx].item_type == ItemSO.ITEM_TYPE.SPEEDUP)
            {
                Debug.Log("speedが呼ばれた");

                // SpeedUp
                int value = masterData.itemList[itemIdx].GetValue();
                masterData.statusList.speedList[masterData.statusUpTargetNo] += value;
                ParmView(parmText.speedText, value);
            }
        }

    }

    void ParmView(Text text, int value)
    {
        int num = int.Parse(text.text);
        text.text = (num + value).ToString();
    }

}
