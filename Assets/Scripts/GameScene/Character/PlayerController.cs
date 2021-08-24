using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : CharaController
{
    MasterController enemyMaster = null;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        text.SetActive(false);

        enemyMaster = GameObject.FindWithTag("MasterEnemy").GetComponent<MasterController>();

        PositionCheck.PositionChenge(myTransform, 0f);

    }

    public override void Update()
    {
        base.Update();

        // この処理は重いか？
        startTextPos = new Vector3(myTransform.position.x, myTransform.position.y + 2.0f, myTransform.position.z);

        // 敵のパネルにオブジェクトがあった場合、攻撃する
        // 攻撃タイプによって攻撃範囲を分ける
        
        if (ATTACK_TYPE.SINGLE_RANGE == attack_type)
        {
            // 単体
            if (longRangeFlag)
            {
                Debug.Log("遠距離");
                if (ChildCheck.AllCountCheck(enemyPanel)) 
                    AllAttack(enemyPanel, enemysObj);
            }
            else
            {
                Debug.Log("近距離");
                if (ChildCheck.FrontCountCheck(enemyPanel) &&
                    ChildCheck.FrontCheck(this.transform))
                    FrontAttack(enemyPanel, enemysObj);
            }
        }
        else if(ATTACK_TYPE.COLUMN_RANGE == attack_type)
        {
            // 縦一列
            if(ChildCheck.ColumnCheck(enemyPanel)) 
                ColumnAttack(enemyPanel, enemysObj);

        }
        else if(ATTACK_TYPE.ROW_RANGE == attack_type)
        {
            // 横一列
            if (longRangeFlag)
            {
                if(ChildCheck.RowCheck(enemyPanel)) 
                    AllRowAttack(enemyPanel, enemysObj);
            }
            else
            {
                if(ChildCheck.FrontRowCheck(enemyPanel)) 
                    FrontRowAttack(enemyPanel, enemysObj);
            }
        }
        else if (ATTACK_TYPE.ALL_RANGE == attack_type)
        {
            // 全体
            if (ChildCheck.AllCountCheck(enemyPanel))
                AllRangeAttack(enemyPanel, enemysObj);
        }

        // オブジェクトの数に変更があった場合、前衛移動の処理をする
        if (ObjCountCheck()) PositionCheck.PositionChenge(myTransform, 0f);

        // HP0になったらオブジェクトを削除
        if (Hp <= 0) Destroy(gameObject);
    }

}
