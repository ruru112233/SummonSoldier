using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

public class ItemCount
{
    public Dictionary<string, int> counts = new Dictionary<string, int>(); 
}

public class MasterData : MonoBehaviour
{

    [SerializeField]
    private AssetLabelReference _labelReference
                              , _itemListLabel;

    // モンスターイメージ画像格納
    public List<Sprite> monsterImageList;

    // ItemListの格納
    public List<ItemSO> itemList;

    // セットしたモンスターの引継ぎ用
    public SelectMonsterList selectMonsterList = null;

    public bool spriteLoadFlag = false
              , itemListFlag = false;

    public Dictionary<string, int> itemCounter = new Dictionary<string, int>();


    public static MasterData instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (this != instance)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        MonsterImageLoad();
        ItemListLoad();
        StartCoroutine(ItemCountList());
        selectMonsterList = this.GetComponent<SelectMonsterList>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // キャラ画像のロード
    void MonsterImageLoad()
    {
        Addressables.LoadAssetsAsync<Sprite>(_labelReference, null).
        Completed += op =>
        {
            foreach (Sprite sprite in op.Result)
            {
                monsterImageList.Add(sprite);
            }

            spriteLoadFlag = true;
        };
    } 

    // ItemListのロード
    void ItemListLoad()
    {
        Addressables.LoadAssetsAsync<ItemSO>(_itemListLabel, null).
        Completed += op =>
        {
            foreach (ItemSO obj in op.Result)
            {
                itemList.Add(obj);
            }

            itemListFlag = true;
        };
    }


    // 初期Item数を格納
    IEnumerator ItemCountList()
    {
        yield return new WaitUntil(() => itemListFlag);

        ItemCount itemCount = new ItemCount();

        int num = 0;

        for (int i = 0; i < itemList.Count; i++)
        {
            string itemId = "id";
            itemId += (num + 1).ToString();
            itemCounter.Add(itemId, 0);
            num++;
        }

    }

}
