using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StrategyDragObj : DragObj
{
    public int monsterNo = 0;
    public int panelIndex = 0;
    MasterData masterData = null;

    private bool moveFlag = true;

    public bool MoveFlag
    {
        get { return moveFlag; }
        set { moveFlag = value; }
    }

    int index = 0;

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

    public void Update()
    {
        monsterImage.color = ImageColor();
    }

    public override void OnBeginDrag(PointerEventData data)
    {
        if (MoveFlag)
        {
            base.OnBeginDrag(data);
            instancePanel.transform.SetSiblingIndex(panelIndex);
        }
    }

    public override void OnDrag(PointerEventData data)
    {
        if (MoveFlag)
        {
            base.OnDrag(data);
        }
    }

    public override void OnEndDrag(PointerEventData data)
    {
        if (MoveFlag)
        {
            base.OnEndDrag(data);
            MoveFlag = selectMonsterCheck();

        }
        transform.SetSiblingIndex(panelIndex);

    }

    // イメージの色を返す（合わせて、存在していなければMoveFlagも操作）
    Color ImageColor()
    {
        SelectMonsterList selectMonsterList = md.selectMonsterList;
        foreach (int mon in selectMonsterList.selectMonsterList)
        {
            if (mon == monsterNo)
            {
                return Color.gray;
            }
        }

        MoveFlag = true;
        return Color.white;
    }

    bool selectMonsterCheck()
    {
        SelectMonsterList selectMonsterList = md.selectMonsterList;
        foreach (int mon in selectMonsterList.selectMonsterList)
        {
            if (mon == monsterNo)
            {
                return false;
            }
        }

        return true;
    }

    // セレクトモンスターの数をカウント
    int objLen()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("SelectMonster");

        int len = objs.Length;

        return len;
    }
}
