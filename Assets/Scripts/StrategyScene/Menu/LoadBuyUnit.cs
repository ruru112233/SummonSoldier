using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBuyUnit : MonoBehaviour
{
    [SerializeField]
    private BuyUnitViewScript buyUnitView = null;

    MasterData masterData;

    // Start is called before the first frame update
    void Start()
    {
        masterData = MasterData.instance;
    }

    public IEnumerator BuyAddButton()
    {

        yield return new WaitUntil(() => masterData.spriteLoadFlag &&
                                         masterData.playerUnitFlag);

        GameObject[] buyUnitButtons = GameObject.FindGameObjectsWithTag("BuyUnitButton");

        foreach (GameObject button in buyUnitButtons)
        {
            Destroy(button);
        }

        for (int i = 0; i < masterData.monsterImageList.Count; i++)
        {
            Sprite unitImage = masterData.monsterImageList[i];
            CharaController unitData = masterData.playerUnitList[i].GetComponent<CharaController>();

            var data = new BuyUnitButton();
            var buyFlag = masterData.buyFlagList[i];

            data.sprite = unitImage;
            data.money = unitData.Money;
            data.index = i;

            // èäéùã‡äzÇ∆çwì¸ã‡äzÇî‰ärÇµÅAêFÇïœçX
            data.color = colorChange(data.money, masterData.GameMoney);

            if (data.money != 0 && !buyFlag)
            {
                buyUnitView.AddBuyUnitButton(data);
            }
        }
    }

    Color colorChange(int unitMoney, int gameMoney)
    {
        if (unitMoney > gameMoney)
        {
            // äDêF
            //return color = new Color(50, 0, 0, 255);
            return Color.gray;
        }

        return Color.white;

    }


}
