using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameDragObj : DragObj
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        int objNo = ChangeScript.SummonPanelIndex(gameObject.name);
        MonsterIndex = md.selectMonsterList.selectMonsterList[objNo];
        if (MonsterIndex >= 0) monsterImage.sprite = md.monsterImageList[MonsterIndex];
    }

    public override void OnBeginDrag(PointerEventData data)
    {
        base.OnBeginDrag(data);
    }

    public override void OnDrag(PointerEventData data)
    {
        base.OnDrag(data);
    }

    public override void OnEndDrag(PointerEventData data)
    {
        base.OnEndDrag(data);
    }



}
