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
        md = GameObject.FindWithTag("MasterData").GetComponent<MasterData>();
    }

    public virtual void OnDrop(PointerEventData data)
    {
        
        Debug.Log(gameObject.name + "Ç…" + data.pointerDrag.name + "ÇÉhÉçÉbÉv");
    }

    

}
