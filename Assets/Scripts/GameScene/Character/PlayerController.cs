using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : CharaController
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        text.SetActive(false);

        PositionCheck.PositionChenge(myTransform, 0f);

        waitTime = CalcScript.AttackTime(Speed);
    }

    public override void Update()
    {
        base.Update();

        // この処理は重いか？
        startTextPos = new Vector3(myTransform.position.x, myTransform.position.y + 2.0f, myTransform.position.z);

        // 敵のパネルにオブジェクトがあった場合、攻撃する
        if (longRangeFlag)
        {
            Debug.Log("遠距離");
            if (ChildCheck.AllCountCheck(enemyPanel)) AllAttack(enemyPanel, enemysObj);
        }
        else
        {
            Debug.Log("近距離");
            if (ChildCheck.FrontCountCheck(enemyPanel)&&
                ChildCheck.FrontCheck(this.transform)) 
                FrontAttack(enemyPanel, enemysObj);
        }

        // オブジェクトの数に変更があった場合、前衛移動の処理をする
        if (ObjCountCheck()) PositionCheck.PositionChenge(myTransform, 0f);

        // HP0になったらオブジェクトを削除
        if (Hp <= 0) Destroy(gameObject);
    }

    
}
