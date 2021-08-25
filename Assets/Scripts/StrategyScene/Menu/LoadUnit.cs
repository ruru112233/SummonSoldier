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
        yield return new WaitUntil(() => masterData.spriteLoadFlag && 
                                         masterData.playerUnitFlag &&
                                         masterData.playerUnitRottationFlag);

        var index = 0;

        for (int i = 0; i < masterData.monsterImageList.Count; i++)
        {
            Sprite unitImage = masterData.monsterImageList[i];
            CharaController unitData = masterData.playerUnitList[i].GetComponent<CharaController>();

            var data = new UnitData();

            data.sprite = unitImage;
            data.unitName = unitData.UnitName;
            data.at = unitData.At;
            data.df = unitData.Df;
            data.speed = unitData.Speed;
            data.attackRange = GetAttackRange(unitData);
            data.attackType = GetAttackType(unitData);
            data.explanatory = unitData.Explanatory;

            data.rotationObj = masterData.playerRotationUnitList[i];
            data.unitIndex = index;

            unitView.AddUnitButton( data );

            index++;

        }
    }

    // AttackRange‚ğæ“¾
    string GetAttackRange(CharaController chara)
    {
        string range = null;

        if (chara.LongRangeFlag)
        {
            range = "‰“‹——£";
        }
        else
        {
            range = "‹ß‹——£";
        }

        return range;
    }

    // AttackType‚ğæ“¾
    string GetAttackType(CharaController chara)
    {
        string type = null;

        if (Prms.ATTACK_TYPE.SINGLE_RANGE== chara.attack_type)
        {
            type = "’P‘ÌUŒ‚";
        }
        else if (Prms.ATTACK_TYPE.COLUMN_RANGE == chara.attack_type)
        {
            type = "cˆê—ñUŒ‚";
        }
        else if (Prms.ATTACK_TYPE.ROW_RANGE == chara.attack_type)
        {
            type = "‰¡ˆê—ñUŒ‚";
        }else if (Prms.ATTACK_TYPE.ALL_RANGE == chara.attack_type)
        {
            type = "‘S‘ÌUŒ‚";
        }

        return type;
    }

}
