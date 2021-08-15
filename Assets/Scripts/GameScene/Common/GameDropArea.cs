using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameDropArea : DropArea
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        sm = GameObject.FindWithTag("SummonManager").GetComponent<SummonManager>();
    }

    public override void OnDrop(PointerEventData data)
    {
        base.OnDrop(data);
        DragObj dragObj = data.pointerDrag.GetComponent<DragObj>();

        if (dragObj != null)
        {
            MonsterIndex = dragObj.SibilingIndex;
            int panelNo = ChangeScript.SummonPanelIndex(transform.name);

            if (panelNo <= 2)
            {
                sm.UpperRowSummon(MonsterIndex - 1, panelNo);
            }
            else
            {
                sm.LowerRowSummon(MonsterIndex - 1, panelNo - 3);
            }
        }
    }

}
