using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

public class ItemCount
{
    public Dictionary<string, int> counts = new Dictionary<string, int>(); 
}

public class UnitPowerUpStatusData
{
    private int hp = 0;
    private int at = 0;
    private int df = 0;
    private int speed = 0;

    public int HpValue
    {
        get { return hp; }
        set { hp = value; }
    }
    public int AtValue
    {
        get { return at; }
        set { at = value; }
    }
    public int DfValue
    {
        get { return df; }
        set { df = value; }
    }
    public int SpeedValue
    {
        get { return speed; }
        set { speed = value; }
    }

}

public class PowerUpStatusDataList
{
    public List<UnitPowerUpStatusData> list = new List<UnitPowerUpStatusData>();
}

public class MasterData : MonoBehaviour
{

    [SerializeField]
    private AssetLabelReference _labelReference
                              , _backGroundImage
                              , _itemListLabel
                              , _playerUnitLabel
                              , _playerRotationLabel;

    // モンスターイメージ画像格納
    public List<Sprite> monsterImageList;

    // 背景画像の格納
    public List<Sprite> stageBackGroundList;

    // ItemListの格納
    public List<ItemSO> itemList;

    // PlayerのUnitを格納
    public List<GameObject> playerUnitList
                          , playerRotationUnitList;

    // 金額の管理
    private int gameMoney = 0;

    public int GameMoney 
    { 
        get { return gameMoney; }  
        set { gameMoney = value; } 
    }

    // 経験値の管理
    private int exp = 0;
    public int Exp 
    { 
        get { return exp; }
        set { exp = value; } 
    }

    // ステータスアップさせるユニット判定用
    public int statusUpTargetNo = -1;

    public List<UnitPowerUpStatusData> statusDataList;

    public Status statusList = new Status();

    // 購入済か判定するフラグ
    public List<bool> buyFlagList;
    public bool buyLoadFlag = false;

    // 選択したシーン
    private string currentStage = "";

    public string CurrentStage
    {
        get { return currentStage; }
        set { currentStage = value; }
    }

    // クリアステージ
    private string clearStage = "";

    public string ClearStage
    {
        get { return clearStage; }
        set { clearStage = value; }
    }

    // セットしたモンスターの引継ぎ用
    public SelectMonsterList selectMonsterList = null;

    public bool spriteLoadFlag = false
              , backGroundFlag = false
              , itemListFlag = false
              , playerUnitFlag = false
              , playerUnitRottationFlag = false;

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
        BackGroundImageLoad();
        ItemListLoad();
        PlayerUnitList();
        StartCoroutine(ItemCountList());
        selectMonsterList = this.GetComponent<SelectMonsterList>();
        statusDataList = new List<UnitPowerUpStatusData>();
        statusList = GetComponent<Status>();
        StartCoroutine(CreatePowerUpDataList());
        StartCoroutine(BuyFlagList());
        ClearStage = "039";

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

    // 背景画像のロード
    void BackGroundImageLoad()
    {
        Addressables.LoadAssetsAsync<Sprite>(_backGroundImage, null).
        Completed += op =>
        {
            foreach (Sprite sprite in op.Result)
            {
                stageBackGroundList.Add(sprite);
            }

            backGroundFlag = true;
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

    // PlayerUnitのロード
    void PlayerUnitList()
    {
        Addressables.LoadAssetsAsync<GameObject>(_playerUnitLabel, null).
        Completed += op =>
        {
            foreach (GameObject obj in op.Result)
            {
                playerUnitList.Add(obj);
            }

            playerUnitFlag = true;
        };

        Addressables.LoadAssetsAsync<GameObject>(_playerRotationLabel, null).
        Completed += op =>
        {
            foreach (GameObject obj in op.Result)
            {
                playerRotationUnitList.Add(obj);
            }

            playerUnitRottationFlag = true;
        };
    }

    // ユニットのステータスアップ判定用のリスト作成
    IEnumerator CreatePowerUpDataList()
    {
        yield return new WaitUntil(() => playerUnitFlag);

        UnitPowerUpStatusData data = new UnitPowerUpStatusData();

        for (int i = 0; i < playerUnitList.Count; i++)
        {
            statusDataList.Add( data );
        }

    }

    // 購入済か判定するフラグ
    IEnumerator BuyFlagList()
    {
        yield return new WaitUntil(() => playerUnitFlag);

        for (int i = 0; i < playerUnitList.Count; i++)
        {
            buyFlagList.Add(false);
        }

        buyLoadFlag = true;

    }
}
