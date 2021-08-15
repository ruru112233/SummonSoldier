using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StrategyDragObj : DragObj
{
    public int monsterNo = 0;
    MasterData masterData = null;

    Image monsterImage = null;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        masterData = MasterData.instance;
        StartCoroutine(SpriteView());
        monsterImage = this.GetComponent<Image>();
    }

    // masterデータにロードされたら、スプライトを表示する
    IEnumerator SpriteView()
    {
        yield return new WaitUntil(() => masterData.spriteLoadFlag);

        monsterImage.sprite = masterData.monsterImageList[monsterNo];
    }


    public override void OnBeginDrag(PointerEventData data)
    {
        base.OnBeginDrag(data);
        instancePanel.transform.SetSiblingIndex(SibilingIndex);
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
