using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMonsterVieu : MonoBehaviour
{


    [SerializeField]
    private GameObject unitContent = null;

    [SerializeField]
    private Image unitImagePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddSelectMonsterButton(UnitData data)
    {
        Image instance = null;
        
        instance = Instantiate(unitImagePrefab);

        StrategyDragObj dragObj = instance.GetComponent<StrategyDragObj>();

        var sprite = data.sprite;
        var index = data.unitIndex;
        var panelIndel = data.panelIndex;

        dragObj.monsterNo = index;
        dragObj.panelIndex = panelIndel;

        instance.transform.SetParent(unitContent.transform, false);

        Image image = instance.GetComponent<Image>();

        image.sprite = sprite;
        image.enabled = true;

        instance.gameObject.SetActive(true);
    }
}
