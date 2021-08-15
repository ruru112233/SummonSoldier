using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

public class MasterData : MonoBehaviour
{
    [SerializeField]
    private AssetLabelReference _labelReference;

    public List<Sprite> monsterImageList;

    // Start is called before the first frame update
    void Start()
    {
        MonsterImageLoad();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // キャラ画像のロード
    void MonsterImageLoad()
    {
        Addressables.LoadAssetsAsync<Sprite>(_labelReference, null).
        Completed += op =>
        {
          foreach (Sprite sprite in op.Result)
          {
              monsterImageList.Add(sprite);
          }
        };
    } 

}
