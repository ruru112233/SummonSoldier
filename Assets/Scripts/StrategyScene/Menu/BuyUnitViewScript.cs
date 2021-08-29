using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyUnitButton
{
    public Sprite sprite = null;
    public int money = 0;
    public bool buyFlag = false;
    public int index = 0;
}

public class BuyUnitViewScript : MonoBehaviour
{

    [SerializeField]
    private GameObject buyUnitContent = null;

    [SerializeField]
    private Button buttonPrefab;

    MasterData masterData;

    // Start is called before the first frame update
    void Start()
    {
        masterData = MasterData.instance;
    }

    public void AddBuyUnitButton(BuyUnitButton data)
    {
        Button instance = null;

        instance = Instantiate(buttonPrefab);

        var sprite = data.sprite;
        var money = data.money;
        var buyFlag = data.buyFlag;
        var index = data.index;


        instance.transform.SetParent(buyUnitContent.transform, false);

        instance.GetComponent<Image>().sprite = sprite;

        var button = instance.GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            masterData.buyFlagList[index] = true;
            Destroy(instance.gameObject);
        });
        instance.gameObject.SetActive(true);

    }


}
