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

    [SerializeField]
    private int dropMoney = 0;

    MasterData masterData;

    MasterController masterPlayer = null;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        masterData = MasterData.instance;

        text.SetActive(false);

        masterPlayer = GameObject.FindWithTag("MasterPlayer").GetComponent<MasterController>();

        // 下段に召喚され、上段が召喚されていなかったら上段に上げる
        PositionCheck.PositionChenge(myTransform, 1.8f);

    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        // 敵のパネルにオブジェクトがあった場合、攻撃する
        if (ATTACK_TYPE.SINGLE_RANGE == attack_type)
        {
            // 単体
            if (longRangeFlag)
            {
                Debug.Log("遠距離");
                if (ChildCheck.AllCountCheck(playerPanel))
                    AllAttack(playerPanel, playersObj, At);
            }
            else
            {
                Debug.Log("近距離");
                if (ChildCheck.FrontCountCheck(playerPanel) &&
                    ChildCheck.FrontCheck(this.transform))
                    FrontAttack(playerPanel, playersObj, At);
            }
        }
        else if (ATTACK_TYPE.COLUMN_RANGE == attack_type)
        {
            // 縦一列
            if (ChildCheck.ColumnCheck(playerPanel))
                ColumnAttack(playerPanel, playersObj, At);

        }
        else if (ATTACK_TYPE.ROW_RANGE == attack_type)
        {
            // 横一列
            if (longRangeFlag)
            {
                if (ChildCheck.RowCheck(playerPanel))
                    AllRowAttack(playerPanel, playersObj, At);
            }
            else
            {
                if (ChildCheck.FrontRowCheck(playerPanel))
                    FrontRowAttack(playerPanel, playersObj, At);
            }
        }
        else if (ATTACK_TYPE.ALL_RANGE == attack_type)
        {
            // 全体
            if (ChildCheck.AllCountCheck(playerPanel))
                AllRangeAttack(playerPanel, playersObj, At);
        }

        // オブジェクトの数に変更があった場合、前衛移動の処理をする
        if (ObjCountCheck()) PositionCheck.PositionChenge(myTransform, 1.8f);

        // Enemyの子オブジェクトが存在しなかった時は、masterオブジェクトへ攻撃
        if (masterPlayer && ChildCheck.PlayerChildObjectCount() <= 0)
            MasterObjectAttack(masterPlayer, At);

        // HPが0になった時の処理
        if (Hp <= 0)
        {
            GetRandomItem();
            masterData.GameMoney += dropMoney;
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
