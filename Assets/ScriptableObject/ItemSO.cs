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
        MAXHPUP, //MaxHp���㏸
        ATUP, //�U���̓A�b�v
        DFUP, //����̓A�b�v
        SPEEDUP, //�X�s�[�h�A�b�v
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
