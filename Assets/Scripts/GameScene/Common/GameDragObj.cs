using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameDragObj : DragObj
{
    ManaManager manaManager;
    PlayerController player;
    public bool dropFlag = true;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        int objNo = ChangeScript.SummonPanelIndex(gameObject.name);
        manaManager = GameManager.instance.manaManager;
        MonsterIndex = md.selectMonsterList.selectMonsterList[objNo];
        if (MonsterIndex >= 0) 
        {
            monsterImage.sprite = md.monsterImageList[MonsterIndex];
            player = GameManager.instance.summonManager.soldierPrefabs[MonsterIndex].GetComponent<PlayerController>();
        }
    }

    public override void OnBeginDrag(PointerEventData data)
    {
        if(player && manaManager.CurentManaPoint >= player.Cost)
        {
            base.OnBeginDrag(data);
            dropFlag = true;
        }
        else
        {
            dropFlag = false;
        }
            
    }

    public override void OnDrag(PointerEventData data)
    {
        if (player && manaManager.CurentManaPoint >= player.Cost)
            base.OnDrag(data);
    }

    public override void OnEndDrag(PointerEventData data)
    {
        base.OnEndDrag(data);

        if (player && manaManager.CurentManaPoint >= player.Cost)
        {
            manaManager.CurentManaPoint -= player.Cost;   
        }
    }

}
