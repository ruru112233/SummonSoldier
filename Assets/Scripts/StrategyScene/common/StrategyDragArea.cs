using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StrategyDragArea : DropArea
{
    Image monsterImage = null;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        monsterImage = this.GetComponent<Image>();
        
    }

    public override void OnDrop(PointerEventData data)
    {
        base.OnDrop(data);

        StrategyDragObj strategyDragObj = data.pointerDrag.GetComponent<StrategyDragObj>();

        if (strategyDragObj != null)
        {
            Debug.Log(strategyDragObj.monsterNo);
            monsterImage.sprite = md.monsterImageList[strategyDragObj.monsterNo];
            md.selectMonsterList.selectMonsterList[ChangeScript.SummonPanelIndex(gameObject.name)]
                = strategyDragObj.monsterNo;
        }
    }
}
