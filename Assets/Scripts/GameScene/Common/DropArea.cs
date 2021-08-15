using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IDropHandler
{
    private int monsterIndex = 0;

    public int MonsterIndex
    {
        get { return monsterIndex; }
        set { monsterIndex = value; }
    }

    protected SummonManager sm;
    protected MasterData md;

    public virtual void Start()
    {
        
        
    }

    public virtual void OnDrop(PointerEventData data)
    {
        
        Debug.Log(gameObject.name + "Ç…" + data.pointerDrag.name + "ÇÉhÉçÉbÉv");
    }

    public int SummonPanelIndex(string name)
    {
        int len = name.Length;
        string str = name.Substring(len - 1);
        int num = int.Parse(str) - 1;

        return num;
    }

}
