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

    SummonManager sm;

    private void Start()
    {
        sm = GameObject.FindWithTag("SummonManager").GetComponent<SummonManager>();
    }

    public void OnDrop(PointerEventData data)
    {
        DragObj dragObj = data.pointerDrag.GetComponent<DragObj>();
        if (dragObj != null)
        {
            MonsterIndex = dragObj.SibilingIndex;
            int panelNo = SummonPanelIndex(transform.name);

            if (panelNo <= 2)
            {
                sm.UpperRowSummon(MonsterIndex - 1, panelNo);
            }
            else
            {
                sm.LowerRowSummon(MonsterIndex - 1, panelNo - 3);
            }


            Debug.Log(gameObject.name + "‚É" + data.pointerDrag.name + "‚ðƒhƒƒbƒv");
        }
    }

    int SummonPanelIndex(string name)
    {
        int len = name.Length;
        string str = name.Substring(len - 1);
        int num = int.Parse(str) - 1;

        return num;
    }

}
