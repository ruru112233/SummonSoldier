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

        //StartCoroutine(UnitAddButton());

    }

    public IEnumerator UnitAddButton()
    {
        yield return new WaitUntil(() => masterData.spriteLoadFlag && 
                                         masterData.playerUnitFlag &&
                                         masterData.playerUnitRottationFlag);

        // 初期化処理
        GameObject[] unitButtons = GameObject.FindGameObjectsWithTag("UnitButton");

        foreach (GameObject unitButton in unitButtons)
        {
            Destroy(unitButton);
        }

        for (int i = 0; i < masterData.monsterImageList.Count; i++)
        {
            Sprite unitImage = masterData.monsterImageList[i];
            CharaController unitData = masterData.playerUnitList[i].GetComponent<CharaController>();

            var data = new UnitData();

            var money = unitData.Money;
            var buyFlag = masterData.buyFlagList[i];

            data.sprite = unitImage;
            data.unitName = unitData.UnitName;
            data.hp = unitData.Hp + masterData.statusList.hpList[i];
            data.at = unitData.At + masterData.statusList.atList[i];
            data.df = unitData.Df + masterData.statusList.dfList[i];
            data.speed = unitData.Speed + masterData.statusList.speedList[i];
            data.attackRange = GetAttackRange(unitData);
            data.attackType = GetAttackType(unitData);
            data.explanatory = unitData.Explanatory;
            
            data.rotationObj = masterData.playerRotationUnitList[i];
            data.unitIndex = i;

            if (money == 0 || buyFlag)
            {
                unitView.AddUnitButton(data);
            }
           
        }
    }

    // AttackRangeを取得
    string GetAttackRange(CharaController chara)
    {
        string range = null;

        if (chara.LongRangeFlag)
        {
            range = "遠距離";
        }
        else
        {
            range = "近距離";
        }

        return range;
    }

    // AttackTypeを取得
    string GetAttackType(CharaController chara)
    {
        string type = null;

        if (Prms.ATTACK_TYPE.SINGLE_RANGE == chara.attack_type)
        {
            type = "単体攻撃";
        }
        else if (Prms.ATTACK_TYPE.COLUMN_RANGE == chara.attack_type)
        {
            type = "縦一列攻撃";
        }
        else if (Prms.ATTACK_TYPE.ROW_RANGE == chara.attack_type)
        {
            type = "横一列攻撃";
        }else if (Prms.ATTACK_TYPE.ALL_RANGE == chara.attack_type)
        {
            type = "全体攻撃";
        }

        return type;
    }

}
