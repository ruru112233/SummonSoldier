using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StrategyDragObj : DragObj
{
    public int monsterNo = 0;
    MasterData masterData = null;

    private bool moveFlag = true;

    public bool MoveFlag
    {
        get { return moveFlag; }
        set { moveFlag = value; }
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        masterData = MasterData.instance;
        StartCoroutine(SpriteView());
        monsterImage = this.GetComponent<Image>();
    }


    // master�f�[�^�Ƀ��[�h���ꂽ��A�X�v���C�g��\������
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
        base.OnBeginDrag(data);
        instancePanel.transform.SetSiblingIndex(SibilingIndex);
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
        base.OnEndDrag(data);
        MoveFlag = selectMonsterCheck();

        Debug.Log(MoveFlag);

    }

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
}
