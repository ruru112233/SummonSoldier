using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadUnit : MonoBehaviour
{
    [SerializeField]
    private UnitViewSctipt unitView = null;

    MasterData masterData;

    // Start is called before the first frame update
    void Start()
    {
        masterData = MasterData.instance;

        StartCoroutine(UnitAddButton());
    }

    IEnumerator UnitAddButton()
    {
        yield return new WaitUntil(() => masterData.spriteLoadFlag && masterData.playerUnitFlag);

        var index = 0;

        for (int i = 0; i < masterData.monsterImageList.Count; i++)
        {
            Sprite unitImage = masterData.monsterImageList[i];
            CharaController unitData = masterData.playerUnitList[i].GetComponent<CharaController>();

            var data = new UnitData();

            data.sprite = unitImage;
            data.unitName = unitData.UnitName;
            data.unitIndex = index;

            unitView.AddUnitButton( data );

            index++;

        }

    }

}
