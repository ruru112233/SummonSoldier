using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Item")]
public class ItemSO : ScriptableObject
{
    [SerializeField]
    public int id;

    [SerializeField]
    private string itemName;

    [SerializeField]
    private int value;

    [SerializeField]
    private string description;

    [SerializeField]
    private Sprite sprite;

    public enum ITEM_TYPE
    {
        MAXHPUP, //MaxHpを上昇
        ATUP, //攻撃力アップ
        DFUP, //守備力アップ
        SPEEDUP, //スピードアップ
    }

    public ITEM_TYPE item_type;


    public int GetId()
    {
        return id;
    }

    public string GetItemName()
    {
        return itemName;
    }

    public int GetValue()
    {
        return value;
    }

    public string GetDescription()
    {
        return description;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }



}
