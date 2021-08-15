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

    public bool spriteLoadFlag = false;

    public static MasterData instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (this != instance)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }

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

            spriteLoadFlag = true;
        };
    } 

}
