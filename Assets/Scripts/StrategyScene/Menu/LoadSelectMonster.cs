using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSelectMonster : MonoBehaviour
{
    [SerializeField]
    private SelectMonsterVieu selectUnitView = null;

    MasterData masterData;

    // Start is called before the first frame update
    void Start()
    {
        masterData = MasterData.instance;

        StartCoroutine(SelectUnitAddButton());
    }

    public IEnumerator SelectUnitAddButton()
    {
        yield return new WaitUntil(() => masterData.spriteLoadFlag &&
                                         masterData.playerUnitFlag &&
                                         masterData.buyLoadFlag);

        // èâä˙âªèàóù
        GameObject[] SelectMonsters = GameObject.FindGameObjectsWithTag("SelectMonster");

        foreach (GameObject SelectMonster in SelectMonsters)
        {
            Destroy(SelectMonster);
        }

        int panelIndex = 0;

        for (int i = 0; i < masterData.monsterImageList.Count; i++)
        {
            Sprite unitImage = masterData.monsterImageList[i];
            CharaController unitData = masterData.playerUnitList[i].GetComponent<CharaController>();

            var data = new UnitData();

            var money = unitData.Money;
            var buyFlag = masterData.buyFlagList[i];

            data.sprite = unitImage;
            data.unitIndex = i;

            if (money == 0 || buyFlag)
            {
                data.panelIndex = panelIndex;
                selectUnitView.AddSelectMonsterButton(data);
                panelIndex++;
            }

        }
    }

}
