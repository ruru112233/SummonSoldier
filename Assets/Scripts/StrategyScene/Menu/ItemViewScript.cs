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
                UseItem(index);

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
                // masterData.statusDataList.list[masterData.statusUpTargetNo].hp += masterData.itemList[itemIdx].GetValue();

                masterData.statusList.hpList[masterData.statusUpTargetNo] += masterData.itemList[itemIdx].GetValue();
                
            }
            else if (masterData.itemList[itemIdx].item_type == ItemSO.ITEM_TYPE.ATUP)
            {
                Debug.Log("atが呼ばれた");
                
                // AtUp
                masterData.statusList.atList[masterData.statusUpTargetNo] += masterData.itemList[itemIdx].GetValue();
            }
            else if (masterData.itemList[itemIdx].item_type == ItemSO.ITEM_TYPE.DFUP)
            {
                Debug.Log("dfが呼ばれた");
                
                // DfUp
                masterData.statusList.dfList[masterData.statusUpTargetNo] += masterData.itemList[itemIdx].GetValue();
            }
            else if (masterData.itemList[itemIdx].item_type == ItemSO.ITEM_TYPE.SPEEDUP)
            {
                Debug.Log("speedが呼ばれた");
                
                // SpeedUp
                masterData.statusList.speedList[masterData.statusUpTargetNo] += masterData.itemList[itemIdx].GetValue();
            }
        }

        //Debug.Log("ID>>>:" + masterData.statusUpTargetNo);
        //Debug.Log("HP>>>:" + masterData.statusDataList.list[masterData.statusUpTargetNo].hp);
        //Debug.Log("AT>>>:" + masterData.statusDataList.list[masterData.statusUpTargetNo].at);
        //Debug.Log("DF>>>:" + masterData.statusDataList.list[masterData.statusUpTargetNo].df);
        //Debug.Log("SPEED>>>:" + masterData.statusDataList.list[masterData.statusUpTargetNo].speed);

        //for (int i = 0; i < masterData.playerUnitList.Count; i++)
        //{
        //    Debug.Log("ID>>>:" + masterData.statusUpTargetNo);
        //    Debug.Log("HP>>>:" + masterData.statusDataList.list[i].hp);
        //    Debug.Log("AT>>>:" + masterData.statusDataList.list[i].at);
        //    Debug.Log("DF>>>:" + masterData.statusDataList.list[i].df);
        //    Debug.Log("SPEED>>>:" + masterData.statusDataList.list[i].speed);
        //}

        int ii = 0;

        foreach (UnitPowerUpStatusData list in masterData.statusDataList)
        {

            Debug.Log(ii + "HP>>>:" + list.HpValue);
            Debug.Log(ii + "At>>>:" + list.AtValue);
            Debug.Log(ii + "Df>>>:" + list.DfValue);
            Debug.Log(ii + "Speed>>>:" + list.SpeedValue);

            ii++;

        }


    }

}
