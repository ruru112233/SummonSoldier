using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadItem : MonoBehaviour
{
    [SerializeField]
    private ItemViewScript itemView = null;

    MasterData masterData;

    // Start is called before the first frame update
    void Start()
    {
        masterData = MasterData.instance;

        StartCoroutine(ItemMenu());

    }

    IEnumerator ItemMenu()
    {
        yield return new WaitUntil(() => masterData.itemListFlag);
        var index = 0;

        for (int i = 0; i < masterData.itemList.Count; i++)
        {
            var itemData = masterData.itemList[i];

            var data = new ItemData();

            string id = "id" + itemData.GetId();

            data.sprite = itemData.GetSprite();
            data.itemName = itemData.GetItemName();
            data.count = GetItemCount(id);
            data.itemIndex = index;

            itemView.AddItemButton( data );

            index++;
        }

    }

    // ƒAƒCƒeƒ€‚ÌŽæ“¾”‚ð•Ô‚·
    int GetItemCount(string id)
    {   

        int count = 0;

        if (masterData.itemCounter.ContainsKey(id))
        {
            count = masterData.itemCounter[id];
            return count;
        }
        
        return count;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
