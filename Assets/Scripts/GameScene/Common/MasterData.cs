using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

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

}
