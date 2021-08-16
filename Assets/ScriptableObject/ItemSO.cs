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
        MAXHPUP, //MaxHp‚ğã¸
        ATUP, //UŒ‚—ÍƒAƒbƒv
    }

    public ITEM_TYPE item_type;

}
