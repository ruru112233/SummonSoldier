using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;

public class EnemyController : CharaController
{
    [SerializeField]
    private List<int> dropItemList = new List<int>();

    MasterData masterData;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        masterData = MasterData.instance;

        text.SetActive(false);

        // 下段に召喚され、上段が召喚されていなかったら上段に上げる
        PositionCheck.PositionChenge(myTransform, 1.8f);

        waitTime = CalcScript.AttackTime(Speed);


    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        // 敵のパネルにオブジェクトがあった場合、攻撃する
        if (longRangeFlag)
        {
            if (ChildCheck.AllCountCheck(playerPanel)) AllAttack(playerPanel, playersObj);
        }
        else
        {
            if (ChildCheck.FrontCountCheck(playerPanel)) FrontAttack(playerPanel, playersObj);
        }

        // オブジェクトの数に変更があった場合、前衛移動の処理をする
        if (ObjCountCheck()) PositionCheck.PositionChenge(myTransform, 1.8f);

        // HPが0になった時の処理
        if (Hp <= 0)
        {
            GetRandomItem();
            Destroy(gameObject);
        }
    }

    
    // HPが0になった時、ランダムでアイテムをドロップする
    ///
    /// id1 = タイリョクノビール
    /// id2 = タイリョクグントノビール
    /// id3 = アタックノビール
    /// id4 = アタックグントノビール
    /// id5 = ディフェンスノビール
    /// id6 = ディフェンスグントノビール
    /// id7 = スピードノビール
    /// id8 = スピードグントノビール
    ///
    void GetRandomItem()
    {
        int randNo = Random.Range(0, dropItemList.Count);
        int randItemId = dropItemList[randNo];

        string dropItemId = "id" + (randItemId + 1);

        if(masterData.itemCounter.ContainsKey(dropItemId))
            masterData.itemCounter[dropItemId] = masterData.itemCounter[dropItemId] + 1;

    }


}
