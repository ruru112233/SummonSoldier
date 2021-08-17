using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public int value;
    public string description;
    public Sprite sprite;

    public enum ITEM_TYPE
    {
        MAXHPUP, //MaxHpを上昇
        ATUP, //攻撃力アップ
        DFUP, //守備力アップ
        SPEEDUP, //スピードアップ
    }

    public ITEM_TYPE item_type;

}
