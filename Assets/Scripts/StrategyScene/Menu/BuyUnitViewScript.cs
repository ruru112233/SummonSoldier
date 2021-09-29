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
    public Color color = new Color();
}

public class BuyUnitViewScript : MonoBehaviour
{

    [SerializeField]
    private GameObject buyUnitContent = null;

    [SerializeField]
    private Button buttonPrefab;

    public GameObject buySelectPanel;
    public GameObject buyNgPanel;

    MasterData masterData;

    BuySelectPanel buySelectPanelScript;
    BuyNgPanel buyNgPanelScript;

    // Start is called before the first frame update
    void Start()
    {
        masterData = MasterData.instance;
        buySelectPanel.SetActive(false);
        buyNgPanel.SetActive(false);

        buySelectPanelScript = buySelectPanel.GetComponent<BuySelectPanel>();
        buyNgPanelScript = buyNgPanel.GetComponent<BuyNgPanel>();

    }

    public void AddBuyUnitButton(BuyUnitButton data)
    {
        Button instance = null;

        instance = Instantiate(buttonPrefab);
        Image image = instance.GetComponent<Image>();

        var sprite = data.sprite;
        var money = data.money;
        var buyFlag = data.buyFlag;
        var index = data.index;

        instance.transform.SetParent(buyUnitContent.transform, false);

        image.sprite = sprite;
        image.color = data.color;

        var button = instance.GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            if (image.color == Color.white)
            {
                BuyOk(money, instance, index);
            }
            else
            {
                BuyNg(money);
            }
            //masterData.buyFlagList[index] = true;
            //Destroy(instance.gameObject);
        });
        instance.gameObject.SetActive(true);

    }

    // 購入出来る場合
    void BuyOk(int money, Button instance, int index)
    {
        string buyText = "このユニットは、" + money.ToString() + "Gで購入出来ます。購入しますか？";

        buySelectPanel.transform.GetChild(0).GetComponent<Text>().text = buyText;
        buySelectPanelScript.button = instance.gameObject;
        buySelectPanelScript.index = index;
        buySelectPanel.SetActive(true);
    }

    // 購入出来ない場合
    void BuyNg(int money)
    {
        string buyText = "このユニットは、" + money.ToString() + "Gが必要なため、購入出来ません";

        buyNgPanel.transform.GetChild(0).GetComponent<Text>().text = buyText;
        buyNgPanel.SetActive(true);
    }



}
