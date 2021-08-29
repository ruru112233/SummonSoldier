using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGround : MonoBehaviour
{
    public MasterData masterData;

    // Start is called before the first frame update
    void Start()
    {
        masterData = MasterData.instance;

        StartCoroutine(ChengeBackGround());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // バックグラウンドの入れ替え
    IEnumerator ChengeBackGround()
    {
        yield return new WaitUntil(() => masterData.backGroundFlag);

        SpriteRenderer backImage = this.GetComponent<SpriteRenderer>();

        int index = int.Parse(masterData.CurrentStage.Substring(0, 2));

        backImage.sprite = masterData.stageBackGroundList[index - 1];
    }
}
